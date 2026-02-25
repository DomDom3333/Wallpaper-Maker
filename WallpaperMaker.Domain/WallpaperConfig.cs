namespace WallpaperMaker.Domain;

public class ShapeConfig
{
    public ShapeType Type { get; set; }
    public bool Enabled { get; set; }
    public int Amount { get; set; } = 5;
    public int SizeW { get; set; } = 5;
    public int SizeH { get; set; } = 5;

    public ShapeConfig(ShapeType type, bool enabled = false, int amount = 5, int sizeW = 5, int sizeH = 5)
    {
        Type = type;
        Enabled = enabled;
        Amount = Math.Clamp(amount, 1, 9);
        SizeW = Math.Clamp(sizeW, 1, 9);
        SizeH = Math.Clamp(sizeH, 1, 9);
    }
}

public class WallpaperConfig
{
    public List<ShapeConfig> Shapes { get; set; } = new();
    public FillMode ShapeFill { get; set; } = FillMode.Solid;
    public float MinOpacity { get; set; } = 1.0f;
    public float MaxOpacity { get; set; } = 1.0f;
    public bool EnableStrokes { get; set; }
    public int StrokeWidth { get; set; } = 2;
    public BackgroundMode Background { get; set; } = BackgroundMode.Solid;

    public static WallpaperConfig CreateDefault()
    {
        var config = new WallpaperConfig();
        foreach (ShapeType type in Enum.GetValues<ShapeType>())
        {
            bool enabled = type is ShapeType.Rectangle or ShapeType.Square or ShapeType.Circle;
            int amount = type switch
            {
                ShapeType.Rectangle => 3,
                ShapeType.Square => 3,
                ShapeType.Circle => 9,
                _ => 5
            };
            config.Shapes.Add(new ShapeConfig(type, enabled, amount, 6, 6));
        }
        return config;
    }

    /// <summary>
    /// Converts legacy 27-char seed into a WallpaperConfig (original 9 shapes only).
    /// </summary>
    public static WallpaperConfig FromSeed(string seed)
    {
        if (seed.Length < 27)
            throw new ArgumentException("Seed must be at least 27 characters.", nameof(seed));

        var config = new WallpaperConfig();
        var legacyTypes = new[]
        {
            ShapeType.Rectangle, ShapeType.Square, ShapeType.Ellipse, ShapeType.Circle,
            ShapeType.Triangle, ShapeType.Pentagon, ShapeType.Hexagon, ShapeType.Octagon,
            ShapeType.Hourglass
        };

        for (int i = 0; i < 9; i++)
        {
            int amount = seed[i] - '0';
            int sizeIdx = 9 + i * 2;
            int sizeW = seed[sizeIdx] - '0';
            int sizeH = seed[sizeIdx + 1] - '0';

            config.Shapes.Add(new ShapeConfig(
                legacyTypes[i],
                enabled: amount > 0,
                amount: Math.Max(amount, 1),
                sizeW: Math.Max(sizeW, 1),
                sizeH: Math.Max(sizeH, 1)));
        }

        // Add new shapes as disabled
        foreach (ShapeType type in Enum.GetValues<ShapeType>())
        {
            if ((int)type >= 9)
                config.Shapes.Add(new ShapeConfig(type, enabled: false));
        }

        return config;
    }

    /// <summary>
    /// Generates a legacy 27-char seed for backward compatibility.
    /// Only encodes the original 9 shape types.
    /// </summary>
    public string ToSeed()
    {
        var chars = new char[27];
        for (int i = 0; i < 27; i++) chars[i] = '0';

        foreach (var shape in Shapes)
        {
            int idx = (int)shape.Type;
            if (idx >= 9) continue;

            chars[idx] = shape.Enabled ? (char)('0' + Math.Clamp(shape.Amount, 1, 9)) : '0';
            int sizeIdx = 9 + idx * 2;
            chars[sizeIdx] = (char)('0' + Math.Clamp(shape.SizeW, 0, 9));
            chars[sizeIdx + 1] = (char)('0' + Math.Clamp(shape.SizeH, 0, 9));
        }

        return new string(chars);
    }

    public ShapeConfig? GetShape(ShapeType type) =>
        Shapes.FirstOrDefault(s => s.Type == type);
}
