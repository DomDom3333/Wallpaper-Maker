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
        // Seed: 330999996666001199999999999
        // Amounts: 3,3,0,9,9,9,9,9,6
        var agg = new ElementAgregator(DefaultSeed, 1920, 1080);
        agg.MakeAll();

        // Shape types with amount > 0 should have shapes
        Assert.NotNull(agg.GetShapesByType(ShapeType.Rectangle)); // amount=3
        Assert.NotNull(agg.GetShapesByType(ShapeType.Square));    // amount=3
        Assert.Null(agg.GetShapesByType(ShapeType.Ellipse));      // amount=0
        Assert.NotNull(agg.GetShapesByType(ShapeType.Circle));    // amount=9
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
        // Seed with all 1s for amounts = minimum count
        var agg = new ElementAgregator("111111111111111111111111111", 1920, 1080);
        agg.MakeAll();

        var rects = agg.GetShapesByType(ShapeType.Rectangle);
        Assert.NotNull(rects);
        // SmallNumberScaler(1, 50) = (50/9)*1 = 5, so should have 5 shapes
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
        // All shape types enabled with amount=5
        var agg = new ElementAgregator("555555555555555555555555555", 1920, 1080);
        agg.MakeAll();

        Assert.NotNull(agg.GetShapesByType(ShapeType.Triangle));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Pentagon));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Hexagon));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Octagon));
        Assert.NotNull(agg.GetShapesByType(ShapeType.Hourglass));

        // Verify polygon shapes are actually polygons
        var triangles = agg.GetShapesByType(ShapeType.Triangle)!;
        Assert.All(triangles, t => Assert.True(t.IsPolygon));
        Assert.All(triangles, t => Assert.Equal(3, t.Polygon!.Length));
    }
}
