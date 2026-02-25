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

        // Verify the bitmap has non-transparent pixels
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
        // Enable triangle (idx 4), pentagon (idx 5), hexagon (idx 6)
        using var bitmap = gen.Generate("000055500000000055555500000");

        Assert.Equal(200, bitmap.Width);
    }
}
