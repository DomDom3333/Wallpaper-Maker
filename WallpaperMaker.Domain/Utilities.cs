using System.Text.Json;

namespace WallpaperMaker.Domain;

public static class Utilities
{
    private static readonly Random Rng = new();

    public static int RandomNumber(int max, int min = 0)
    {
        if (max <= min) return min;
        return Rng.Next(min, max);
    }

    public static Pallet RandomPalletFromList(List<Pallet> input)
    {
        return input[RandomNumber(input.Count)];
    }

    public static int SmallNumberScaler(int input, int max)
    {
        double step = (double)max / 9;
        return (int)(step * input);
    }

    public static List<int> ShuffledRange(int count)
    {
        var list = Enumerable.Range(0, count).ToList();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        return list;
    }

    public static List<Pallet> UnpackExternalColorPallets()
    {
        string filePath = Path.Combine("Resources", "ColorPallets.json");
        string jsonText = File.ReadAllText(filePath);
        return DeserializePallets(jsonText);
    }

    public static List<Pallet> UnpackUserColorPallets(string jsonText)
    {
        return DeserializePallets(jsonText);
    }

    public static string PackUpUserColorPallets(List<Pallet> toSave)
    {
        var palletEntries = toSave.Select(p => new
        {
            Pallet = new
            {
                p.Name,
                Colors = p.Colors.Select(c => $"{c.Red},{c.Green},{c.Blue}").ToArray()
            }
        }).ToArray();

        var wrapper = new { Pallets = palletEntries };
        return JsonSerializer.Serialize(wrapper, new JsonSerializerOptions { WriteIndented = true });
    }

    private static List<Pallet> DeserializePallets(string jsonText)
    {
        var pallets = new List<Pallet>();
        using var doc = JsonDocument.Parse(jsonText);
        var root = doc.RootElement;

        if (!root.TryGetProperty("Pallets", out var palletsArray))
            return pallets;

        foreach (var entry in palletsArray.EnumerateArray())
        {
            if (!entry.TryGetProperty("Pallet", out var palletObj))
                continue;

            string name = palletObj.GetProperty("Name").GetString() ?? "Unnamed";
            var colors = new List<string>();
            foreach (var color in palletObj.GetProperty("Colors").EnumerateArray())
            {
                colors.Add(color.GetString() ?? "0,0,0");
            }
            pallets.Add(new Pallet(name, colors));
        }
        return pallets;
    }
}
