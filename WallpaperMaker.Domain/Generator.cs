using SkiaSharp;

namespace WallpaperMaker.Domain;

public class Generator : IDisposable
{
    private readonly int _xRes;
    private readonly int _yRes;
    private readonly int _supersampling;
    private readonly Pallet _palette;
    private readonly WallpaperConfig _wallpaperConfig;
    private SKBitmap? _bitmap;
    private SKCanvas? _canvas;
    private ElementAgregator? _maker;
    private bool _disposed;

    private static readonly Random Rng = new();

    public Generator(Pallet palette, int horRes, int vertRes, int supersampling)
        : this(palette, horRes, vertRes, supersampling, WallpaperConfig.CreateDefault())
    {
    }

    public Generator(Pallet palette, int horRes, int vertRes, int supersampling, WallpaperConfig config)
    {
        _palette = palette;
        _supersampling = supersampling;
        _wallpaperConfig = config;

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

        var result = _bitmap;
        _bitmap = null;
        return result;
    }

    public SKBitmap Generate(WallpaperConfig config)
    {
        if (_bitmap == null || _canvas == null)
            throw new ObjectDisposedException(nameof(Generator));

        _maker = new ElementAgregator(config, _xRes, _yRes);
        _maker.MakeFromConfig(config.Shapes.Where(s => s.Enabled).ToList());
        DrawAll(config);

        if (_supersampling > 1)
            return DownsampleBitmap();

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

    private void DrawAll() => DrawAll(_wallpaperConfig);

    private void DrawAll(WallpaperConfig config)
    {
        if (_canvas == null || _maker == null)
            return;

        DrawBackground(config);
        DrawShapes(config);
    }

    private void DrawBackground(WallpaperConfig config)
    {
        switch (config.Background)
        {
            case BackgroundMode.LinearGradient:
            {
                var color1 = _palette.RandomColor();
                var color2 = _palette.RandomColor();
                using var shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(_xRes, _yRes),
                    new[] { color1, color2 },
                    SKShaderTileMode.Clamp);
                using var paint = new SKPaint { Shader = shader, IsAntialias = true };
                _canvas!.DrawRect(0, 0, _xRes, _yRes, paint);
                break;
            }
            case BackgroundMode.RadialGradient:
            {
                var color1 = _palette.RandomColor();
                var color2 = _palette.RandomColor();
                float radius = Math.Max(_xRes, _yRes) * 0.7f;
                using var shader = SKShader.CreateRadialGradient(
                    new SKPoint(_xRes / 2f, _yRes / 2f),
                    radius,
                    new[] { color1, color2 },
                    SKShaderTileMode.Clamp);
                using var paint = new SKPaint { Shader = shader, IsAntialias = true };
                _canvas!.DrawRect(0, 0, _xRes, _yRes, paint);
                break;
            }
            default:
            {
                using var bgPaint = new SKPaint { Color = _palette.RandomColor(), IsAntialias = true };
                _canvas!.DrawRect(0, 0, _xRes, _yRes, bgPaint);
                break;
            }
        }
    }

    private void DrawShapes(WallpaperConfig config)
    {
        var shapeTypes = Enum.GetValues<ShapeType>();
        var drawOrder = Utilities.ShuffledRange(shapeTypes.Length);

        foreach (int idx in drawOrder)
        {
            var type = shapeTypes[idx];
            var shapes = _maker!.GetShapesByType(type);
            if (shapes == null) continue;

            foreach (var shape in shapes)
            {
                using var paint = new SKPaint { IsAntialias = true };

                float opacity = config.MinOpacity + (float)Rng.NextDouble() * (config.MaxOpacity - config.MinOpacity);
                var baseColor = _palette.RandomColor();
                paint.Color = baseColor.WithAlpha((byte)(255 * opacity));

                ApplyFillMode(paint, config.ShapeFill, shape, baseColor, opacity);

                if (config.EnableStrokes)
                {
                    paint.Style = SKPaintStyle.StrokeAndFill;
                    paint.StrokeWidth = config.StrokeWidth;
                }

                DrawShape(_canvas!, shape, paint);
            }
        }
    }

    private void ApplyFillMode(SKPaint paint, FillMode fillMode, Shape shape, SKColor baseColor, float opacity)
    {
        if (fillMode == FillMode.Solid) return;

        var secondColor = _palette.RandomColor().WithAlpha((byte)(255 * opacity));

        switch (fillMode)
        {
            case FillMode.LinearGradient:
            {
                float angle = (float)(Rng.NextDouble() * Math.PI * 2);
                float dx = (float)Math.Cos(angle) * shape.Bounds.Width / 2f;
                float dy = (float)Math.Sin(angle) * shape.Bounds.Height / 2f;

                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(shape.Bounds.MidX - dx, shape.Bounds.MidY - dy),
                    new SKPoint(shape.Bounds.MidX + dx, shape.Bounds.MidY + dy),
                    new[] { baseColor.WithAlpha((byte)(255 * opacity)), secondColor },
                    SKShaderTileMode.Clamp);
                break;
            }
            case FillMode.RadialGradient:
            {
                float radius = Math.Max(shape.Bounds.Width, shape.Bounds.Height) / 2f;
                paint.Shader = SKShader.CreateRadialGradient(
                    new SKPoint(shape.Bounds.MidX, shape.Bounds.MidY),
                    Math.Max(radius, 1f),
                    new[] { baseColor.WithAlpha((byte)(255 * opacity)), secondColor },
                    SKShaderTileMode.Clamp);
                break;
            }
        }
    }

    private static void DrawShape(SKCanvas canvas, Shape shape, SKPaint paint)
    {
        canvas.Save();

        float cx = shape.Bounds.MidX;
        float cy = shape.Bounds.MidY;
        canvas.RotateDegrees(shape.Rotation, cx, cy);

        if (shape.IsPath)
        {
            if (shape.Type == ShapeType.CurvedLine || shape.Type == ShapeType.Spiral)
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = Math.Max(paint.StrokeWidth, 2f + shape.Bounds.Width * 0.01f);
                paint.StrokeCap = SKStrokeCap.Round;
            }
            canvas.DrawPath(shape.Path!, paint);
        }
        else if (shape.IsPolygon)
        {
            using var path = new SKPath();
            path.MoveTo(shape.Polygon![0]);
            for (int i = 1; i < shape.Polygon.Length; i++)
                path.LineTo(shape.Polygon[i]);
            path.Close();
            canvas.DrawPath(path, paint);
        }
        else if (shape.IsRoundedRect)
        {
            canvas.DrawRoundRect(shape.Bounds, shape.CornerRadius, shape.CornerRadius, paint);
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
