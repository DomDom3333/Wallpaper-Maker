using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Json.Net;
using System.IO;
using WallpaperMaker.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WallpaperMaker.Properties;

class Utils
{
    internal static int grabXRes()
    {
        Rectangle Monitor = Screen.PrimaryScreen.Bounds;
        return Monitor.Width;
    }
    internal static int grabYRes()
    {
        Rectangle Monitor = Screen.PrimaryScreen.Bounds;
        return Monitor.Height;
    }
    internal static int RandomNumber(int max, int min = 0)
    {
        Random rand = new Random();
        return rand.Next(min,max); ;
    }

    internal static Pallet RandomPalletFromList(List<Pallet> input)
    {
        return input[RandomNumber(input.Count)];
    }
    internal static int smallNumberScaler(int input, int max)
    {
        double step = max / 9;

        return (int)(step * input);
    }

    internal static List<int> listOfRandomSequentialNumbers(int length, bool startAtZero = false)
    {
        int generatedNumber = 0;
        List<int> returnList = new List<int> { };
        while (true)
        {
            generatedNumber = RandomNumber(length,1);
            if (!returnList.Contains(generatedNumber))
            {
                returnList.Add(generatedNumber);
            }
            if (returnList.Count == 4)
            {
                break;
            }
        }
        return returnList;
    }
    internal static List<Pallet> UnpackExternalColorPallets()
    {
        string filePath = Path.Combine("Resources", "ColorPallets.json");
        string jsonText = "";
        using (StreamReader r = new StreamReader(filePath))
        {
            jsonText = r.ReadToEnd();
        }

        JObject jObject = JObject.Parse(jsonText);
        JToken[] jPallets = jObject["Pallets"].ToArray();
        JToken finalPallet;

        string palletName = "";
        List<string> palletColors = new List<string> { };
        List<Pallet> loadedPallets = new List<Pallet> { };


        foreach (JToken x in jPallets)
        {
            finalPallet = x["Pallet"];
            palletName = (string)finalPallet["Name"];
            foreach (string finalColor in finalPallet["Colors"].ToArray())
            {
                palletColors.Add(finalColor);
            }
            loadedPallets.Add(new Pallet(palletName, palletColors.ToArray()));
        }
        return loadedPallets;
    }
    internal static List<Pallet> UnpackUserColorPallets(string jsonText)
    {
        JObject jObject = JObject.Parse(jsonText);
        JToken[] jPallets = jObject["Pallets"].ToArray();
        JToken finalPallet;

        string palletName = "";
        List<string> palletColors = new List<string> { };
        List<Pallet> loadedPallets = new List<Pallet> { };


        foreach (JToken x in jPallets)
        {
            finalPallet = x["Pallet"];
            palletName = (string)finalPallet["Name"];
            foreach (string finalColor in finalPallet["Colors"].ToArray())
            {
                palletColors.Add(finalColor);
            }
            loadedPallets.Add(new Pallet(palletName, palletColors.ToArray()));
            palletColors = new List<string> { };
        }
        return loadedPallets;
    }
    internal static string PackUpUserColorPallets(List<Pallet> toSave)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();
            writer.WritePropertyName("Pallets");
            writer.WriteStartArray();
            foreach (Pallet item in toSave)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Pallet");
                writer.WriteStartObject();
                writer.WritePropertyName("Name");
                writer.WriteValue(item.Name);
                writer.WritePropertyName("Colors");
                writer.WriteStartArray();
                foreach (List<int> intList in item.Colors)
                {
                    if (intList.Count > 3)
                    {
                        writer.WriteValue($"{intList[1]},{intList[2]},{intList[3]}");
                    }
                    else
                    {
                        writer.WriteValue($"{intList[0]},{intList[1]},{intList[2]}");
                    }
                }
                writer.WriteEnd();
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        string output = sb.ToString();
        return output;
    }
}

