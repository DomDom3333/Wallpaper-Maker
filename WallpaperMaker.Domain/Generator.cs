using SkiaSharp;

namespace WallpaperMaker.Domain;

public class Generator : IDisposable
{
    private readonly int _xRes;
    private readonly int _yRes;
    private readonly int _supersampling;
    private readonly Pallet _palette;
    private SKBitmap? _bitmap;
    private SKCanvas? _canvas;
    private ElementAgregator? _maker;
    private bool _disposed;

    public Generator(Pallet palette, int horRes, int vertRes, int supersampling)
    {
        _palette = palette;
        _supersampling = supersampling;

        if (_supersampling <= 0)
        {
            _xRes = horRes;
            _yRes = vertRes;
        }
        else
        {
            _xRes = horRes * _supersampling;
            _yRes = vertRes * _supersampling;
        }

        _bitmap = new SKBitmap(_xRes, _yRes, SKColorType.Rgba8888, SKAlphaType.Premul);
        _canvas = new SKCanvas(_bitmap);
        _canvas.Clear(SKColors.Transparent);
    }

    public SKBitmap Generate(string seed)
    {
        if (_bitmap == null || _canvas == null)
            throw new ObjectDisposedException(nameof(Generator));

        GenerateShapes(seed);
        DrawAll();

        if (_supersampling > 1)
            return DownsampleBitmap();

        // Transfer ownership of _bitmap to the caller.
        // Setting _bitmap to null prevents Dispose() from freeing memory
        // that the caller now owns, avoiding an AccessViolationException.
        var result = _bitmap;
        _bitmap = null;
        return result;
    }

    private SKBitmap DownsampleBitmap()
    {
        int targetW = _xRes / _supersampling;
        int targetH = _yRes / _supersampling;
        var resized = new SKBitmap(targetW, targetH, SKColorType.Rgba8888, SKAlphaType.Premul);

        using var canvas = new SKCanvas(resized);
        using var paint = new SKPaint
        {
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true
        };
        canvas.DrawBitmap(_bitmap!, new SKRect(0, 0, targetW, targetH), paint);
        return resized;
    }

    private void GenerateShapes(string seed)
    {
        _maker = new ElementAgregator(seed, _xRes, _yRes);

        bool hasDisabledShape = false;
        for (int i = 0; i < 9; i++)
        {
            if (seed[i] == '0')
            {
                hasDisabledShape = true;
                break;
            }
        }

        if (hasDisabledShape)
        {
            bool[] enabledShapes = new bool[9];
            for (int i = 0; i < 9; i++)
                enabledShapes[i] = seed[i] != '0';
            _maker.MakeSome(enabledShapes);
        }
        else
        {
            _maker.MakeAll();
        }
    }

    private void DrawAll()
    {
        if (_canvas == null || _maker == null)
            return;

        // Draw background with a random palette color
        using (var bgPaint = new SKPaint { Color = _palette.RandomColor(), IsAntialias = true })
        {
            _canvas.DrawRect(0, 0, _xRes, _yRes, bgPaint);
        }

        // Draw shapes in random order across all shape types
        var shapeTypes = Enum.GetValues<ShapeType>();
        var drawOrder = Utilities.ShuffledRange(shapeTypes.Length);

        using var paint = new SKPaint { IsAntialias = true };

        foreach (int idx in drawOrder)
        {
            var type = shapeTypes[idx];
            var shapes = _maker.GetShapesByType(type);
            if (shapes == null) continue;

            foreach (var shape in shapes)
            {
                paint.Color = _palette.RandomColor();
                DrawShape(_canvas, shape, paint);
            }
        }
    }

    private static void DrawShape(SKCanvas canvas, Shape shape, SKPaint paint)
    {
        canvas.Save();

        float cx = shape.Bounds.MidX;
        float cy = shape.Bounds.MidY;
        canvas.RotateDegrees(shape.Rotation, cx, cy);

        if (shape.IsPolygon)
        {
            using var path = new SKPath();
            path.MoveTo(shape.Polygon![0]);
            for (int i = 1; i < shape.Polygon.Length; i++)
                path.LineTo(shape.Polygon[i]);
            path.Close();
            canvas.DrawPath(path, paint);
        }
        else
        {
            switch (shape.Type)
            {
                case ShapeType.Rectangle:
                case ShapeType.Square:
                    canvas.DrawRect(shape.Bounds, paint);
                    break;
                case ShapeType.Ellipse:
                case ShapeType.Circle:
                    canvas.DrawOval(shape.Bounds, paint);
                    break;
            }
        }

        canvas.Restore();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _canvas?.Dispose();
        _bitmap?.Dispose();
        _canvas = null;
        _bitmap = null;
        GC.SuppressFinalize(this);
    }
}
