namespace WallpaperMaker.Domain;

public enum ShapeType
{
    // Original shapes (indices 0-8, seed-compatible)
    Rectangle,
    Square,
    Ellipse,
    Circle,
    Triangle,
    Pentagon,
    Hexagon,
    Octagon,
    Hourglass,

    // New shapes (indices 9+)
    Star,
    Diamond,
    Cross,
    Arrow,
    RoundedRectangle,
    CurvedLine,
    Blob,
    Spiral
}

public enum FillMode
{
    Solid,
    LinearGradient,
    RadialGradient
}

public enum BackgroundMode
{
    Solid,
    LinearGradient,
    RadialGradient
}
