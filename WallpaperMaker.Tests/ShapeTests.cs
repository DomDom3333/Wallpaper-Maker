using SkiaSharp;
using WallpaperMaker.Domain;

namespace WallpaperMaker.Tests;

public class ShapeTests
{
    [Fact]
    public void RectangleShape_HasCorrectProperties()
    {
        var bounds = new SKRect(10, 20, 110, 70);
        var shape = new Shape(ShapeType.Rectangle, 45, bounds);

        Assert.Equal(ShapeType.Rectangle, shape.Type);
        Assert.Equal(45, shape.Rotation);
        Assert.Equal(bounds, shape.Bounds);
        Assert.False(shape.IsPolygon);
        Assert.Null(shape.Polygon);
    }

    [Fact]
    public void PolygonShape_HasCorrectProperties()
    {
        var points = new SKPoint[]
        {
            new(0, 0), new(100, 0), new(50, 86)
        };
        var shape = new Shape(ShapeType.Triangle, 30, points);

        Assert.Equal(ShapeType.Triangle, shape.Type);
        Assert.Equal(30, shape.Rotation);
        Assert.True(shape.IsPolygon);
        Assert.Equal(3, shape.Polygon!.Length);
    }

    [Fact]
    public void PolygonShape_CalculatesBoundsCorrectly()
    {
        var points = new SKPoint[]
        {
            new(10, 20), new(100, 50), new(50, 90)
        };
        var shape = new Shape(ShapeType.Triangle, 0, points);

        Assert.Equal(10, shape.Bounds.Left);
        Assert.Equal(20, shape.Bounds.Top);
        Assert.Equal(100, shape.Bounds.Right);
        Assert.Equal(90, shape.Bounds.Bottom);
    }
}
