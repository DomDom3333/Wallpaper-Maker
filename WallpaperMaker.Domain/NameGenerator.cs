namespace WallpaperMaker.Domain;

public static class NameGenerator
{
    private static readonly Random Rng = new();

    private static readonly string[] Adjectives =
    {
        "Vibrant", "Neon", "Electric", "Mellow", "Soft", "Bold", "Dusty", "Cosmic", "Lunar", "Solar",
        "Deep", "Light", "Bright", "Dark", "Sweet", "Salty", "Bitter", "Fresh", "Ancient", "Modern",
        "Silent", "Loud", "Fast", "Slow", "Warm", "Cold", "Hot", "Icy", "Golden", "Silver",
        "Crimson", "Azure", "Emerald", "Indigo", "Velvet", "Satin", "Rusty", "Shiny", "Matte", "Glossy"
    };

    private static readonly string[] Nouns =
    {
        "Dream", "Sky", "Ocean", "Forest", "Mountain", "River", "Desert", "Canyon", "Valley", "Meadow",
        "Star", "Moon", "Sun", "Planet", "Nebula", "Galaxy", "Universe", "Space", "Time", "Light",
        "Shadow", "Mist", "Fog", "Rain", "Snow", "Wind", "Storm", "Fire", "Ice", "Stone",
        "Wood", "Metal", "Glass", "Sand", "Dust", "Ash", "Leaf", "Flower", "Tree", "Seed"
    };

    public static string GenerateName(IEnumerable<string> existingNames)
    {
        var existingSet = new HashSet<string>(existingNames, StringComparer.OrdinalIgnoreCase);
        
        for (int i = 0; i < 100; i++)
        {
            string adj = Adjectives[Rng.Next(Adjectives.Length)];
            string noun = Nouns[Rng.Next(Nouns.Length)];
            string candidate = $"{adj} {noun}";
            
            if (!existingSet.Contains(candidate))
                return candidate;
        }

        // Fallback with a number if we're extremely unlucky
        string baseName = $"{Adjectives[Rng.Next(Adjectives.Length)]} {Nouns[Rng.Next(Nouns.Length)]}";
        int counter = 2;
        while (existingSet.Contains($"{baseName} {counter}"))
        {
            counter++;
        }
        return $"{baseName} {counter}";
    }
}
