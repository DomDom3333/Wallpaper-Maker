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

    [Fact]
    public void PathShape_HasCorrectProperties()
    {
        var path = new SKPath();
        path.MoveTo(0, 0);
        path.CubicTo(50, 100, 100, 100, 150, 0);

        var shape = new Shape(ShapeType.CurvedLine, 45, path);

        Assert.Equal(ShapeType.CurvedLine, shape.Type);
        Assert.Equal(45, shape.Rotation);
        Assert.True(shape.IsPath);
        Assert.False(shape.IsPolygon);
        Assert.NotNull(shape.Path);
    }

    [Fact]
    public void RoundedRectShape_HasCorrectProperties()
    {
        var bounds = new SKRect(10, 20, 110, 70);
        var shape = new Shape(ShapeType.RoundedRectangle, 0, bounds, 10f);

        Assert.Equal(ShapeType.RoundedRectangle, shape.Type);
        Assert.True(shape.IsRoundedRect);
        Assert.Equal(10f, shape.CornerRadius);
        Assert.False(shape.IsPolygon);
        Assert.False(shape.IsPath);
    }

    [Fact]
    public void RegularRectShape_IsNotRoundedRect()
    {
        var bounds = new SKRect(10, 20, 110, 70);
        var shape = new Shape(ShapeType.Rectangle, 0, bounds);

        Assert.False(shape.IsRoundedRect);
        Assert.Equal(0, shape.CornerRadius);
    }
}
