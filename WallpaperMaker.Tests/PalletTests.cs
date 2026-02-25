using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.Tests;

public class PalletTests
{
    [Fact]
    public void Constructor_ParsesColorStringsCorrectly()
    {
        var pallet = new Pallet("Test", new[] { "255,128,0", "0,255,0" });
        Assert.Equal("Test", pallet.Name);
        Assert.Equal(2, pallet.Colors.Count);
        Assert.Equal(new SKColor(255, 128, 0), pallet.Colors[0]);
        Assert.Equal(new SKColor(0, 255, 0), pallet.Colors[1]);
    }

    [Fact]
    public void Constructor_HandlesEmptyColorList()
    {
        var pallet = new Pallet("Empty", Array.Empty<string>());
        Assert.Empty(pallet.Colors);
    }

    [Fact]
    public void Constructor_FromSKColors()
    {
        var colors = new[] { SKColors.Red, SKColors.Blue };
        var pallet = new Pallet("Test", colors);
        Assert.Equal(2, pallet.Colors.Count);
    }

    [Fact]
    public void Rename_ChangesName()
    {
        var pallet = new Pallet("Old", new[] { "1,2,3" });
        pallet.Rename("New");
        Assert.Equal("New", pallet.Name);
    }

    [Fact]
    public void RandomColor_ReturnsWhiteForEmptyPallet()
    {
        var pallet = new Pallet("Empty", Array.Empty<string>());
        Assert.Equal(SKColors.White, pallet.RandomColor());
    }

    [Fact]
    public void RandomColor_ReturnsColorFromPallet()
    {
        var pallet = new Pallet("Test", new[] { "255,0,0" });
        var color = pallet.RandomColor();
        Assert.Equal(new SKColor(255, 0, 0), color);
    }

    [Fact]
    public void AddColor_AddsToCollection()
    {
        var pallet = new Pallet("Test", Array.Empty<string>());
        pallet.AddColor(SKColors.Green);
        Assert.Single(pallet.Colors);
        Assert.Equal(SKColors.Green, pallet.Colors[0]);
    }

    [Fact]
    public void RemoveColor_RemovesFromCollection()
    {
        var pallet = new Pallet("Test", new[] { "255,0,0", "0,255,0" });
        bool removed = pallet.RemoveColor(new SKColor(255, 0, 0));
        Assert.True(removed);
        Assert.Single(pallet.Colors);
    }
}
