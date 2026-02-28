using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using WallpaperMaker.Domain;

namespace WallpaperMaker.Avalonia;

public partial class SettingsWindow : Window
{
    public WallpaperConfig? ResultConfig { get; private set; }
    public string? ResultSeed { get; private set; }

    private readonly List<ShapeRow> _rows = new();
    private bool _initialized;

    private class ShapeRow
    {
        public ShapeType Type { get; init; }
        public Slider AmountSlider { get; init; } = null!;
        public Slider SizeSlider { get; init; } = null!;
        public CheckBox EnabledCheckBox { get; init; } = null!;
    }

    public SettingsWindow() : this(WallpaperConfig.CreateDefault()) { }

    public SettingsWindow(string currentSeed) : this(WallpaperConfig.FromSeed(currentSeed)) { }

    public SettingsWindow(WallpaperConfig config)
    {
        InitializeComponent();
        PopulateFillModes();
        PopulateBackgroundModes();
        BuildShapeRows(config);
        LoadConfigSettings(config);
        _initialized = true;
        UpdateSeed();
    }

    private void PopulateFillModes()
    {
        foreach (var mode in Enum.GetValues<FillMode>())
            CbFillMode.Items.Add(FormatEnumName(mode.ToString()));
    }

    private void PopulateBackgroundModes()
    {
        foreach (var mode in Enum.GetValues<BackgroundMode>())
            CbBackgroundMode.Items.Add(FormatEnumName(mode.ToString()));
    }

    private static string FormatEnumName(string name)
    {
        // Insert spaces before capitals: "LinearGradient" -> "Linear Gradient"
        var chars = new List<char>();
        for (int i = 0; i < name.Length; i++)
        {
            if (i > 0 && char.IsUpper(name[i]) && char.IsLower(name[i - 1]))
                chars.Add(' ');
            chars.Add(name[i]);
        }
        return new string(chars.ToArray());
    }

    private void BuildShapeRows(WallpaperConfig config)
    {
        foreach (var shapeConfig in config.Shapes)
        {
            var row = CreateShapeRow(shapeConfig);
            _rows.Add(row);
        }
    }

    private ShapeRow CreateShapeRow(ShapeConfig shapeConfig)
    {
        var amountSlider = new Slider
        {
            Minimum = 1, Maximum = 9,
            Value = shapeConfig.Amount,
            IsSnapToTickEnabled = true,
            TickFrequency = 1,
            IsEnabled = shapeConfig.Enabled,
        };
        amountSlider.ValueChanged += AnySlider_Changed;

        var sizeSlider = new Slider
        {
            Minimum = 1, Maximum = 9,
            Value = shapeConfig.SizeW,
            IsSnapToTickEnabled = true,
            TickFrequency = 1,
            IsEnabled = shapeConfig.Enabled,
        };
        sizeSlider.ValueChanged += AnySlider_Changed;

        var checkBox = new CheckBox
        {
            IsChecked = shapeConfig.Enabled,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        var row = new ShapeRow
        {
            Type = shapeConfig.Type,
            AmountSlider = amountSlider,
            SizeSlider = sizeSlider,
            EnabledCheckBox = checkBox,
        };

        checkBox.IsCheckedChanged += (_, _) =>
        {
            bool enabled = checkBox.IsChecked == true;
            amountSlider.IsEnabled = enabled;
            sizeSlider.IsEnabled = enabled;
            if (_initialized) UpdateSeed();
        };

        var grid = new Grid
        {
            ColumnDefinitions = ColumnDefinitions.Parse("120,*,30,*,65"),
        };

        var label = new TextBlock
        {
            Text = FormatEnumName(shapeConfig.Type.ToString()),
            VerticalAlignment = VerticalAlignment.Center,
        };

        var sizeLabel = new TextBlock
        {
            Text = "Size",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        Grid.SetColumn(label, 0);
        Grid.SetColumn(amountSlider, 1);
        Grid.SetColumn(sizeLabel, 2);
        Grid.SetColumn(sizeSlider, 3);
        Grid.SetColumn(checkBox, 4);

        grid.Children.Add(label);
        grid.Children.Add(amountSlider);
        grid.Children.Add(sizeLabel);
        grid.Children.Add(sizeSlider);
        grid.Children.Add(checkBox);

        ShapeListPanel.Children.Add(grid);

        return row;
    }

    private void LoadConfigSettings(WallpaperConfig config)
    {
        CbFillMode.SelectedIndex = (int)config.ShapeFill;
        CbBackgroundMode.SelectedIndex = (int)config.Background;
        SlMinOpacity.Value = config.MinOpacity * 100;
        SlMaxOpacity.Value = config.MaxOpacity * 100;
        CbStrokes.IsChecked = config.EnableStrokes;
        SlStrokeWidth.Value = config.StrokeWidth;
    }

    public WallpaperConfig BuildConfig()
    {
        var config = new WallpaperConfig();

        foreach (var row in _rows)
        {
            config.Shapes.Add(new ShapeConfig(
                row.Type,
                row.EnabledCheckBox.IsChecked == true,
                (int)row.AmountSlider.Value,
                (int)row.SizeSlider.Value,
                (int)row.SizeSlider.Value));
        }

        config.ShapeFill = (FillMode)Math.Max(CbFillMode.SelectedIndex, 0);
        config.Background = (BackgroundMode)Math.Max(CbBackgroundMode.SelectedIndex, 0);
        config.MinOpacity = (float)SlMinOpacity.Value / 100f;
        config.MaxOpacity = (float)SlMaxOpacity.Value / 100f;
        config.EnableStrokes = CbStrokes.IsChecked == true;
        config.StrokeWidth = (int)SlStrokeWidth.Value;

        return config;
    }

    private void UpdateSeed()
    {
        var config = BuildConfig();
        TbSeed.Text = config.ToSeed();
    }

    private void AnySlider_Changed(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (_initialized) UpdateSeed();
    }

    private void BtnDone_Click(object? sender, RoutedEventArgs e)
    {
        bool anyEnabled = _rows.Any(r => r.EnabledCheckBox.IsChecked == true);
        if (!anyEnabled) return;

        ResultConfig = BuildConfig();
        ResultSeed = ResultConfig.ToSeed();
        Close();
    }

    private void BtnCancel_Click(object? sender, RoutedEventArgs e)
    {
        ResultConfig = null;
        ResultSeed = null;
        Close();
    }
}
