using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperMaker.Domain;
using static WallpaperMaker.WinForm.WinFormUtils;

namespace WallpaperMaker.WinForm
{
    public partial class frm_Main : Form
    {
        internal List<Pallet> avaliablePallets { get; set; }
        internal Pallet PalletInUse { get; set; }
        internal Bitmap finishedArt { get; set; }
        public int xRes { get; set; }
        public int yRes { get; set; }
        public frm_Main()
        {
            InitializeComponent();
            initFormElements();
        }
        //UI EVENTS ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void frm_Main_Load(object sender, EventArgs e)
        {
            if(cb_CollorPalletUsed.SelectedIndex != avaliablePallets.IndexOf(PalletInUse))
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

        private void bt_Generate_Click(object sender, EventArgs e)
        {
            bt_Generate.Enabled = false;
            GC.Collect();//is stupid but needs to happen or it will swallow ALL Ram and slow down to hell
            try
            {
                beginGeneratorAsync();
            }
            catch
            {

            }
            bt_Generate.Enabled = true;
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
            if (!int.TryParse(tb_xRes.Text, out int parsedValue))
            {
                tb_xRes.Text = grabXRes().ToString();
                MessageBox.Show("This is a number only field");
                return;
            }
            setPreviewSize();
            updateMSList();
        }

        private void tb_yRes_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_yRes.Text, out int parsedValue))
            {
                tb_yRes.Text = grabYRes().ToString();
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
            SettingsPannel settings = new SettingsPannel();
            settings.Show();
        }

        private void bt_Colors_Click(object sender, EventArgs e)
        {
            ColorPicker colors = new ColorPicker(avaliablePallets);
            var uselessVar = colors.ShowDialog();
            avaliablePallets = colors.ExistingPallets;
            updatePalletUsed();
        }

        private void cb_CollorPalletUsed_SelectedIndexChanged(object sender, EventArgs e)
        {
            PalletInUse = avaliablePallets[cb_CollorPalletUsed.SelectedIndex];
        }

        //Form Functions ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void updatePalletUsed()
        {
            updateAvaliablePallets();
            if (PalletInUse == null || !avaliablePallets.Contains(PalletInUse))
            {
                PalletInUse = avaliablePallets[0];
                cb_CollorPalletUsed.SelectedIndex = 0;
            }
            else if(cb_CollorPalletUsed.SelectedIndex > 0)
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
            Size size = new Size(xRes/2, yRes/2);
            pb_Preview.MaximumSize = size;
            avaliablePallets = Utilities.UnpackUserColorPallets(Properties.Settings.Default.UserColorPallets);
            if (File.Exists(Path.Combine("Resources", "ColorPallets.json")))
            {
                avaliablePallets.AddRange(Utilities.UnpackExternalColorPallets());
            }
            updatePalletUsed();
            cb_CollorPalletUsed.SelectedIndex = 0;
        }

        private void fillResolutionBoxes()
        {
            tb_xRes.Text = grabXRes().ToString();
            tb_yRes.Text = grabYRes().ToString();
        }

        private void setPreviewSize()
        {
            xRes = Int32.Parse(tb_xRes.Text);
            yRes = Int32.Parse(tb_yRes.Text);
            pb_Preview.Width = xRes/2;
            pb_Preview.Height = yRes/2;
        }

        private async Task beginGeneratorAsync()
        {
            finishedArt = null;
            int MSLevel = cb_MSLevel.SelectedIndex;
            setPreviewSize();
            Generator Gen = new Generator(PalletInUse, xRes,yRes, MSLevel);

            finishedArt = await Task.Run(() => Gen.Generate(Properties.Settings.Default.Seed));

            pb_Preview.Image = finishedArt;
        }

        private void SaveOutput()
        {

            if (finishedArt == null)
            {
                MessageBox.Show("There is nothing to save yet!!!", "Nothing to save!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        finishedArt.Save(fs, ImageFormat.Jpeg);
                        break;

                    case 2:
                        finishedArt.Save(fs, ImageFormat.Bmp);
                        break;
                }

                fs.Close();
            }
            //string outpuFile = "test.jpeg";
            //string outputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string fullOutput = Path.Combine(outputPath, outpuFile);
            //finishedArt.Save(fullOutput, ImageFormat.Jpeg);
            
        }

        private void updateMSList()
        {
            int prevSelectedIndex = cb_MSLevel.SelectedIndex;
            cb_MSLevel.Items.Clear();
            double ResSize = 0;
            double MaxSize = Math.Pow(2, 32);
            for (int i = 0; i <= 32; i++)
            {
                ResSize = xRes * i * yRes * i * 4;
                if (ResSize < MaxSize && ResSize >= 0)
                {
                    cb_MSLevel.Items.Add(i);
                }
                else
                {
                    break;
                }
            }
            if(prevSelectedIndex >= cb_MSLevel.Items.Count)
            {
                cb_MSLevel.SelectedIndex = cb_MSLevel.Items.Count-1;
            }
            else if(prevSelectedIndex > 0)
            {
                cb_MSLevel.SelectedIndex = prevSelectedIndex;
            }
            else
            {
                cb_MSLevel.SelectedIndex = 0;
            }
        }
    }
}
