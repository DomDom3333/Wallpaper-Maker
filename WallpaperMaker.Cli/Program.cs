using SkiaSharp;
using WallpaperMaker.Domain;

const string DefaultSeed = "330999996666001199999999999";
const string DefaultPalletJson = """
{
  "Pallets": [
    {
      "Pallet": {
        "Name": "Default",
        "Colors": [
          "229,244,227",
          "93,169,233",
          "0,63,145",
          "255,255,255",
          "109,50,109"
        ]
      }
    }
  ]
}
""";

int width = 1920;
int height = 1080;
int supersampling = 0;
string seed = DefaultSeed;
string outputPath = "wallpaper.png";
string? palletFile = null;
string format = "png";

// Parse command-line arguments
for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "-w" or "--width":
            width = int.Parse(args[++i]);
            break;
        case "-h" or "--height":
            height = int.Parse(args[++i]);
            break;
        case "-s" or "--seed":
            seed = args[++i];
            break;
        case "-o" or "--output":
            outputPath = args[++i];
            break;
        case "-m" or "--multisampling":
            supersampling = int.Parse(args[++i]);
            break;
        case "-p" or "--palette":
            palletFile = args[++i];
            break;
        case "-f" or "--format":
            format = args[++i].ToLowerInvariant();
            break;
        case "--help":
            PrintHelp();
            return 0;
        default:
            Console.Error.WriteLine($"Unknown argument: {args[i]}");
            PrintHelp();
            return 1;
    }
}

// Load palette
List<Pallet> pallets;
if (palletFile != null && File.Exists(palletFile))
{
    string json = File.ReadAllText(palletFile);
    pallets = Utilities.UnpackUserColorPallets(json);
}
else
{
    pallets = Utilities.UnpackUserColorPallets(DefaultPalletJson);
}

if (pallets.Count == 0)
{
    Console.Error.WriteLine("Error: No palettes loaded.");
    return 1;
}

var palette = Utilities.RandomPalletFromList(pallets);

Console.WriteLine($"Generating wallpaper: {width}x{height}, seed={seed}, palette={palette.Name}, supersampling={supersampling}x");

using var generator = new Generator(palette, width, height, supersampling);
using var bitmap = generator.Generate(seed);

// Determine format from file extension or explicit format flag
var encodedFormat = format switch
{
    "jpg" or "jpeg" => SKEncodedImageFormat.Jpeg,
    "bmp" => SKEncodedImageFormat.Bmp,
    "webp" => SKEncodedImageFormat.Webp,
    _ => SKEncodedImageFormat.Png
};

// Auto-detect from extension if not explicitly set
if (format == "png" && outputPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
    encodedFormat = SKEncodedImageFormat.Jpeg;
else if (format == "png" && outputPath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
    encodedFormat = SKEncodedImageFormat.Bmp;
else if (format == "png" && outputPath.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
    encodedFormat = SKEncodedImageFormat.Webp;

using var image = SKImage.FromBitmap(bitmap);
using var data = image.Encode(encodedFormat, 95);
using var stream = File.OpenWrite(outputPath);
data.SaveTo(stream);

Console.WriteLine($"Wallpaper saved to: {outputPath}");
return 0;

static void PrintHelp()
{
    Console.WriteLine("""
    Wallpaper Maker CLI - Cross-platform wallpaper generator

    Usage: wallpaper-maker [options]

    Options:
      -w, --width <pixels>        Width in pixels (default: 1920)
      -h, --height <pixels>       Height in pixels (default: 1080)
      -s, --seed <seed>           27-character seed string
      -o, --output <path>         Output file path (default: wallpaper.png)
      -m, --multisampling <level> Supersampling level 0-32 (default: 0)
      -p, --palette <path>        Path to palette JSON file
      -f, --format <fmt>          Output format: png, jpg, bmp, webp (default: png)
          --help                  Show this help message

    Seed Format (27 characters):
      Positions 0-8:   Amount of each shape type (0=disabled, 1-9=amount)
                       [Rectangle, Square, Ellipse, Circle, Triangle,
                        Pentagon, Hexagon, Octagon, Hourglass]
      Positions 9-26:  Size pairs for each shape type (2 digits each)

    Example:
      wallpaper-maker -w 3840 -h 2160 -s 330999996666001199999999999 -o my_wallpaper.png
    """);
}
