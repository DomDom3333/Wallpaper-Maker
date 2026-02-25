using SkiaSharp;

namespace WallpaperMaker.Domain;

public class Pallet
{
    private static readonly Random Rng = new();

    public string Name { get; private set; }
    public List<SKColor> Colors { get; private set; }

    public Pallet(string name, IEnumerable<string> colorStrings)
    {
        Name = name;
        Colors = new List<SKColor>();
        foreach (var colorStr in colorStrings)
        {
            var parts = colorStr.Split(',');
            if (parts.Length >= 3)
            {
                int r = int.Parse(parts[parts.Length - 3]);
                int g = int.Parse(parts[parts.Length - 2]);
                int b = int.Parse(parts[parts.Length - 1]);
                Colors.Add(new SKColor((byte)r, (byte)g, (byte)b));
            }
        }
    }

    public Pallet(string name, IEnumerable<SKColor> colors)
    {
        Name = name;
        Colors = new List<SKColor>(colors);
    }

    public void Rename(string newName)
    {
        Name = newName;
    }

    public SKColor RandomColor()
    {
        if (Colors.Count == 0)
            return SKColors.White;
        return Colors[Rng.Next(Colors.Count)];
    }

    public void AddColor(SKColor color)
    {
        Colors.Add(color);
    }

    public bool RemoveColor(SKColor color)
    {
        return Colors.Remove(color);
    }
}
