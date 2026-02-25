namespace WallpaperMaker.WinForm;

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
        toggleRowEnables(1, cb_isEnabledRecs.Checked);
    }

    private void cb_isEnabledSquares_CheckedChanged(object sender, EventArgs e)
    {
        toggleRowEnables(2, cb_isEnabledSquares.Checked);
    }

    private void cb_isEnabledEllis_CheckedChanged(object sender, EventArgs e)
    {
        toggleRowEnables(3, cb_isEnabledEllis.Checked);
    }

    private void cb_isEnabledCircles_CheckedChanged(object sender, EventArgs e)
    {
        toggleRowEnables(4, cb_isEnabledCircles.Checked);
    }

    private void bt_Done_Click(object sender, EventArgs e)
    {
        if (!cb_isEnabledRecs.Checked && !cb_isEnabledSquares.Checked && !cb_isEnabledEllis.Checked && !cb_isEnabledCircles.Checked)
        {
            MessageBox.Show("At least 1 Element needs to be Enabled", "Not enough elements Enabled", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            Properties.Settings.Default.Seed = tb_ResultSeed.Text;
            this.Close();
        }
    }

    private void bt_Cancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    // Seed management
    private void fillSeedTextbox(string input)
    {
        tb_ResultSeed.Text = input;
    }

    private string buildSeed()
    {
        return $"{collectAmountSliderData()}99999{collectSizeSliderData()}9999999999";
    }

    private void loadFromSeed(string seed)
    {
        if (seed.Length < 27) return;

        // Each shape: amount at seed[i], size at seed[9 + i*2]
        loadRowFromSeed(seed[0], seed[9], 1);
        loadRowFromSeed(seed[1], seed[11], 2);
        loadRowFromSeed(seed[2], seed[13], 3);
        loadRowFromSeed(seed[3], seed[15], 4);
    }

    private void loadRowFromSeed(char amountChar, char sizeChar, int row)
    {
        int amount = amountChar - '0';
        int size = sizeChar - '0';
        bool enabled = amount > 0;

        var (checkBox, amountBar, sizeBar) = GetRowControls(row);
        if (checkBox == null || amountBar == null || sizeBar == null) return;

        checkBox.Checked = enabled;
        amountBar.Value = enabled ? Math.Clamp(amount, 1, 9) : 1;
        sizeBar.Value = Math.Clamp(size, 1, 9);
        toggleRowEnables(row, enabled);
    }

    private (CheckBox? cb, TrackBar? amount, TrackBar? size) GetRowControls(int row) => row switch
    {
        1 => (cb_isEnabledRecs, tb_AmoutRecs, tb_SizeRecs),
        2 => (cb_isEnabledSquares, tb_AmoutSquares, tb_SizeSquares),
        3 => (cb_isEnabledEllis, tb_AmoutEllies, tb_SizeEllies),
        4 => (cb_isEnabledCircles, tb_AmoutCircles, tb_SizeCircles),
        _ => (null, null, null)
    };

    private string collectAmountSliderData()
    {
        return string.Concat(
            cb_isEnabledRecs.Checked ? tb_AmoutRecs.Value : 0,
            cb_isEnabledSquares.Checked ? tb_AmoutSquares.Value : 0,
            cb_isEnabledEllis.Checked ? tb_AmoutEllies.Value : 0,
            cb_isEnabledCircles.Checked ? tb_AmoutCircles.Value : 0);
    }

    private string collectSizeSliderData()
    {
        static string sizeFor(bool enabled, TrackBar bar) =>
            enabled ? $"{bar.Value}{bar.Value}" : "00";

        return string.Concat(
            sizeFor(cb_isEnabledRecs.Checked, tb_SizeRecs),
            sizeFor(cb_isEnabledSquares.Checked, tb_SizeSquares),
            sizeFor(cb_isEnabledEllis.Checked, tb_SizeEllies),
            sizeFor(cb_isEnabledCircles.Checked, tb_SizeCircles));
    }

    private void toggleRowEnables(int row, bool isEnabled)
    {
        var (_, amountBar, sizeBar) = GetRowControls(row);
        if (amountBar == null || sizeBar == null) return;

        // Enable/disable the corresponding labels and sliders
        switch (row)
        {
            case 1:
                lbl_AmoutRecs.Enabled = isEnabled;
                lbl_SizeRecs.Enabled = isEnabled;
                break;
            case 2:
                lbl_AmoutSquares.Enabled = isEnabled;
                lbl_SizeSquares.Enabled = isEnabled;
                break;
            case 3:
                lbl_AmountEllies.Enabled = isEnabled;
                lbl_SizeEllies.Enabled = isEnabled;
                break;
            case 4:
                lbl_AmountCircles.Enabled = isEnabled;
                lbl_SizeCircles.Enabled = isEnabled;
                break;
        }
        amountBar.Enabled = isEnabled;
        sizeBar.Enabled = isEnabled;

        fillSeedTextbox(buildSeed());
    }
}
