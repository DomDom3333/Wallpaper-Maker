using System.Drawing.Imaging;
using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.WinForm;

public partial class frm_Main : Form
{
    internal List<Pallet> avaliablePallets { get; set; } = new();
    internal Pallet? PalletInUse { get; set; }
    internal Bitmap? finishedArt { get; set; }
    private SKBitmap? _skBitmap;
    public int xRes { get; set; }
    public int yRes { get; set; }

    public frm_Main()
    {
        InitializeComponent();
        initFormElements();
    }

    // UI EVENTS
    private void frm_Main_Load(object sender, EventArgs e)
    {
        if (avaliablePallets.Count > 0 && (PalletInUse == null || cb_CollorPalletUsed.SelectedIndex != avaliablePallets.IndexOf(PalletInUse)))
        {
            cb_CollorPalletUsed.SelectedIndex = 0;
            PalletInUse = avaliablePallets[0];
        }
    }

    private void bt_AutoDetect_Click(object sender, EventArgs e)
    {
        fillResolutionBoxes();
        setPreviewSize();
    }

    private async void bt_Generate_Click(object sender, EventArgs e)
    {
        bt_Generate.Enabled = false;
        try
        {
            await beginGeneratorAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Generation failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            bt_Generate.Enabled = true;
        }
    }

    private void bt_Exit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void bt_Resize_Click(object sender, EventArgs e)
    {
        setPreviewSize();
        updateMSList();
    }

    private void tb_xRes_Leave(object sender, EventArgs e)
    {
        if (!int.TryParse(tb_xRes.Text, out _))
        {
            tb_xRes.Text = WinFormUtils.GrabXRes().ToString();
            MessageBox.Show("This is a number only field");
            return;
        }
        setPreviewSize();
        updateMSList();
    }

    private void tb_yRes_Leave(object sender, EventArgs e)
    {
        if (!int.TryParse(tb_yRes.Text, out _))
        {
            tb_yRes.Text = WinFormUtils.GrabYRes().ToString();
            MessageBox.Show("This is a number only field");
            return;
        }
        setPreviewSize();
        updateMSList();
    }

    private void bt_SaveOutput_Click(object sender, EventArgs e)
    {
        SaveOutput();
    }

    private void bt_Settings_Click(object sender, EventArgs e)
    {
        var settings = new SettingsPannel();
        settings.ShowDialog();
    }

    private void bt_Colors_Click(object sender, EventArgs e)
    {
        var colors = new ColorPicker(avaliablePallets);
        colors.ShowDialog();
        avaliablePallets = colors.ExistingPallets;
        updatePalletUsed();
    }

    private void cb_CollorPalletUsed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_CollorPalletUsed.SelectedIndex >= 0 && cb_CollorPalletUsed.SelectedIndex < avaliablePallets.Count)
            PalletInUse = avaliablePallets[cb_CollorPalletUsed.SelectedIndex];
    }

    // Form Functions
    private void updatePalletUsed()
    {
        updateAvaliablePallets();
        if (PalletInUse == null || !avaliablePallets.Contains(PalletInUse))
        {
            if (avaliablePallets.Count > 0)
            {
                PalletInUse = avaliablePallets[0];
                cb_CollorPalletUsed.SelectedIndex = 0;
            }
        }
        else if (cb_CollorPalletUsed.SelectedIndex >= 0)
        {
            PalletInUse = avaliablePallets[cb_CollorPalletUsed.SelectedIndex];
        }
    }

    private void updateAvaliablePallets()
    {
        cb_CollorPalletUsed.Items.Clear();
        foreach (Pallet item in avaliablePallets)
        {
            cb_CollorPalletUsed.Items.Add(item.Name);
        }
    }

    private void initFormElements()
    {
        fillResolutionBoxes();
        setPreviewSize();
        updateMSList();
        Size size = new Size(xRes / 2, yRes / 2);
        pb_Preview.MaximumSize = size;
        avaliablePallets = Utilities.UnpackUserColorPallets(Properties.Settings.Default.UserColorPallets);
        if (File.Exists(Path.Combine("Resources", "ColorPallets.json")))
        {
            avaliablePallets.AddRange(Utilities.UnpackExternalColorPallets());
        }
        updatePalletUsed();
        if (cb_CollorPalletUsed.Items.Count > 0)
            cb_CollorPalletUsed.SelectedIndex = 0;
    }

    private void fillResolutionBoxes()
    {
        tb_xRes.Text = WinFormUtils.GrabXRes().ToString();
        tb_yRes.Text = WinFormUtils.GrabYRes().ToString();
    }

    private void setPreviewSize()
    {
        if (int.TryParse(tb_xRes.Text, out int x) && int.TryParse(tb_yRes.Text, out int y))
        {
            xRes = x;
            yRes = y;
        }
        pb_Preview.Width = xRes / 2;
        pb_Preview.Height = yRes / 2;
    }

    private async Task beginGeneratorAsync()
    {
        finishedArt?.Dispose();
        finishedArt = null;
        _skBitmap?.Dispose();
        _skBitmap = null;

        int MSLevel = cb_MSLevel.SelectedIndex;
        setPreviewSize();

        if (PalletInUse == null) return;

        _skBitmap = await Task.Run(() =>
        {
            using var gen = new Generator(PalletInUse, xRes, yRes, MSLevel);
            return gen.Generate(Properties.Settings.Default.Seed);
        });

        finishedArt = WinFormUtils.SKBitmapToWinFormsBitmap(_skBitmap);
        pb_Preview.Image = finishedArt;
    }

    private void SaveOutput()
    {
        if (_skBitmap == null)
        {
            MessageBox.Show("There is nothing to save yet!", "Nothing to save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        using var saveFileDialog1 = new SaveFileDialog();
        saveFileDialog1.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp";
        saveFileDialog1.Title = "Save an Image File";

        if (saveFileDialog1.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(saveFileDialog1.FileName))
            return;

        using var image = SKImage.FromBitmap(_skBitmap);
        var format = saveFileDialog1.FilterIndex switch
        {
            1 => SKEncodedImageFormat.Png,
            2 => SKEncodedImageFormat.Jpeg,
            3 => SKEncodedImageFormat.Bmp,
            _ => SKEncodedImageFormat.Png
        };

        using var data = image.Encode(format, 95);
        using var fs = File.OpenWrite(saveFileDialog1.FileName);
        data.SaveTo(fs);
    }

    private void updateMSList()
    {
        int prevSelectedIndex = cb_MSLevel.SelectedIndex;
        cb_MSLevel.Items.Clear();
        double maxSize = Math.Pow(2, 32);
        for (int i = 0; i <= 32; i++)
        {
            double resSize = (double)xRes * i * yRes * i * 4;
            if (resSize < maxSize && resSize >= 0)
            {
                cb_MSLevel.Items.Add(i);
            }
            else
            {
                break;
            }
        }
        if (prevSelectedIndex >= cb_MSLevel.Items.Count)
        {
            cb_MSLevel.SelectedIndex = cb_MSLevel.Items.Count - 1;
        }
        else if (prevSelectedIndex > 0)
        {
            cb_MSLevel.SelectedIndex = prevSelectedIndex;
        }
        else if (cb_MSLevel.Items.Count > 0)
        {
            cb_MSLevel.SelectedIndex = 0;
        }
    }
}
