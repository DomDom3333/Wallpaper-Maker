using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.WinForm;

public partial class ColorPicker : Form
{
    internal List<Pallet> ExistingPallets { get; private set; }
    private Pallet? selectedPallet { get; set; }
    private ListViewItem? selectedColor { get; set; }

    internal ColorPicker(List<Pallet> PalletList)
    {
        InitializeComponent();
        ExistingPallets = PalletList;
    }

    private void ColorPicker_Load(object sender, EventArgs e)
    {
        initForm();
    }

    private void lb_PalletsAvaliable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lb_PalletsAvaliable.SelectedItem != null)
        {
            selectedPallet = findPalletByName(lb_PalletsAvaliable.SelectedItem.ToString()!);
            FillColorList();
        }
    }

    private void bt_newPallet_Click(object sender, EventArgs e)
    {
        AddPallet();
        refreshLists();
    }

    private void bt_addColor_Click(object sender, EventArgs e)
    {
        AddColor();
        refreshLists();
    }

    private void lv_ColorsInPallet_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListView.SelectedIndexCollection indices = lv_ColorsInPallet.SelectedIndices;
        if (indices.Count > 0)
        {
            selectedColor = lv_ColorsInPallet.Items[indices[0]];
            updatePreview();
        }
    }

    private void bt_Done_Click(object sender, EventArgs e)
    {
        if (!ValidatePallets()) return;
        SaveAndClose();
    }

    private void bt_DeleteColor_Click(object sender, EventArgs e)
    {
        if (selectedColor != null && lv_ColorsInPallet.Items.Contains(selectedColor))
        {
            deleteColor();
            refreshLists();
        }
        else
        {
            MessageBox.Show("Select a color to delete", "Select Color First!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void bt_DeletePallet_Click(object sender, EventArgs e)
    {
        if (selectedPallet != null && lb_PalletsAvaliable.Items.Contains(selectedPallet.Name))
        {
            deletePallet();
            refreshLists();
        }
        else
        {
            MessageBox.Show("Select a Pallet to delete", "Select Pallet First!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void bt_RenamePallet_Click(object sender, EventArgs e)
    {
        if (selectedPallet != null && lb_PalletsAvaliable.Items.Contains(selectedPallet.Name))
        {
            RenamePallet();
            refreshLists();
        }
        else
        {
            MessageBox.Show("Select a Pallet to rename", "Select Pallet First!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // Internal methods
    private void initForm()
    {
        if (ExistingPallets.Count == 0) return;
        FillPalletList();
        lb_PalletsAvaliable.SelectedIndex = 0;
        selectedPallet = ExistingPallets[0];
        FillColorList();
    }

    private Pallet? findPalletByName(string name)
    {
        return ExistingPallets.FirstOrDefault(p => p.Name == name);
    }

    private void FillPalletList()
    {
        lb_PalletsAvaliable.Items.Clear();
        foreach (Pallet item in ExistingPallets)
        {
            lb_PalletsAvaliable.Items.Add(item.Name);
        }
    }

    private void FillColorList()
    {
        lv_ColorsInPallet.Clear();
        if (selectedPallet == null) return;

        foreach (SKColor col in selectedPallet.Colors)
        {
            var lv = new ListViewItem($"{col.Red},{col.Green},{col.Blue}");
            lv.BackColor = WinFormUtils.SKColorToDrawingColor(col);
            lv_ColorsInPallet.Items.Add(lv);
        }
    }

    private void AddColor()
    {
        if (selectedPallet == null) return;

        using var cd = new ColorDialog();
        DialogResult result = cd.ShowDialog();
        if (result != DialogResult.OK) return;

        Color newColor = cd.Color;
        pb_ColorPreview.BackColor = newColor;

        var skColor = WinFormUtils.DrawingColorToSKColor(newColor);
        selectedPallet.AddColor(skColor);

        var lv = new ListViewItem($"{newColor.R},{newColor.G},{newColor.B}");
        lv.BackColor = newColor;
        lv_ColorsInPallet.Items.Add(lv);
    }

    private void AddPallet()
    {
        if (findPalletByName(tb_newPalletName.Text) != null)
        {
            MessageBox.Show("This Pallet name is already taken. Please choose another", "InvalidName", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        ExistingPallets.Add(new Pallet(tb_newPalletName.Text, Array.Empty<string>()));
    }

    private void refreshLists()
    {
        FillPalletList();
        FillColorList();
        if (ExistingPallets.Count < 1)
        {
            lv_ColorsInPallet.Refresh();
            return;
        }
        if (selectedPallet != null)
            lb_PalletsAvaliable.SelectedIndex = findInListBox(selectedPallet.Name);
    }

    private int findInListBox(string name)
    {
        for (int i = 0; i < lb_PalletsAvaliable.Items.Count; i++)
        {
            if (lb_PalletsAvaliable.Items[i].ToString() == name)
                return i;
        }
        return 0;
    }

    private void deleteColor()
    {
        if (selectedPallet == null || selectedColor == null) return;

        DialogResult results = MessageBox.Show("Are you sure? This cannot be undone.", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (results != DialogResult.OK) return;

        string[] parts = selectedColor.Text.Split(',');
        if (parts.Length >= 3)
        {
            byte r = byte.Parse(parts[0]);
            byte g = byte.Parse(parts[1]);
            byte b = byte.Parse(parts[2]);
            var colorToRemove = new SKColor(r, g, b);
            selectedPallet.RemoveColor(colorToRemove);
        }
        lv_ColorsInPallet.Items.Remove(selectedColor);
    }

    private void deletePallet()
    {
        DialogResult results = MessageBox.Show("Are you sure? This cannot be undone.", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (results == DialogResult.OK && selectedPallet != null)
        {
            ExistingPallets.Remove(selectedPallet);
            selectedPallet = ExistingPallets.FirstOrDefault();
        }
    }

    private void updatePreview()
    {
        ListView.SelectedIndexCollection indices = lv_ColorsInPallet.SelectedIndices;
        if (indices.Count > 0)
        {
            string[] color = lv_ColorsInPallet.Items[indices[0]].Text.Split(',');
            if (color.Length >= 3)
                pb_ColorPreview.BackColor = Color.FromArgb(255, int.Parse(color[0]), int.Parse(color[1]), int.Parse(color[2]));
            lv_ColorsInPallet.SelectedIndices.Clear();
        }
    }

    private void ColorPicker_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!ValidatePallets())
        {
            e.Cancel = true;
            return;
        }
        Properties.Settings.Default.UserColorPallets = Utilities.PackUpUserColorPallets(ExistingPallets);
        Properties.Settings.Default.Save();
    }

    private bool ValidatePallets()
    {
        if (ExistingPallets.Count < 1)
        {
            MessageBox.Show("There needs to be at least 1 Pallet with 1 Color!", "Not Enough Pallets!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        foreach (Pallet item in ExistingPallets)
        {
            if (item.Colors.Count < 1)
            {
                MessageBox.Show("There needs to be at least 1 Color in each Pallet!", "Not Enough Colors!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        return true;
    }

    private void SaveAndClose()
    {
        Properties.Settings.Default.UserColorPallets = Utilities.PackUpUserColorPallets(ExistingPallets);
        Properties.Settings.Default.Save();
        this.Close();
    }

    private void RenamePallet()
    {
        if (findPalletByName(tb_RenamedPallet.Text) != null)
        {
            MessageBox.Show("This Pallet name is already taken. Please choose another", "InvalidName", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        selectedPallet?.Rename(tb_RenamedPallet.Text);
    }
}
