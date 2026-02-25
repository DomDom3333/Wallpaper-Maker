using Avalonia.Controls;
using Avalonia.Interactivity;

namespace WallpaperMaker.Avalonia;

public partial class SettingsWindow : Window
{
    public string? ResultSeed { get; private set; }
    private bool _initialized;

    public SettingsWindow() : this("330999996666001199999999999") { }

    public SettingsWindow(string currentSeed)
    {
        InitializeComponent();
        LoadFromSeed(currentSeed);
        _initialized = true;
        UpdateSeed();
    }

    private void LoadFromSeed(string seed)
    {
        if (seed.Length < 27) return;

        LoadRow(seed[0], seed[9], CbRecs, SlAmountRecs, SlSizeRecs);
        LoadRow(seed[1], seed[11], CbSquares, SlAmountSquares, SlSizeSquares);
        LoadRow(seed[2], seed[13], CbEllipses, SlAmountEllipses, SlSizeEllipses);
        LoadRow(seed[3], seed[15], CbCircles, SlAmountCircles, SlSizeCircles);
    }

    private static void LoadRow(char amountChar, char sizeChar, CheckBox cb, Slider slAmount, Slider slSize)
    {
        int amount = amountChar - '0';
        int size = sizeChar - '0';
        bool enabled = amount > 0;

        cb.IsChecked = enabled;
        slAmount.Value = enabled ? Math.Clamp(amount, 1, 9) : 1;
        slSize.Value = Math.Clamp(size, 1, 9);
        slAmount.IsEnabled = enabled;
        slSize.IsEnabled = enabled;
    }

    private string BuildSeed()
    {
        static string amountFor(CheckBox cb, Slider sl) =>
            cb.IsChecked == true ? ((int)sl.Value).ToString() : "0";

        static string sizeFor(CheckBox cb, Slider sl) =>
            cb.IsChecked == true ? $"{(int)sl.Value}{(int)sl.Value}" : "00";

        string amounts = string.Concat(
            amountFor(CbRecs, SlAmountRecs),
            amountFor(CbSquares, SlAmountSquares),
            amountFor(CbEllipses, SlAmountEllipses),
            amountFor(CbCircles, SlAmountCircles));

        string sizes = string.Concat(
            sizeFor(CbRecs, SlSizeRecs),
            sizeFor(CbSquares, SlSizeSquares),
            sizeFor(CbEllipses, SlSizeEllipses),
            sizeFor(CbCircles, SlSizeCircles));

        // Pad to 27 chars: 4 amounts + 5 future amounts (9) + 4*2 sizes + 5*2 future sizes (9)
        return $"{amounts}99999{sizes}9999999999";
    }

    private void UpdateSeed()
    {
        TbSeed.Text = BuildSeed();
    }

    private void UpdateRowEnabled(CheckBox cb, Slider slAmount, Slider slSize)
    {
        bool enabled = cb.IsChecked == true;
        slAmount.IsEnabled = enabled;
        slSize.IsEnabled = enabled;
    }

    private void AnySlider_Changed(object? sender, global::Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        if (_initialized) UpdateSeed();
    }

    private void AnyCheckbox_Changed(object? sender, RoutedEventArgs e)
    {
        if (!_initialized) return;

        if (sender == CbRecs) UpdateRowEnabled(CbRecs, SlAmountRecs, SlSizeRecs);
        else if (sender == CbSquares) UpdateRowEnabled(CbSquares, SlAmountSquares, SlSizeSquares);
        else if (sender == CbEllipses) UpdateRowEnabled(CbEllipses, SlAmountEllipses, SlSizeEllipses);
        else if (sender == CbCircles) UpdateRowEnabled(CbCircles, SlAmountCircles, SlSizeCircles);

        UpdateSeed();
    }

    private void BtnDone_Click(object? sender, RoutedEventArgs e)
    {
        if (CbRecs.IsChecked != true && CbSquares.IsChecked != true &&
            CbEllipses.IsChecked != true && CbCircles.IsChecked != true)
        {
            // Could show an error dialog, but for simplicity just don't close
            return;
        }

        ResultSeed = BuildSeed();
        Close();
    }

    private void BtnCancel_Click(object? sender, RoutedEventArgs e)
    {
        ResultSeed = null;
        Close();
    }
}
