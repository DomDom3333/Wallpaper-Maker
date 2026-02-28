using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.Tests;

public class ColorTheoryTests
{
    [Theory]
    [InlineData(ColorHarmony.Complementary)]
    [InlineData(ColorHarmony.Analogous)]
    [InlineData(ColorHarmony.Triadic)]
    [InlineData(ColorHarmony.SplitComplementary)]
    [InlineData(ColorHarmony.Tetradic)]
    [InlineData(ColorHarmony.Monochromatic)]
    public void GeneratePalette_ProducesFiveColors(ColorHarmony harmony)
    {
        var pallet = ColorTheory.GeneratePalette(harmony, "Test");
        Assert.Equal(5, pallet.Colors.Count);
    }

    [Fact]
    public void GeneratePalette_SetsCorrectName()
    {
        var pallet = ColorTheory.GeneratePalette(ColorHarmony.Complementary, "My Palette");
        Assert.Equal("My Palette", pallet.Name);
    }

    [Fact]
    public void GenerateRandomPalette_ProducesFiveColors()
    {
        var pallet = ColorTheory.GenerateRandomPalette("Random Test");
        Assert.Equal(5, pallet.Colors.Count);
        Assert.Equal("Random Test", pallet.Name);
    }

    [Fact]
    public void GeneratePalette_WithExplicitHue_ProducesConsistentColors()
    {
        var pallet1 = ColorTheory.GeneratePalette(ColorHarmony.Complementary, "A", 180f, 0.7f, 0.5f);
        var pallet2 = ColorTheory.GeneratePalette(ColorHarmony.Complementary, "B", 180f, 0.7f, 0.5f);

        // Same inputs should produce same colors
        Assert.Equal(pallet1.Colors.Count, pallet2.Colors.Count);
        for (int i = 0; i < pallet1.Colors.Count; i++)
            Assert.Equal(pallet1.Colors[i], pallet2.Colors[i]);
    }

    [Fact]
    public void HslToColor_ProducesRed()
    {
        var color = ColorTheory.HslToColor(0, 1f, 0.5f);
        Assert.Equal(255, color.Red);
        Assert.InRange(color.Green, 0, 1);
        Assert.InRange(color.Blue, 0, 1);
    }

    [Fact]
    public void HslToColor_ProducesGreen()
    {
        var color = ColorTheory.HslToColor(120, 1f, 0.5f);
        Assert.InRange(color.Red, 0, 1);
        Assert.Equal(255, color.Green);
        Assert.InRange(color.Blue, 0, 1);
    }

    [Fact]
    public void HslToColor_ProducesBlue()
    {
        var color = ColorTheory.HslToColor(240, 1f, 0.5f);
        Assert.InRange(color.Red, 0, 1);
        Assert.InRange(color.Green, 0, 1);
        Assert.Equal(255, color.Blue);
    }

    [Fact]
    public void HslToColor_ProducesWhiteAtFullLightness()
    {
        var color = ColorTheory.HslToColor(0, 0f, 1f);
        Assert.Equal(255, color.Red);
        Assert.Equal(255, color.Green);
        Assert.Equal(255, color.Blue);
    }

    [Fact]
    public void HslToColor_ProducesBlackAtZeroLightness()
    {
        var color = ColorTheory.HslToColor(0, 0f, 0f);
        Assert.Equal(0, color.Red);
        Assert.Equal(0, color.Green);
        Assert.Equal(0, color.Blue);
    }

    [Fact]
    public void HslToColor_ClampsValues()
    {
        // Should not throw with out-of-range inputs
        var color = ColorTheory.HslToColor(400, 1.5f, -0.5f);
        Assert.True(color.Alpha > 0);
    }

    [Theory]
    [InlineData(ColorHarmony.Complementary)]
    [InlineData(ColorHarmony.Analogous)]
    [InlineData(ColorHarmony.Triadic)]
    [InlineData(ColorHarmony.SplitComplementary)]
    [InlineData(ColorHarmony.Tetradic)]
    [InlineData(ColorHarmony.Monochromatic)]
    public void GeneratePalette_AllColorsAreOpaque(ColorHarmony harmony)
    {
        var pallet = ColorTheory.GeneratePalette(harmony, "Test");
        Assert.All(pallet.Colors, c => Assert.Equal(255, c.Alpha));
    }
}
