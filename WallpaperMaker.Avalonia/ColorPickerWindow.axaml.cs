using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.Avalonia;

public class ColorItem
{
    public SKColor SkColor { get; }
    public string Label => $"{SkColor.Red}, {SkColor.Green}, {SkColor.Blue}";
    public IBrush Brush => new SolidColorBrush(new Color(255, SkColor.Red, SkColor.Green, SkColor.Blue));

    public ColorItem(SKColor color) => SkColor = color;
}

public partial class ColorPickerWindow : Window
{
    public List<Pallet> ResultPallets { get; private set; }
    private Pallet? _selectedPallet;

    public ColorPickerWindow() : this(new List<Pallet>()) { }

    public ColorPickerWindow(List<Pallet> pallets)
    {
        InitializeComponent();
        ResultPallets = new List<Pallet>(pallets);
        RefreshPaletteList();
        if (ResultPallets.Count > 0)
        {
            LbPalettes.SelectedIndex = 0;
            _selectedPallet = ResultPallets[0];
            RefreshColorList();
        }
    }

    private void RefreshPaletteList()
    {
        LbPalettes.Items.Clear();
        foreach (var p in ResultPallets)
            LbPalettes.Items.Add(new PalletItem(p));
    }

    private void RefreshColorList()
    {
        LbColors.Items.Clear();
        if (_selectedPallet == null) return;

        foreach (var c in _selectedPallet.Colors)
            LbColors.Items.Add(new ColorItem(c));
    }

    private void LbPalettes_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LbPalettes.SelectedItem is PalletItem item)
        {
            _selectedPallet = item.Pallet;
            RefreshColorList();
        }
    }

    private void LbColors_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LbColors.SelectedItem is ColorItem item)
        {
            BdrPreview.Background = item.Brush;
            TbR.Text = item.SkColor.Red.ToString();
            TbG.Text = item.SkColor.Green.ToString();
            TbB.Text = item.SkColor.Blue.ToString();
        }
    }

    private void BtnNewPalette_Click(object? sender, RoutedEventArgs e)
    {
        string? name = TbPaletteName.Text?.Trim();
        if (string.IsNullOrEmpty(name))
            name = NameGenerator.GenerateName(ResultPallets.Select(p => p.Name));

        if (ResultPallets.Any(p => p.Name == name))
        {
            string baseName = name;
            int counter = 2;
            while (ResultPallets.Any(p => p.Name == $"{baseName} ({counter})"))
                counter++;
            name = $"{baseName} ({counter})";
        }

        ResultPallets.Add(new Pallet(name, Array.Empty<string>()));
        RefreshPaletteList();
        LbPalettes.SelectedIndex = ResultPallets.Count - 1;
    }

    private void BtnRenamePalette_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedPallet == null) return;

        string newName = TbPaletteName.Text?.Trim() ?? "";
        if (string.IsNullOrEmpty(newName)) return;

        if (ResultPallets.Any(p => p.Name == newName && p != _selectedPallet))
            return;

        _selectedPallet.Rename(newName);
        int idx = LbPalettes.SelectedIndex;
        RefreshPaletteList();
        LbPalettes.SelectedIndex = idx;
    }

    private void BtnDeletePalette_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedPallet == null) return;

        ResultPallets.Remove(_selectedPallet);
        _selectedPallet = ResultPallets.FirstOrDefault();
        RefreshPaletteList();
        if (ResultPallets.Count > 0)
        {
            LbPalettes.SelectedIndex = 0;
            RefreshColorList();
        }
        else
        {
            LbColors.Items.Clear();
        }
    }

    private void BtnAddColor_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedPallet == null) return;

        if (!byte.TryParse(TbR.Text, out byte r) ||
            !byte.TryParse(TbG.Text, out byte g) ||
            !byte.TryParse(TbB.Text, out byte b))
            return;

        var color = new SKColor(r, g, b);
        _selectedPallet.AddColor(color);
        RefreshColorList();
    }

    private void BtnDeleteColor_Click(object? sender, RoutedEventArgs e)
    {
        if (_selectedPallet == null || LbColors.SelectedItem is not ColorItem item)
            return;

        _selectedPallet.RemoveColor(item.SkColor);
        RefreshColorList();
    }

    private void AddGeneratedPalette(ColorHarmony harmony)
    {
        string name = NameGenerator.GenerateName(ResultPallets.Select(p => p.Name));
        var pallet = ColorTheory.GeneratePalette(harmony, name);
        ResultPallets.Add(pallet);
        RefreshPaletteList();
        LbPalettes.SelectedIndex = ResultPallets.Count - 1;
    }

    private void BtnGenComplementary_Click(object? sender, RoutedEventArgs e)
        => AddGeneratedPalette(ColorHarmony.Complementary);

    private void BtnGenAnalogous_Click(object? sender, RoutedEventArgs e)
        => AddGeneratedPalette(ColorHarmony.Analogous);

    private void BtnGenTriadic_Click(object? sender, RoutedEventArgs e)
        => AddGeneratedPalette(ColorHarmony.Triadic);

    private void BtnGenMonochromatic_Click(object? sender, RoutedEventArgs e)
        => AddGeneratedPalette(ColorHarmony.Monochromatic);

    private void BtnGenRandom_Click(object? sender, RoutedEventArgs e)
    {
        string name = NameGenerator.GenerateName(ResultPallets.Select(p => p.Name));
        var pallet = ColorTheory.GenerateRandomPalette(name);
        ResultPallets.Add(pallet);
        RefreshPaletteList();
        LbPalettes.SelectedIndex = ResultPallets.Count - 1;
    }

    private void BtnDone_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
