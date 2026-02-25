using SkiaSharp;

namespace WallpaperMaker.Domain;

public class ElementAgregator
{
    private static readonly Random Rng = new();

    internal List<Shape>? Rectangles { get; private set; }
    internal List<Shape>? Squares { get; private set; }
    internal List<Shape>? Ellipses { get; private set; }
    internal List<Shape>? Circles { get; private set; }
    internal List<Shape>? Triangles { get; private set; }
    internal List<Shape>? Pentagons { get; private set; }
    internal List<Shape>? Hexagons { get; private set; }
    internal List<Shape>? Octagons { get; private set; }
    internal List<Shape>? Hourglasses { get; private set; }

    private readonly int[] _amounts = new int[9];
    private readonly (int W, int H)[] _sizes = new (int, int)[9];

    public int XResolution { get; }
    public int YResolution { get; }
    private int MaxNumberOfElements { get; } = 50;

    internal ElementAgregator(string seed, int width, int height)
    {
        if (seed.Length != 27)
            throw new ArgumentException($"Seed must be exactly 27 characters, got {seed.Length}.", nameof(seed));

        for (int i = 0; i < 9; i++)
            _amounts[i] = seed[i] - '0';

        for (int i = 0; i < 9; i++)
        {
            int sizeIdx = 9 + i * 2;
            _sizes[i] = (seed[sizeIdx] - '0', seed[sizeIdx + 1] - '0');
        }

        XResolution = width;
        YResolution = height;
    }

    internal void MakeAll()
    {
        var tasks = new List<Task>();

        for (int i = 0; i < 9; i++)
        {
            if (_amounts[i] > 0)
                tasks.Add(CreateShapesForType(i));
        }

        Task.WaitAll(tasks.ToArray());
    }

    internal void MakeSome(bool[] toMake)
    {
        var tasks = new List<Task>();

        for (int i = 0; i < Math.Min(toMake.Length, 9); i++)
        {
            if (toMake[i] && _amounts[i] > 0)
                tasks.Add(CreateShapesForType(i));
        }

        Task.WaitAll(tasks.ToArray());
    }

    private Task CreateShapesForType(int index)
    {
        var type = (ShapeType)index;
        int count = Utilities.SmallNumberScaler(_amounts[index], MaxNumberOfElements);
        var size = _sizes[index];

        return Task.Run(() =>
        {
            var shapes = type switch
            {
                ShapeType.Rectangle => MakeRectangles(count, size.W, size.H),
                ShapeType.Square => MakeSquares(count, size.W),
                ShapeType.Ellipse => MakeEllipses(count, size.W, size.H),
                ShapeType.Circle => MakeCircles(count, size.W),
                ShapeType.Triangle => MakePolygons(ShapeType.Triangle, count, size.W, size.H, 3),
                ShapeType.Pentagon => MakePolygons(ShapeType.Pentagon, count, size.W, size.H, 5),
                ShapeType.Hexagon => MakePolygons(ShapeType.Hexagon, count, size.W, size.H, 6),
                ShapeType.Octagon => MakePolygons(ShapeType.Octagon, count, size.W, size.H, 8),
                ShapeType.Hourglass => MakeHourglasses(count, size.W, size.H),
                _ => new List<Shape>()
            };

            AssignShapes(type, shapes);
        });
    }

    private void AssignShapes(ShapeType type, List<Shape> shapes)
    {
        switch (type)
        {
            case ShapeType.Rectangle: Rectangles = shapes; break;
            case ShapeType.Square: Squares = shapes; break;
            case ShapeType.Ellipse: Ellipses = shapes; break;
            case ShapeType.Circle: Circles = shapes; break;
            case ShapeType.Triangle: Triangles = shapes; break;
            case ShapeType.Pentagon: Pentagons = shapes; break;
            case ShapeType.Hexagon: Hexagons = shapes; break;
            case ShapeType.Octagon: Octagons = shapes; break;
            case ShapeType.Hourglass: Hourglasses = shapes; break;
        }
    }

