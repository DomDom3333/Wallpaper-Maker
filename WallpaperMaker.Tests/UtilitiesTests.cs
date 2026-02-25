using WallpaperMaker.Domain;

namespace WallpaperMaker.Tests;

public class UtilitiesTests
{
    [Fact]
    public void RandomNumber_ReturnsWithinRange()
    {
        for (int i = 0; i < 100; i++)
        {
            int result = Utilities.RandomNumber(10, 5);
            Assert.InRange(result, 5, 9);
        }
    }

    [Fact]
    public void RandomNumber_ReturnsMinWhenMaxEqualsMin()
    {
        int result = Utilities.RandomNumber(5, 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void RandomNumber_ReturnsMinWhenMaxLessThanMin()
    {
        int result = Utilities.RandomNumber(3, 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void SmallNumberScaler_ScalesCorrectly()
    {
        // step = 100/9 = 11.11, input 5 => 55
        int result = Utilities.SmallNumberScaler(5, 100);
        Assert.Equal(55, result);
    }

    [Fact]
    public void SmallNumberScaler_ZeroInput_ReturnsZero()
    {
        int result = Utilities.SmallNumberScaler(0, 100);
        Assert.Equal(0, result);
    }

    [Fact]
    public void SmallNumberScaler_MaxInput_ReturnsMax()
    {
        int result = Utilities.SmallNumberScaler(9, 900);
        Assert.Equal(900, result);
    }

    [Fact]
    public void ShuffledRange_ContainsAllNumbers()
    {
        var result = Utilities.ShuffledRange(10);
        Assert.Equal(10, result.Count);
        for (int i = 0; i < 10; i++)
            Assert.Contains(i, result);
    }

    [Fact]
    public void ShuffledRange_NoDuplicates()
    {
        var result = Utilities.ShuffledRange(10);
        Assert.Equal(result.Count, result.Distinct().Count());
    }

    [Fact]
    public void PackAndUnpackPallets_RoundTrips()
    {
        var original = new List<Pallet>
        {
            new Pallet("Test", new[] { "255,0,0", "0,255,0", "0,0,255" }),
            new Pallet("Other", new[] { "128,128,128" })
        };

        string json = Utilities.PackUpUserColorPallets(original);
        var restored = Utilities.UnpackUserColorPallets(json);

        Assert.Equal(2, restored.Count);
        Assert.Equal("Test", restored[0].Name);
        Assert.Equal(3, restored[0].Colors.Count);
        Assert.Equal("Other", restored[1].Name);
        Assert.Single(restored[1].Colors);
    }

    [Fact]
    public void UnpackUserColorPallets_ParsesDefaultSettings()
    {
        string json = """
        {
          "Pallets": [
            {
              "Pallet": {
                "Name": "Sample",
                "Colors": ["229,244,227", "93,169,233"]
              }
            }
          ]
        }
        """;

        var pallets = Utilities.UnpackUserColorPallets(json);
        Assert.Single(pallets);
        Assert.Equal("Sample", pallets[0].Name);
        Assert.Equal(2, pallets[0].Colors.Count);
    }

    [Fact]
    public void RandomPalletFromList_ReturnsValidPallet()
    {
        var pallets = new List<Pallet>
        {
            new Pallet("A", new[] { "1,2,3" }),
            new Pallet("B", new[] { "4,5,6" })
        };

        for (int i = 0; i < 50; i++)
        {
            var result = Utilities.RandomPalletFromList(pallets);
            Assert.Contains(result, pallets);
        }
    }
}
