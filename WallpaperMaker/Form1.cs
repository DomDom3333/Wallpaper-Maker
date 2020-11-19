using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperMaker.Classes;
using static Utils;
namespace WallpaperMaker
{
    public partial class frm_Main : Form
    {
        internal List<Pallet> avaliablePallets { get; set; }
        internal Bitmap finishedArt { get; set; }
        public int xRes { get; set; }
        public int yRes { get; set; }
        public frm_Main()
        {
            InitializeComponent();
            initFormElements();
        }
        //UI EVENTS ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void bt_AutoDetect_Click(object sender, EventArgs e)
        {
            fillResolutionBoxes();
            setPreviewSize();
        }
        private void bt_Generate_Click(object sender, EventArgs e)
        {
            beginGenerator();
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
            setPreviewSize();
            updateMSList();
        }
        private void tb_yRes_Leave(object sender, EventArgs e)
        {
            setPreviewSize();
            updateMSList();
        }
        private void bt_SaveOutput_Click(object sender, EventArgs e)
        {
            SaveOutput();
        }
        //Form Functions ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void fillResolutionBoxes()
        {
            tb_xRes.Text = grabXRes().ToString();
            tb_yRes.Text = grabYRes().ToString();
        }
        private void initFormElements()
        {
            fillResolutionBoxes();
            setPreviewSize();
            updateMSList();
            Size size = new Size(xRes/2, yRes/2);
            pb_Preview.MaximumSize = size;
        }
        private void setPreviewSize()
        {
            xRes = Int32.Parse(tb_xRes.Text);
            yRes = Int32.Parse(tb_yRes.Text);
            pb_Preview.Width = xRes/2;
            pb_Preview.Height = yRes/2;
        }
        private void beginGenerator()
        {
            int MSLevel = cb_MSLevel.SelectedIndex;
            avaliablePallets = UnpackInternalColorPallets();
            setPreviewSize();
            Generator Gen = new Generator(RandomPalletFromList(avaliablePallets), xRes,yRes, MSLevel);

            finishedArt = Gen.Generate();

            pb_Preview.Image = finishedArt;
        }

        private void SaveOutput()
        {
            string outpuFile = "test.jpeg";
            string outputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullOutput = Path.Combine(outputPath, outpuFile);
            finishedArt.Save(fullOutput, ImageFormat.Jpeg);
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
