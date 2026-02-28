using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.Tests;

public class GeneratorTests
{
    private static Pallet CreateTestPallet()
    {
        return new Pallet("Test", new[] { "255,0,0", "0,255,0", "0,0,255", "255,255,255" });
    }

    [Fact]
    public void Generate_ReturnsBitmapWithCorrectDimensions()
    {
        var pallet = CreateTestPallet();
        using var gen = new Generator(pallet, 200, 100, 0);
        using var bitmap = gen.Generate("330999996666001199999999999");

        Assert.Equal(200, bitmap.Width);
        Assert.Equal(100, bitmap.Height);
    }

    [Fact]
    public void Generate_WithSupersampling_ReturnsSmallerBitmap()
    {
        var pallet = CreateTestPallet();
        using var gen = new Generator(pallet, 200, 100, 2);
        using var bitmap = gen.Generate("330999996666001199999999999");

        Assert.Equal(200, bitmap.Width);
        Assert.Equal(100, bitmap.Height);
    }

    [Fact]
    public void Generate_ProducesNonEmptyImage()
    {
        var pallet = CreateTestPallet();
        using var gen = new Generator(pallet, 100, 100, 0);
        using var bitmap = gen.Generate("999999999999999999999999999");

        bool hasContent = false;
        for (int x = 0; x < bitmap.Width && !hasContent; x++)
        {
            for (int y = 0; y < bitmap.Height && !hasContent; y++)
            {
                if (bitmap.GetPixel(x, y).Alpha > 0)
                    hasContent = true;
            }
        }
        Assert.True(hasContent);
    }

    [Fact]
    public void Generate_AllShapesEnabled_ProducesImage()
    {
        var pallet = CreateTestPallet();
        using var gen = new Generator(pallet, 200, 200, 0);
        using var bitmap = gen.Generate("999999999999999999999999999");

        Assert.Equal(200, bitmap.Width);
        Assert.Equal(200, bitmap.Height);
    }

    [Fact]
    public void Generate_OnlyRectangles_ProducesImage()
    {
        var pallet = CreateTestPallet();
        using var gen = new Generator(pallet, 200, 200, 0);
        using var bitmap = gen.Generate("500000000550000000000000000");

        Assert.Equal(200, bitmap.Width);
        Assert.Equal(200, bitmap.Height);
    }

    [Fact]
    public void Dispose_PreventsFurtherGeneration()
    {
        var pallet = CreateTestPallet();
        var gen = new Generator(pallet, 100, 100, 0);
        gen.Dispose();

        Assert.Throws<ObjectDisposedException>(() => gen.Generate("330999996666001199999999999"));
    }

    [Fact]
    public void Generate_WithPolygonShapes_Works()
    {
        var pallet = CreateTestPallet();
        using var gen = new Generator(pallet, 200, 200, 0);
        using var bitmap = gen.Generate("000055500000000055555500000");

        Assert.Equal(200, bitmap.Width);
    }

    [Fact]
    public void Generate_WithConfig_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = WallpaperConfig.CreateDefault();
        using var gen = new Generator(pallet, 200, 200, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(200, bitmap.Width);
        Assert.Equal(200, bitmap.Height);
    }

    [Fact]
    public void Generate_WithLinearGradientFill_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = WallpaperConfig.CreateDefault();
        config.ShapeFill = FillMode.LinearGradient;
        using var gen = new Generator(pallet, 200, 200, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(200, bitmap.Width);
    }

    [Fact]
    public void Generate_WithRadialGradientFill_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = WallpaperConfig.CreateDefault();
        config.ShapeFill = FillMode.RadialGradient;
        using var gen = new Generator(pallet, 200, 200, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(200, bitmap.Width);
    }

    [Fact]
    public void Generate_WithGradientBackground_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = WallpaperConfig.CreateDefault();
        config.Background = BackgroundMode.LinearGradient;
        using var gen = new Generator(pallet, 200, 200, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(200, bitmap.Width);
    }

    [Fact]
    public void Generate_WithRadialGradientBackground_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = WallpaperConfig.CreateDefault();
        config.Background = BackgroundMode.RadialGradient;
        using var gen = new Generator(pallet, 200, 200, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(200, bitmap.Width);
    }

    [Fact]
    public void Generate_WithOpacityRange_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = WallpaperConfig.CreateDefault();
        config.MinOpacity = 0.3f;
        config.MaxOpacity = 0.8f;
        using var gen = new Generator(pallet, 200, 200, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(200, bitmap.Width);
    }

    [Fact]
    public void Generate_WithStrokesEnabled_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = WallpaperConfig.CreateDefault();
        config.EnableStrokes = true;
        config.StrokeWidth = 3;
        using var gen = new Generator(pallet, 200, 200, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(200, bitmap.Width);
    }

    [Fact]
    public void Generate_WithAllNewShapes_ProducesImage()
    {
        var pallet = CreateTestPallet();
        var config = new WallpaperConfig();
        foreach (ShapeType type in Enum.GetValues<ShapeType>())
            config.Shapes.Add(new ShapeConfig(type, enabled: true, amount: 2, sizeW: 5, sizeH: 5));

        using var gen = new Generator(pallet, 300, 300, 0, config);
        using var bitmap = gen.Generate(config);

        Assert.Equal(300, bitmap.Width);
        Assert.Equal(300, bitmap.Height);
    }
}
