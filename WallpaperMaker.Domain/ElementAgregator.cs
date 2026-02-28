using SkiaSharp;

namespace WallpaperMaker.Domain;

public class ElementAgregator
{
    private static readonly Random Rng = new();

    private readonly Dictionary<ShapeType, List<Shape>> _shapes = new();

    private readonly int[] _amounts = new int[9];
    private readonly (int W, int H)[] _sizes = new (int, int)[9];

    private readonly WallpaperConfig? _config;

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

    internal ElementAgregator(WallpaperConfig config, int width, int height)
    {
        _config = config;
        XResolution = width;
        YResolution = height;
    }

    internal void MakeAll()
    {
        if (_config != null)
        {
            MakeFromConfig(_config.Shapes.Where(s => s.Enabled).ToList());
            return;
        }

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
        if (_config != null)
        {
            var enabledShapes = _config.Shapes
                .Where((s, i) => i < toMake.Length && toMake[i] && s.Enabled)
                .ToList();
            MakeFromConfig(enabledShapes);
            return;
        }

        var tasks = new List<Task>();

        for (int i = 0; i < Math.Min(toMake.Length, 9); i++)
        {
            if (toMake[i] && _amounts[i] > 0)
                tasks.Add(CreateShapesForType(i));
        }

        Task.WaitAll(tasks.ToArray());
    }

