using WallpaperMaker.Domain;

namespace WallpaperMaker.Tests;

public class ElementAgregatorTests
{
    private const string DefaultSeed = "330999996666001199999999999";

    [Fact]
    public void Constructor_ThrowsOnInvalidSeedLength()
    {
        Assert.Throws<ArgumentException>(() => new ElementAgregator("short", 1920, 1080));
    }

    [Fact]
    public void Constructor_AcceptsValidSeed()
    {
        var agg = new ElementAgregator(DefaultSeed, 1920, 1080);
        Assert.Equal(1920, agg.XResolution);
        Assert.Equal(1080, agg.YResolution);
    }

    [Fact]
    public void MakeAll_GeneratesShapesForAllEnabledTypes()
    {
        var agg = new ElementAgregator(DefaultSeed, 1920, 1080);
        agg.MakeAll();

        Assert.NotNull(agg.GetShapesByType(ShapeType.Rectangle));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Square));
        Assert.Null(agg.GetShapesByType(ShapeType.Ellipse));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Circle));
    }

    [Fact]
    public void MakeSome_OnlyCreatesSelectedShapes()
    {
        var agg = new ElementAgregator("999999999999999999999999999", 1920, 1080);
        bool[] toMake = { true, false, true, false, false, false, false, false, false };
        agg.MakeSome(toMake);

        Assert.NotNull(agg.GetShapesByType(ShapeType.Rectangle));
        Assert.Null(agg.GetShapesByType(ShapeType.Square));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Ellipse));
        Assert.Null(agg.GetShapesByType(ShapeType.Circle));
    }

    [Fact]
    public void MakeAll_GeneratesCorrectShapeCount()
    {
        var agg = new ElementAgregator("111111111111111111111111111", 1920, 1080);
        agg.MakeAll();

        var rects = agg.GetShapesByType(ShapeType.Rectangle);
        Assert.NotNull(rects);
        Assert.Equal(5, rects!.Count);
    }

    [Fact]
    public void GenerateRegularPolygon_CreatesCorrectPointCount()
    {
        var triangle = ElementAgregator.GenerateRegularPolygon(100, 100, 50, 50, 3);
        Assert.Equal(3, triangle.Length);

        var hexagon = ElementAgregator.GenerateRegularPolygon(100, 100, 50, 50, 6);
        Assert.Equal(6, hexagon.Length);

        var octagon = ElementAgregator.GenerateRegularPolygon(100, 100, 50, 50, 8);
        Assert.Equal(8, octagon.Length);
    }

    [Fact]
    public void MakeAll_CreatesPolygonShapes()
    {
        var agg = new ElementAgregator("555555555555555555555555555", 1920, 1080);
        agg.MakeAll();

        Assert.NotNull(agg.GetShapesByType(ShapeType.Triangle));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Pentagon));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Hexagon));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Octagon));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Hourglass));

        var triangles = agg.GetShapesByType(ShapeType.Triangle)!;
        Assert.All(triangles, t => Assert.True(t.IsPolygon));
        Assert.All(triangles, t => Assert.Equal(3, t.Polygon!.Length));
    }

    [Fact]
    public void MakeFromConfig_GeneratesNewShapeTypes()
    {
        var config = new WallpaperConfig();
        config.Shapes.Add(new ShapeConfig(ShapeType.Star, enabled: true, amount: 3, sizeW: 5, sizeH: 5));
        config.Shapes.Add(new ShapeConfig(ShapeType.Diamond, enabled: true, amount: 3, sizeW: 5, sizeH: 5));
        config.Shapes.Add(new ShapeConfig(ShapeType.Cross, enabled: true, amount: 3, sizeW: 5, sizeH: 5));
        config.Shapes.Add(new ShapeConfig(ShapeType.Arrow, enabled: true, amount: 3, sizeW: 5, sizeH: 5));

        var agg = new ElementAgregator(config, 1920, 1080);
        agg.MakeFromConfig(config.Shapes);

        Assert.NotNull(agg.GetShapesByType(ShapeType.Star));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Diamond));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Cross));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Arrow));

        // Stars should be polygon-based
        var stars = agg.GetShapesByType(ShapeType.Star)!;
        Assert.All(stars, s => Assert.True(s.IsPolygon));
    }

    [Fact]
    public void MakeFromConfig_GeneratesRoundedRectangles()
    {
        var config = new WallpaperConfig();
        config.Shapes.Add(new ShapeConfig(ShapeType.RoundedRectangle, enabled: true, amount: 3, sizeW: 5, sizeH: 5));

        var agg = new ElementAgregator(config, 1920, 1080);
        agg.MakeFromConfig(config.Shapes);

        var shapes = agg.GetShapesByType(ShapeType.RoundedRectangle);
        Assert.NotNull(shapes);
        Assert.All(shapes!, s => Assert.True(s.IsRoundedRect));
        Assert.All(shapes!, s => Assert.True(s.CornerRadius > 0));
    }

    [Fact]
    public void MakeFromConfig_GeneratesPathBasedShapes()
    {
        var config = new WallpaperConfig();
        config.Shapes.Add(new ShapeConfig(ShapeType.CurvedLine, enabled: true, amount: 3, sizeW: 5, sizeH: 5));
        config.Shapes.Add(new ShapeConfig(ShapeType.Blob, enabled: true, amount: 3, sizeW: 5, sizeH: 5));
        config.Shapes.Add(new ShapeConfig(ShapeType.Spiral, enabled: true, amount: 3, sizeW: 5, sizeH: 5));

        var agg = new ElementAgregator(config, 1920, 1080);
        agg.MakeFromConfig(config.Shapes);

        var curves = agg.GetShapesByType(ShapeType.CurvedLine);
        var blobs = agg.GetShapesByType(ShapeType.Blob);
        var spirals = agg.GetShapesByType(ShapeType.Spiral);

        Assert.NotNull(curves);
        Assert.NotNull(blobs);
        Assert.NotNull(spirals);

        Assert.All(curves!, s => Assert.True(s.IsPath));
        Assert.All(blobs!, s => Assert.True(s.IsPath));
        Assert.All(spirals!, s => Assert.True(s.IsPath));
    }

    [Fact]
    public void MakeFromConfig_DisabledShapesAreSkipped()
    {
        var config = new WallpaperConfig();
        config.Shapes.Add(new ShapeConfig(ShapeType.Star, enabled: false, amount: 5, sizeW: 5, sizeH: 5));
        config.Shapes.Add(new ShapeConfig(ShapeType.Circle, enabled: true, amount: 3, sizeW: 5, sizeH: 5));

        var agg = new ElementAgregator(config, 1920, 1080);
        agg.MakeFromConfig(config.Shapes);

        Assert.Null(agg.GetShapesByType(ShapeType.Star));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Circle));
    }

    [Fact]
    public void GenerateStarPolygon_CreatesCorrectPointCount()
    {
        var star5 = ElementAgregator.GenerateStarPolygon(100, 100, 50, 50, 20, 20, 5);
        Assert.Equal(10, star5.Length); // 5 outer + 5 inner

        var star8 = ElementAgregator.GenerateStarPolygon(100, 100, 50, 50, 20, 20, 8);
        Assert.Equal(16, star8.Length);
    }

    [Fact]
    public void GenerateBlobPath_CreatesClosedPath()
    {
        var path = ElementAgregator.GenerateBlobPath(100, 100, 50, 50, 6);
        Assert.NotNull(path);
        Assert.False(path.IsEmpty);
    }

    [Fact]
    public void GenerateSpiralPath_CreatesNonEmptyPath()
    {
        var path = ElementAgregator.GenerateSpiralPath(100, 100, 50, 3);
        Assert.NotNull(path);
        Assert.False(path.IsEmpty);
    }
}
