using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.Avalonia;

public class PalletItem
{
    public Pallet Pallet { get; }
    public string Name => Pallet.Name;
    public List<IBrush> ColorBrushes => Pallet.Colors
        .Select(c => (IBrush)new SolidColorBrush(new Color(255, c.Red, c.Green, c.Blue)))
        .ToList();

    public PalletItem(Pallet pallet) => Pallet = pallet;
}

public partial class MainWindow : Window
{
    private List<Pallet> _pallets = new();
    private Pallet? _activePalette;
    private SKBitmap? _lastBitmap;
    private WallpaperConfig _config = WallpaperConfig.CreateDefault();

    private const string DefaultPalletJson = """
    {
      "Pallets": [
        {
          "Pallet": {
            "Name": "Sample Pallet",
            "Colors": [
              "229,244,227",
              "93,169,233",
              "0,63,145",
              "255,255,255",
              "109,50,109"
            ]
          }
        },
        {
          "Pallet": {
            "Name": "Another Pallet",
            "Colors": [
              "109,238,170",
              "183,209,18",
              "216,236,126",
              "210,64,22",
              "177,107,7"
            ]
          }
        }
      ]
    }
    """;

    private readonly string _userPalletsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "WallpaperMaker",
        "UserPallets.json");

    public MainWindow()
    {
        InitializeComponent();
        InitFormElements();
    }

    private void InitFormElements()
    {
        if (File.Exists(_userPalletsPath))
        {
            try
            {
                string json = File.ReadAllText(_userPalletsPath);
                _pallets = Utilities.UnpackUserColorPallets(json);
            }
            catch
            {
                _pallets = Utilities.UnpackUserColorPallets(DefaultPalletJson);
            }
        }
        else
        {
            _pallets = Utilities.UnpackUserColorPallets(DefaultPalletJson);
        }

        var externalPath = Path.Combine("Resources", "ColorPallets.json");
        if (File.Exists(externalPath))
            _pallets.AddRange(Utilities.UnpackExternalColorPallets());

        RefreshPaletteComboBox();
        DetectResolution();
        UpdateMultisamplingList();
    }

    private void DetectResolution()
    {
        var screen = Screens.ScreenFromWindow(this) ?? Screens.Primary;
        if (screen != null)
        {
            TbWidth.Text  = screen.Bounds.Width.ToString();
            TbHeight.Text = screen.Bounds.Height.ToString();
        }
        else
        {
            TbWidth.Text  = "1920";
            TbHeight.Text = "1080";
        }
    }

    private void RefreshPaletteComboBox()
    {
        CbPalette.Items.Clear();
        foreach (var p in _pallets)
            CbPalette.Items.Add(new PalletItem(p));

        if (_pallets.Count > 0)
        {
            CbPalette.SelectedIndex = 0;
            _activePalette = _pallets[0];
        }
    }

    private void UpdateMultisamplingList()
    {
        int prevIdx = CbMultisampling.SelectedIndex;
        CbMultisampling.Items.Clear();

        if (!int.TryParse(TbWidth.Text, out int w)) w = 1920;
        if (!int.TryParse(TbHeight.Text, out int h)) h = 1080;

        double maxSize = Math.Pow(2, 32);
        for (int i = 0; i <= 32; i++)
        {
            double resSize = (double)w * i * h * i * 4;
            if (resSize < maxSize && resSize >= 0)
                CbMultisampling.Items.Add(i.ToString());
            else
                break;
        }

        if (prevIdx >= CbMultisampling.ItemCount)
            CbMultisampling.SelectedIndex = CbMultisampling.ItemCount - 1;
        else if (prevIdx > 0)
            CbMultisampling.SelectedIndex = prevIdx;
        else if (CbMultisampling.ItemCount > 0)
            CbMultisampling.SelectedIndex = 0;
    }

    private async void BtnGenerate_Click(object? sender, RoutedEventArgs e)
    {
        if (_activePalette == null || _activePalette.Colors.Count == 0)
        {
            TxtStatus.Text = "Error: No palette selected or palette has no colors.";
            return;
        }

        if (!int.TryParse(TbWidth.Text, out int w) || w <= 0)
        {
            TxtStatus.Text = "Error: Invalid width.";
            return;
        }
        if (!int.TryParse(TbHeight.Text, out int h) || h <= 0)
        {
            TxtStatus.Text = "Error: Invalid height.";
            return;
        }

        int ms = CbMultisampling.SelectedIndex;
        if (ms < 0) ms = 0;

        BtnGenerate.IsEnabled = false;
        PbProgress.IsVisible = true;
        PbProgress.IsIndeterminate = true;
        TxtStatus.Text = $"Generating {w}x{h} wallpaper (supersampling: {ms}x)...";

        try
        {
            _lastBitmap?.Dispose();
            _lastBitmap = await Task.Run(() =>
            {
                using var gen = new Generator(_activePalette, w, h, ms, _config);
                return gen.Generate(_config);
            });

            ImgPreview.Source = SKBitmapToAvaloniaBitmap(_lastBitmap);
            TxtStatus.Text = $"Generated {w}x{h} wallpaper.";
        }
        catch (Exception ex)
        {
            TxtStatus.Text = $"Error: {ex.Message}";
        }
        finally
        {
            BtnGenerate.IsEnabled = true;
            PbProgress.IsVisible = false;
            PbProgress.IsIndeterminate = false;
        }
    }

    private void BtnSettings_Click(object? sender, RoutedEventArgs e)
    {
        var settingsWindow = new SettingsWindow(_config);
        settingsWindow.ShowDialog(this).ContinueWith(t =>
        {
            if (settingsWindow.ResultConfig != null)
                _config = settingsWindow.ResultConfig;
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private async void BtnSave_Click(object? sender, RoutedEventArgs e)
    {
        if (_lastBitmap == null)
        {
            TxtStatus.Text = "Nothing to save yet — generate a wallpaper first.";
            return;
        }

        var topLevel = GetTopLevel(this);
        if (topLevel == null) return;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Wallpaper",
            DefaultExtension = "png",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("PNG Image") { Patterns = new[] { "*.png" } },
                new FilePickerFileType("JPEG Image") { Patterns = new[] { "*.jpg", "*.jpeg" } },
                new FilePickerFileType("BMP Image") { Patterns = new[] { "*.bmp" } },
                new FilePickerFileType("WebP Image") { Patterns = new[] { "*.webp" } },
            },
            SuggestedFileName = "wallpaper"
        });

        if (file == null) return;

        var path = file.Path.LocalPath;
        var ext = Path.GetExtension(path).ToLowerInvariant();
        var format = ext switch
        {
            ".jpg" or ".jpeg" => SKEncodedImageFormat.Jpeg,
            ".bmp" => SKEncodedImageFormat.Bmp,
            ".webp" => SKEncodedImageFormat.Webp,
            _ => SKEncodedImageFormat.Png
        };

        using var image = SKImage.FromBitmap(_lastBitmap);
        using var data = image.Encode(format, 95);
        await using var stream = File.OpenWrite(path);
        data.SaveTo(stream);

        TxtStatus.Text = $"Saved to: {path}";
    }

    private void BtnColors_Click(object? sender, RoutedEventArgs e)
    {
        var colorWindow = new ColorPickerWindow(_pallets);
        colorWindow.ShowDialog(this).ContinueWith(t =>
        {
            _pallets = colorWindow.ResultPallets;
            SavePallets();
            RefreshPaletteComboBox();
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void SavePallets()
    {
        try
        {
            string? dir = Path.GetDirectoryName(_userPalletsPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string json = Utilities.PackUpUserColorPallets(_pallets);
            File.WriteAllText(_userPalletsPath, json);
        }
        catch (Exception ex)
        {
            TxtStatus.Text = $"Error saving pallets: {ex.Message}";
        }
    }

    private void BtnAutoDetect_Click(object? sender, RoutedEventArgs e)
    {
        DetectResolution();
        UpdateMultisamplingList();
    }

    private void CbPalette_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (CbPalette.SelectedItem is PalletItem item)
            _activePalette = item.Pallet;
    }

    private static Bitmap SKBitmapToAvaloniaBitmap(SKBitmap skBitmap)
    {
        using var image = SKImage.FromBitmap(skBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = new MemoryStream(data.ToArray());
        return new Bitmap(stream);
    }
}
