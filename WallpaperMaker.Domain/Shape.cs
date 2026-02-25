using SkiaSharp;

namespace WallpaperMaker.Domain;

public class Shape
{
    public ShapeType Type { get; }
    public int Rotation { get; }
    public SKRect Bounds { get; }
    public SKPoint[]? Polygon { get; }

    public Shape(ShapeType type, int rotation, SKRect bounds)
    {
        Type = type;
        Rotation = rotation;
        Bounds = bounds;
    }

    public Shape(ShapeType type, int rotation, SKPoint[] polygon)
    {
        Type = type;
        Rotation = rotation;
        Polygon = polygon;
        Bounds = CalculateBounds(polygon);
    }

    public bool IsPolygon => Polygon != null;

    private static SKRect CalculateBounds(SKPoint[] points)
    {
        float minX = points[0].X, maxX = points[0].X;
        float minY = points[0].Y, maxY = points[0].Y;
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i].X < minX) minX = points[i].X;
            if (points[i].X > maxX) maxX = points[i].X;
            if (points[i].Y < minY) minY = points[i].Y;
            if (points[i].Y > maxY) maxY = points[i].Y;
        }
        return new SKRect(minX, minY, maxX, maxY);
    }
}
