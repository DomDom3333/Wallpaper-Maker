using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperMaker.Domain;

namespace WallpaperMaker.WinForm
{
    public partial class ColorPicker : Form
    {

        internal List<Pallet> ExistingPallets { get; private set; }
        private Pallet selectedPallet { get; set; }
        private ListViewItem selectedColor { get; set; }

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
            selectedPallet = findPalletByName(lb_PalletsAvaliable.SelectedItem.ToString());
            FillColorList();
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
            if (ExistingPallets.Count < 1)
            {                
                MessageBox.Show("There needs to be at least 1 Pallet with 1 Color!","Not Enough Pallets!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            foreach (Pallet item in ExistingPallets)
            {
                if(item.Colors.Count < 1)
                {
                    MessageBox.Show("There needs to be at least 1 Color in each Pallet!", "Not Enough Colors!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Properties.Settings.Default.UserColorPallets = Utilities.PackUpUserColorPallets(ExistingPallets);
            this.Close();
        }
        private void bt_DeleteColor_Click(object sender, EventArgs e)
        {
            deleteColor();
            refreshLists();
        }
        private void bt_DeletePallet_Click(object sender, EventArgs e)
        {
            deletePallet();
            refreshLists();
        }



        //################################################################################################################################################################################################################



        private void initForm()
        {
            FillPalletList();
            lb_PalletsAvaliable.SelectedIndex = 0;
            selectedPallet = ExistingPallets[0];
            FillColorList();
        }
        private Pallet findPalletByName(string Name)
        {
            foreach (Pallet item in ExistingPallets)
            {
                if (item.Name == Name)
                {
                    return item;
                }
            }
            return null;
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
            Color finColor = new Color();
            foreach (List<int> col in selectedPallet.Colors)
            {
                finColor = Color.FromArgb(255, col[0], col[1], col[2]);
                ListViewItem lv;
                if (col.Count == 4)
                {
                    lv = new ListViewItem($"{col[1]},{col[2]},{col[3]}");
                }
                else
                {
                    lv = new ListViewItem($"{col[0]},{col[1]},{col[2]}");
                }
                lv_ColorsInPallet.Items.Add(lv);
            }
            for (int i = 0; i < lv_ColorsInPallet.Items.Count; i++)
            {
                string[] vals = lv_ColorsInPallet.Items[i].Text.Split(',');
                int val1 = Int16.Parse(vals[0]);
                int val2 = Int16.Parse(vals[1]);
                int val3 = Int16.Parse(vals[2]);             
                lv_ColorsInPallet.Items[i].BackColor = Color.FromArgb(255, val1, val2, val3);
            }
        }

        private void AddColor()
        {
            ColorDialog cd = new ColorDialog();
            DialogResult result = cd.ShowDialog();
            Color newColor;
            if(result == DialogResult.OK)
            {
                newColor = cd.Color;
                //newColor = Color.FromArgb(255, newColor.R, newColor.G, newColor.B);
                pb_ColorPreview.BackColor = newColor;
            }
            else
            {
                return;
            }
            ListViewItem lv = new ListViewItem($"{newColor.R},{newColor.G},{newColor.B}");
            lv_ColorsInPallet.Items.Add(lv);
            selectedPallet.addColor(newColor);
        }
        private void AddPallet()
        {
            if(findPalletByName(tb_newPalletName.Text) != null)
            {
                MessageBox.Show("This Pallet name is already taken. Please choose another","InvalidName",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string[] colors = new string[0];
            ExistingPallets.Add(new Pallet(tb_newPalletName.Text, colors));
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
            lb_PalletsAvaliable.SelectedIndex = findInListBox(selectedPallet.Name);
        }
        private int findInListBox(string Name)
        {
            for (int i = 0; i < lb_PalletsAvaliable.Items.Count; i++)
            {
                if (lb_PalletsAvaliable.Items[i].ToString() == Name)
                {
                    return i;
                }
            }
            return 0;
        }
        private void deleteColor()
        {
            DialogResult results = MessageBox.Show("WAIT!!!!\nAre you sure? This can NOT be undone", "ARE YOU SURE?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (results == DialogResult.OK)
            {
                lv_ColorsInPallet.Items.Remove(selectedColor);
            }
            else
            {
                return;
            }
        }
        private void deletePallet()
        {
            DialogResult results = MessageBox.Show("WAIT!!!!\nAre you sure? This can NOT be undone","ARE YOU SURE?",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            if(results == DialogResult.OK)
            {
                ExistingPallets.Remove(selectedPallet);
            }
            else
            {
                return;
            }
        }
        private void updatePreview()
        {
            ListView.SelectedIndexCollection indices = lv_ColorsInPallet.SelectedIndices;
            if (indices.Count > 0)
            {
                string[] color = lv_ColorsInPallet.Items[indices[0]].Text.Split(',');
                pb_ColorPreview.BackColor = Color.FromArgb(255, Int16.Parse(color[0]), Int16.Parse(color[1]), Int16.Parse(color[2]));
                lv_ColorsInPallet.SelectedIndices.Clear();
            }
        }

        private void ColorPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ExistingPallets.Count < 1)
            {
                MessageBox.Show("There needs to be at least 1 Pallet with 1 Color!", "Not Enough Pallets!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            foreach (Pallet item in ExistingPallets)
            {
                if (item.Colors.Count < 1)
                {
                    MessageBox.Show("There needs to be at least 1 Color in each Pallet!", "Not Enough Colors!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            Properties.Settings.Default.UserColorPallets = Utilities.PackUpUserColorPallets(ExistingPallets);
        }
    }
}