    internal void MakeFromConfig(List<ShapeConfig> shapes)
    {
        var tasks = new List<Task>();

        foreach (var shapeConfig in shapes)
        {
            if (!shapeConfig.Enabled) continue;

            int count = Utilities.SmallNumberScaler(shapeConfig.Amount, MaxNumberOfElements);
            var sc = shapeConfig;

            tasks.Add(Task.Run(() =>
            {
                var generated = GenerateShapesForType(sc.Type, count, sc.SizeW, sc.SizeH);
                lock (_shapes)
                {
                    _shapes[sc.Type] = generated;
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());
    }

    private List<Shape> GenerateShapesForType(ShapeType type, int count, int sizeW, int sizeH)
    {
        return type switch
        {
            ShapeType.Rectangle => MakeRectangles(count, sizeW, sizeH),
            ShapeType.Square => MakeSquares(count, sizeW),
            ShapeType.Ellipse => MakeEllipses(count, sizeW, sizeH),
            ShapeType.Circle => MakeCircles(count, sizeW),
            ShapeType.Triangle => MakePolygons(ShapeType.Triangle, count, sizeW, sizeH, 3),
            ShapeType.Pentagon => MakePolygons(ShapeType.Pentagon, count, sizeW, sizeH, 5),
            ShapeType.Hexagon => MakePolygons(ShapeType.Hexagon, count, sizeW, sizeH, 6),
            ShapeType.Octagon => MakePolygons(ShapeType.Octagon, count, sizeW, sizeH, 8),
            ShapeType.Hourglass => MakeHourglasses(count, sizeW, sizeH),
            ShapeType.Star => MakeStars(count, sizeW, sizeH),
            ShapeType.Diamond => MakePolygons(ShapeType.Diamond, count, sizeW, sizeH, 4),
            ShapeType.Cross => MakeCrosses(count, sizeW, sizeH),
            ShapeType.Arrow => MakeArrows(count, sizeW, sizeH),
            ShapeType.RoundedRectangle => MakeRoundedRectangles(count, sizeW, sizeH),
            ShapeType.CurvedLine => MakeCurvedLines(count, sizeW, sizeH),
            ShapeType.Blob => MakeBlobs(count, sizeW, sizeH),
            ShapeType.Spiral => MakeSpirals(count, sizeW, sizeH),
            _ => new List<Shape>()
        };
    }

    private Task CreateShapesForType(int index)
    {
        var type = (ShapeType)index;
        int count = Utilities.SmallNumberScaler(_amounts[index], MaxNumberOfElements);
        var size = _sizes[index];

        return Task.Run(() =>
        {
            var shapes = GenerateShapesForType(type, count, size.W, size.H);
            lock (_shapes)
            {
                _shapes[type] = shapes;
            }
        });
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

    private List<Shape> MakeStars(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(360);
            float cx = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float cy = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float outerRx = scaledW > 0 ? Rng.Next(1, scaledW) / 2f : 1;
            float outerRy = scaledH > 0 ? Rng.Next(1, scaledH) / 2f : 1;
            float innerRx = outerRx * 0.4f;
            float innerRy = outerRy * 0.4f;

            int pointCount = Rng.Next(5, 9);
            var points = GenerateStarPolygon(cx, cy, outerRx, outerRy, innerRx, innerRy, pointCount);
            shapes.Add(new Shape(ShapeType.Star, rotation, points));
        }
        return shapes;
    }

    private List<Shape> MakeCrosses(int count, int maxSizeW, int maxSizeH)
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
            float armW = hw * 0.33f;
            float armH = hh * 0.33f;

            var points = new SKPoint[]
            {
                new(cx - armW, cy - hh),
                new(cx + armW, cy - hh),
                new(cx + armW, cy - armH),
                new(cx + hw, cy - armH),
                new(cx + hw, cy + armH),
                new(cx + armW, cy + armH),
                new(cx + armW, cy + hh),
                new(cx - armW, cy + hh),
                new(cx - armW, cy + armH),
                new(cx - hw, cy + armH),
                new(cx - hw, cy - armH),
                new(cx - armW, cy - armH),
            };
            shapes.Add(new Shape(ShapeType.Cross, rotation, points));
        }
        return shapes;
    }

    private List<Shape> MakeArrows(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(360);
            float cx = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float cy = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float hw = (scaledW > 0 ? Rng.Next(1, scaledW) : 1) / 2f;
            float hh = (scaledH > 0 ? Rng.Next(1, scaledH) : 1) / 2f;
            float shaftW = hw * 0.35f;

            var points = new SKPoint[]
            {
                new(cx, cy - hh),           // tip
                new(cx + hw, cy),            // right wing
                new(cx + shaftW, cy),        // right inner
                new(cx + shaftW, cy + hh),   // bottom right
                new(cx - shaftW, cy + hh),   // bottom left
                new(cx - shaftW, cy),        // left inner
                new(cx - hw, cy),            // left wing
            };
            shapes.Add(new Shape(ShapeType.Arrow, rotation, points));
        }
        return shapes;
    }

    private List<Shape> MakeRoundedRectangles(int count, int maxSizeW, int maxSizeH)
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
            float cornerRadius = Math.Min(w, h) * 0.2f;

            shapes.Add(new Shape(ShapeType.RoundedRectangle, rotation, new SKRect(x, y, x + w, y + h), cornerRadius));
        }
        return shapes;
    }

    private List<Shape> MakeCurvedLines(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(360);
            float startX = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float startY = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float rangeW = scaledW > 0 ? Rng.Next(1, scaledW) : 1;
            float rangeH = scaledH > 0 ? Rng.Next(1, scaledH) : 1;

            var path = new SKPath();
            path.MoveTo(startX, startY);

            int segments = Rng.Next(2, 6);
            for (int s = 0; s < segments; s++)
            {
                float cp1x = startX + (float)(Rng.NextDouble() - 0.5) * rangeW;
                float cp1y = startY + (float)(Rng.NextDouble() - 0.5) * rangeH;
                float cp2x = startX + (float)(Rng.NextDouble() - 0.5) * rangeW;
                float cp2y = startY + (float)(Rng.NextDouble() - 0.5) * rangeH;
                float endX = startX + (float)(Rng.NextDouble() - 0.5) * rangeW;
                float endY = startY + (float)(Rng.NextDouble() - 0.5) * rangeH;

                path.CubicTo(cp1x, cp1y, cp2x, cp2y, endX, endY);
            }

            shapes.Add(new Shape(ShapeType.CurvedLine, rotation, path));
        }
        return shapes;
    }

    private List<Shape> MakeBlobs(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);
        int scaledH = Utilities.SmallNumberScaler(maxSizeH, YResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(360);
            float cx = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float cy = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float rx = scaledW > 0 ? Rng.Next(1, scaledW) / 2f : 1;
            float ry = scaledH > 0 ? Rng.Next(1, scaledH) / 2f : 1;

            int points = Rng.Next(5, 10);
            var path = GenerateBlobPath(cx, cy, rx, ry, points);
            shapes.Add(new Shape(ShapeType.Blob, rotation, path));
        }
        return shapes;
    }

    private List<Shape> MakeSpirals(int count, int maxSizeW, int maxSizeH)
    {
        var shapes = new List<Shape>(count);
        int scaledW = Utilities.SmallNumberScaler(maxSizeW, XResolution);

        for (int i = 0; i < count; i++)
        {
            int rotation = Rng.Next(360);
            float cx = Rng.Next(XResolution / 10, XResolution - XResolution / 10);
            float cy = Rng.Next(YResolution / 100, YResolution - YResolution / 10);
            float maxR = scaledW > 0 ? Rng.Next(1, scaledW) / 2f : 1;

            float turns = 2f + (float)Rng.NextDouble() * 3f;
            var path = GenerateSpiralPath(cx, cy, maxR, turns);
            shapes.Add(new Shape(ShapeType.Spiral, rotation, path));
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

    internal static SKPoint[] GenerateStarPolygon(float cx, float cy, float outerRx, float outerRy,
        float innerRx, float innerRy, int pointCount)
    {
        var points = new SKPoint[pointCount * 2];
        double angleStep = Math.PI / pointCount;
        double startAngle = -Math.PI / 2;

        for (int i = 0; i < pointCount * 2; i++)
        {
            double angle = startAngle + i * angleStep;
            bool isOuter = i % 2 == 0;
            float rx = isOuter ? outerRx : innerRx;
            float ry = isOuter ? outerRy : innerRy;

            points[i] = new SKPoint(
                cx + (float)(rx * Math.Cos(angle)),
                cy + (float)(ry * Math.Sin(angle)));
        }
        return points;
    }

    internal static SKPath GenerateBlobPath(float cx, float cy, float rx, float ry, int pointCount)
    {
        var rng = new Random(Rng.Next());
        double angleStep = 2 * Math.PI / pointCount;
        var controlPoints = new SKPoint[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            double angle = i * angleStep;
            float jitterR = 0.6f + (float)rng.NextDouble() * 0.8f;
            controlPoints[i] = new SKPoint(
                cx + (float)(rx * jitterR * Math.Cos(angle)),
                cy + (float)(ry * jitterR * Math.Sin(angle)));
        }

        var path = new SKPath();
        path.MoveTo(controlPoints[0]);

        for (int i = 0; i < pointCount; i++)
        {
            var p0 = controlPoints[i];
            var p1 = controlPoints[(i + 1) % pointCount];
            var mid = new SKPoint((p0.X + p1.X) / 2f, (p0.Y + p1.Y) / 2f);

            float cpOffsetX = (float)(rng.NextDouble() - 0.5) * rx * 0.5f;
            float cpOffsetY = (float)(rng.NextDouble() - 0.5) * ry * 0.5f;

            path.QuadTo(
                p0.X + cpOffsetX, p0.Y + cpOffsetY,
                mid.X, mid.Y);
        }

        path.Close();
        return path;
    }

    internal static SKPath GenerateSpiralPath(float cx, float cy, float maxRadius, float turns)
    {
        var path = new SKPath();
        int steps = (int)(turns * 60);
        float totalAngle = turns * 2f * (float)Math.PI;

        path.MoveTo(cx, cy);

        for (int i = 1; i <= steps; i++)
        {
            float t = (float)i / steps;
            float angle = t * totalAngle;
            float r = t * maxRadius;
            float x = cx + r * (float)Math.Cos(angle);
            float y = cy + r * (float)Math.Sin(angle);
            path.LineTo(x, y);
        }

        return path;
    }

    internal List<Shape>? GetShapesByType(ShapeType type)
    {
        lock (_shapes)
        {
            return _shapes.TryGetValue(type, out var list) ? list : null;
        }
    }
}
