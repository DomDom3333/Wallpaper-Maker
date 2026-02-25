using SkiaSharp;

namespace WallpaperMaker.Domain;

public enum ColorHarmony
{
    Complementary,
    Analogous,
    Triadic,
    SplitComplementary,
    Tetradic,
    Monochromatic
}

public static class ColorTheory
{
    private static readonly Random Rng = new();

    public static Pallet GeneratePalette(ColorHarmony harmony, string name)
    {
        float hue = (float)(Rng.NextDouble() * 360);
        float saturation = 0.5f + (float)Rng.NextDouble() * 0.4f;
        float lightness = 0.35f + (float)Rng.NextDouble() * 0.3f;

        return GeneratePalette(harmony, name, hue, saturation, lightness);
    }

    public static Pallet GeneratePalette(ColorHarmony harmony, string name, float baseHue, float saturation, float lightness)
    {
        var colors = harmony switch
        {
            ColorHarmony.Complementary => GenerateComplementary(baseHue, saturation, lightness),
            ColorHarmony.Analogous => GenerateAnalogous(baseHue, saturation, lightness),
            ColorHarmony.Triadic => GenerateTriadic(baseHue, saturation, lightness),
            ColorHarmony.SplitComplementary => GenerateSplitComplementary(baseHue, saturation, lightness),
            ColorHarmony.Tetradic => GenerateTetradic(baseHue, saturation, lightness),
            ColorHarmony.Monochromatic => GenerateMonochromatic(baseHue, saturation, lightness),
            _ => GenerateComplementary(baseHue, saturation, lightness)
        };

        return new Pallet(name, colors);
    }

    public static Pallet GenerateRandomPalette(string name)
    {
        var harmonies = Enum.GetValues<ColorHarmony>();
        var harmony = harmonies[Rng.Next(harmonies.Length)];
        return GeneratePalette(harmony, name);
    }

    private static List<SKColor> GenerateComplementary(float hue, float sat, float lit)
    {
        return new List<SKColor>
        {
            HslToColor(hue, sat, lit),
            HslToColor(hue, sat, lit + 0.15f),
            HslToColor((hue + 180) % 360, sat, lit),
            HslToColor((hue + 180) % 360, sat, lit + 0.15f),
            HslToColor(hue, sat * 0.5f, lit + 0.3f),
        };
    }

    private static List<SKColor> GenerateAnalogous(float hue, float sat, float lit)
    {
        return new List<SKColor>
        {
            HslToColor((hue - 30 + 360) % 360, sat, lit),
            HslToColor((hue - 15 + 360) % 360, sat, lit + 0.1f),
            HslToColor(hue, sat, lit),
            HslToColor((hue + 15) % 360, sat, lit + 0.1f),
            HslToColor((hue + 30) % 360, sat, lit),
        };
    }

    private static List<SKColor> GenerateTriadic(float hue, float sat, float lit)
    {
        return new List<SKColor>
        {
            HslToColor(hue, sat, lit),
            HslToColor(hue, sat, lit + 0.2f),
            HslToColor((hue + 120) % 360, sat, lit),
            HslToColor((hue + 240) % 360, sat, lit),
            HslToColor((hue + 240) % 360, sat, lit + 0.2f),
        };
    }

    private static List<SKColor> GenerateSplitComplementary(float hue, float sat, float lit)
    {
        return new List<SKColor>
        {
            HslToColor(hue, sat, lit),
            HslToColor(hue, sat, lit + 0.15f),
            HslToColor((hue + 150) % 360, sat, lit),
            HslToColor((hue + 210) % 360, sat, lit),
            HslToColor((hue + 180) % 360, sat * 0.4f, lit + 0.3f),
        };
    }

    private static List<SKColor> GenerateTetradic(float hue, float sat, float lit)
    {
        return new List<SKColor>
        {
            HslToColor(hue, sat, lit),
            HslToColor((hue + 90) % 360, sat, lit),
            HslToColor((hue + 180) % 360, sat, lit),
            HslToColor((hue + 270) % 360, sat, lit),
            HslToColor(hue, sat * 0.3f, lit + 0.35f),
        };
    }

    private static List<SKColor> GenerateMonochromatic(float hue, float sat, float lit)
    {
        return new List<SKColor>
        {
            HslToColor(hue, sat, 0.15f),
            HslToColor(hue, sat, 0.30f),
            HslToColor(hue, sat, 0.50f),
            HslToColor(hue, sat, 0.70f),
            HslToColor(hue, sat, 0.85f),
        };
    }

    internal static SKColor HslToColor(float h, float s, float l)
    {
        s = Math.Clamp(s, 0f, 1f);
        l = Math.Clamp(l, 0f, 1f);
        h = ((h % 360) + 360) % 360;

        float c = (1f - Math.Abs(2f * l - 1f)) * s;
        float x = c * (1f - Math.Abs((h / 60f) % 2f - 1f));
        float m = l - c / 2f;

        float r, g, b;
        if (h < 60) { r = c; g = x; b = 0; }
        else if (h < 120) { r = x; g = c; b = 0; }
        else if (h < 180) { r = 0; g = c; b = x; }
        else if (h < 240) { r = 0; g = x; b = c; }
        else if (h < 300) { r = x; g = 0; b = c; }
        else { r = c; g = 0; b = x; }

        return new SKColor(
            (byte)Math.Round((r + m) * 255),
            (byte)Math.Round((g + m) * 255),
            (byte)Math.Round((b + m) * 255));
    }
}
