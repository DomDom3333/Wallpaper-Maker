using WallpaperMaker.Domain;

namespace WallpaperMaker.Tests;

public class NameGeneratorTests
{
    [Fact]
    public void GenerateName_ReturnsNonEmptyString()
    {
        var name = NameGenerator.GenerateName(Enumerable.Empty<string>());
        Assert.False(string.IsNullOrWhiteSpace(name));
    }

    [Fact]
    public void GenerateName_ReturnsUniqueName()
    {
        var existing = new List<string> { "Vibrant Dream", "Neon Sky" };
        var name = NameGenerator.GenerateName(existing);
        
        Assert.DoesNotContain(name, existing);
    }

    [Fact]
    public void GenerateName_HandlesDuplicateConflicts()
    {
        // This is a bit non-deterministic but let's try to force a conflict if we can't easily.
        // Actually, just calling it multiple times should work.
        var names = new HashSet<string>();
        for (int i = 0; i < 10; i++)
        {
            var name = NameGenerator.GenerateName(names);
            Assert.DoesNotContain(name, names);
            names.Add(name);
        }
    }
}
