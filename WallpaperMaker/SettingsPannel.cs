using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperMaker
{
    public partial class SettingsPannel : Form
    {
        public SettingsPannel()
        {
            InitializeComponent();
        }

        private void SettingsPannel_Load(object sender, EventArgs e)
        {
            fillSeedTextbox(Properties.Settings.Default.Seed);
            loadFromSeed(Properties.Settings.Default.Seed);
        }

        private void AnyBar_Scroll(object sender, EventArgs e)
        {
            fillSeedTextbox(buildSeed());
        }
        private void cb_isEnabledRecs_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_isEnabledRecs.Checked)
            {
                toggleRowEnables(1, true);
            }
            else
            {
                toggleRowEnables(1, false);
            }
        }

        private void cb_isEnabledSquares_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_isEnabledSquares.Checked)
            {
                toggleRowEnables(2, true);
            }
            else
            {
                toggleRowEnables(2, false);
            }
        }

        private void cb_isEnabledEllis_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_isEnabledEllis.Checked)
            {
                toggleRowEnables(3, true);
            }
            else
            {
                toggleRowEnables(3, false);
            }
        }

        private void cb_isEnabledCircles_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_isEnabledCircles.Checked)
            {
                toggleRowEnables(4, true);
            }
            else
            {
                toggleRowEnables(4, false);
            }
        }
        private void bt_Done_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Seed = tb_ResultSeed.Text;
            this.Close();
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //##########################################################################################################################################################################################################################################################################################################################################################################################################################################

        private void fillSeedTextbox(string input)
        {
            tb_ResultSeed.Text = input;
        }
        private string buildSeed()
        {
            string seed = $"{collectAmountSliderData()}99999{collectSizeSliderData()}9999999999";
            return seed;
        }

        private void loadFromSeed(string seed) //NO! PLEASE NO! DEAR GOD NO! WHY WOULD OYU DO IT LIKE THAT! YOU MONSTER! I HATE YOU!
        {
            loadRowFromSeed(seed[0], seed[9], 1);
            loadRowFromSeed(seed[1], seed[11], 2);
            loadRowFromSeed(seed[2], seed[13], 3);
            loadRowFromSeed(seed[3], seed[15], 4);

            //For Future Shapes
            //loadRowFromSeed(seed[4], seed[17], 5);
            //loadRowFromSeed(seed[5], seed[19], 6);
            //loadRowFromSeed(seed[6], seed[21], 7);
            //loadRowFromSeed(seed[7], seed[23], 8);
            //loadRowFromSeed(seed[8], seed[25], 9);

        }

        private void loadRowFromSeed(char value1, char value2, int rowToLoad) //CONTINIUATION OF DISPAIR
        {
            if(rowToLoad == 1)
            {
                if(value1 == '0')
                {
                    cb_isEnabledRecs.Checked = false;
                    tb_AmoutRecs.Value = 1;
                    tb_SizeRecs.Value = 1;
                    toggleRowEnables(rowToLoad, false);
                }
                else
                {
                    cb_isEnabledRecs.Checked = true;
                    tb_AmoutRecs.Value = Int16.Parse(value1.ToString());
                    tb_SizeRecs.Value = Int16.Parse(value2.ToString());
                    toggleRowEnables(rowToLoad, true);

                }
            }
            else if (rowToLoad == 2)
            {
                if (value1 == '0')
                {
                    cb_isEnabledSquares.Checked = false;
                    tb_AmoutSquares.Value = 1;
                    tb_SizeSquares.Value = 1;
                    toggleRowEnables(rowToLoad, false);
                }
                else
                {
                    cb_isEnabledSquares.Checked = true;
                    toggleRowEnables(rowToLoad, true);
                    tb_AmoutSquares.Value = Int16.Parse(value1.ToString());
                    tb_SizeSquares.Value = Int16.Parse(value2.ToString());
                }
            }
            else if (rowToLoad == 3)
            {
                if (value1 == '0')
                {
                    cb_isEnabledEllis.Checked = false;                   
                    tb_AmoutEllies.Value = 1;
                    tb_SizeEllies.Value = 1;
                    toggleRowEnables(rowToLoad, false);
                }
                else
                {
                    cb_isEnabledEllis.Checked = true;
                    toggleRowEnables(rowToLoad, true);
                    tb_AmoutEllies.Value = Int16.Parse(value1.ToString());
                    tb_SizeEllies.Value = Int16.Parse(value2.ToString());
                }
            }
            else if (rowToLoad == 4)
            {
                if (value1 == '0')
                {
                    cb_isEnabledCircles.Checked = false;
                    tb_AmoutCircles.Value = 1;
                    tb_SizeCircles.Value = 1;
                    toggleRowEnables(rowToLoad, false);
                }
                else
                {
                    cb_isEnabledCircles.Checked = true;
                    toggleRowEnables(rowToLoad, true);
                    tb_AmoutCircles.Value = Int16.Parse(value1.ToString());
                    tb_SizeCircles.Value = Int16.Parse(value2.ToString());
                }
            }
        }

        private string collectAmountSliderData()
        {
            string output = "";
            if (cb_isEnabledRecs.Checked)
            {
                output = $"{output}{tb_AmoutRecs.Value}";
            }
            else
            {
                output = $"{output}{0}";
            }

            if (cb_isEnabledSquares.Checked)
            {
                output = $"{output}{tb_AmoutSquares.Value}";
            }
            else
            {
                output = $"{output}{0}";
            }

            if (cb_isEnabledEllis.Checked)
            {
                output = $"{output}{tb_AmoutEllies.Value}";
            }
            else
            {
                output = $"{output}{0}";
            }

            if (cb_isEnabledCircles.Checked)
            {
                output = $"{output}{tb_AmoutCircles.Value}";
            }
            else
            {
                output = $"{output}{0}";
            }

            return output;
        }

        private string collectSizeSliderData()
        {
            string output = "";
            if (cb_isEnabledRecs.Checked)
            {
                output = $"{output}{tb_SizeRecs.Value}{tb_SizeRecs.Value}";
            }
            else
            {
                output = $"{output}{0}{0}";
            }

            if (cb_isEnabledSquares.Checked)
            {
                output = $"{output}{tb_SizeSquares.Value}{tb_SizeSquares.Value}";
            }
            else
            {
                output = $"{output}{0}{0}";
            }

            if (cb_isEnabledEllis.Checked)
            {
                output = $"{output}{tb_SizeEllies.Value}{tb_SizeEllies.Value}";
            }
            else
            {
                output = $"{output}{0}{0}";
            }

            if (cb_isEnabledCircles.Checked)
            {
                output = $"{output}{tb_SizeCircles.Value}{tb_SizeCircles.Value}";
            }
            else
            {
                output = $"{output}{0}{0}";
            }

            return output;
        }

        private void toggleRowEnables(int row, bool isEnabled)
        {
            if (isEnabled)
            {
                switch (row)
                {
                    case 1:
                        lbl_AmoutRecs.Enabled = true;
                        lbl_SizeRecs.Enabled = true;
                        tb_AmoutRecs.Enabled = true;
                        tb_SizeRecs.Enabled = true;
                        break;
                    case 2:
                        lbl_AmoutSquares.Enabled = true;
                        lbl_SizeSquares.Enabled = true;
                        tb_AmoutSquares.Enabled = true;
                        tb_SizeSquares.Enabled = true;
                        break;
                    case 3:
                        lbl_AmountEllies.Enabled = true;
                        lbl_SizeEllies.Enabled = true;
                        tb_AmoutEllies.Enabled = true;
                        tb_SizeEllies.Enabled = true;
                        break;
                    case 4:
                        lbl_AmountCircles.Enabled = true;
                        lbl_SizeCircles.Enabled = true;
                        tb_AmoutCircles.Enabled = true;
                        tb_SizeCircles.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (row)
                {
                    case 1:
                        lbl_AmoutRecs.Enabled = false;
                        lbl_SizeRecs.Enabled = false;
                        tb_AmoutRecs.Enabled = false;
                        tb_SizeRecs.Enabled = false;
                        break;
                    case 2:
                        lbl_AmoutSquares.Enabled = false;
                        lbl_SizeSquares.Enabled = false;
                        tb_AmoutSquares.Enabled = false;
                        tb_SizeSquares.Enabled = false;
                        break;
                    case 3:
                        lbl_AmountEllies.Enabled = false;
                        lbl_SizeEllies.Enabled = false;
                        tb_AmoutEllies.Enabled = false;
                        tb_SizeEllies.Enabled = false;
                        break;
                    case 4:
                        lbl_AmountCircles.Enabled = false;
                        lbl_SizeCircles.Enabled = false;
                        tb_AmoutCircles.Enabled = false;
                        tb_SizeCircles.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
            fillSeedTextbox(buildSeed());
        }


    }
}