    private List<Shape> MakeRectangles(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(90);
            float x = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float y = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float w = scaledW > 0 ? Rng.Next(1, scaledW) : 1;
            float h = scaledH > 0 ? Rng.Next(1, scaledH) : 1;
            shapes.Add(new Shape(ShapeType.Rectangle, rotation, new SKRect(x, y, x + w, y + h)));
        }
        return shapes;
    }

    private List<Shape> MakeSquares(int count, int maxSizeW)
    {
        var shapes = new List<Shape>(count);
        int scaled = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int size = scaled > 0 ? Rng.Next(1, scaled) : 1;

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(90);
            float x = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float y = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            shapes.Add(new Shape(ShapeType.Square, rotation, new SKRect(x, y, x + size, y + size)));
        }
        return shapes;
    }

    private List<Shape> MakeEllipses(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(90);
            float x = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float y = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float w = scaledW > 0 ? Rng.Next(1, scaledW) : 1;
            float h = scaledH > 0 ? Rng.Next(1, scaledH) : 1;
            shapes.Add(new Shape(ShapeType.Ellipse, rotation, new SKRect(x, y, x + w, y + h)));
        }
        return shapes;
    }

    private List<Shape> MakeCircles(int count, int maxSizeW)
    {
        var shapes = new List<Shape>(count);
        int scaled = Utilities.SmallNumberScaler(maxSizeW + 1, XResolution);
        int size = scaled > 0 ? Rng.Next(1, scaled) : 1;

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(90);
            float x = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float y = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            shapes.Add(new Shape(ShapeType.Circle, rotation, new SKRect(x, y, x + size, y + size)));
        }
        return shapes;
    }

    private List<Shape> MakePolygons(ShapeType type, int count, int maxSizeW, int maxSizeH, int sides)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(90);
            float cx = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float cy = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float rx = scaledW > 0 ? Rng.Next(1, scaledW) / 2f : 1;
            float ry = scaledH > 0 ? Rng.Next(1, scaledH) / 2f : 1;

            var points = GenerateRegularPolygon(cx, cy, rx, ry, sides);
            shapes.Add(new Shape(type, rotation, points));
        }
        return shapes;
    }

    private List<Shape> MakeHourglasses(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(90);
            float cx = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float cy = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float hw = (scaledW > 0 ? Rng.Next(1, scaledW) : 1) / 2f;
            float hh = (scaledH > 0 ? Rng.Next(1, scaledH) : 1) / 2f;

            // Hourglass: two triangles meeting at center, crossed
            var points = new SKPoint[]
            {
                new(cx - hw, cy - hh),
                new(cx + hw, cy - hh),
                new(cx - hw, cy + hh),
                new(cx + hw, cy + hh),
            };
            shapes.Add(new Shape(ShapeType.Hourglass, rotation, points));
        }
        return shapes;
    }

    internal static SKPoint[] GenerateRegularPolygon(float cx, float cy, float rx, float ry, int sides)
    {
        var points = new SKPoint[sides];
        double angleStep = 2 * Math.PI / sides;
        double startAngle = -Math.PI / 2; // start from top

        for (int i = 0; i < sides; i++)
        {
            double angle = startAngle + i * angleStep;
            points[i] = new SKPoint(
                cx + (float)(rx * Math.Cos(angle)),
                cy + (float)(ry * Math.Sin(angle)));
        }
        return points;
    }

    internal List<Shape>? GetShapesByType(ShapeType type) => type switch
    {
        ShapeType.Rectangle => Rectangles,
        ShapeType.Square => Squares,
        ShapeType.Ellipse => Ellipses,
        ShapeType.Circle => Circles,
        ShapeType.Triangle => Triangles,
        ShapeType.Pentagon => Pentagons,
        ShapeType.Hexagon => Hexagons,
        ShapeType.Octagon => Octagons,
        ShapeType.Hourglass => Hourglasses,
        _ => null
    };
}
