using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils;

namespace WallpaperMaker.Classes
{
    class Pallet
    {
        internal string Name { get; private set; }
        internal List<List<int>> Colors { get; private set; }

        internal Pallet(string newName, string[] colorList)
        {
            Colors = new List<List<int>> { };


            Name = newName;
            foreach (string color in colorList)
            {
                List<int> RGB = new List<int> { };
                string[] RGBValsString = color.Split(",").ToArray();
                foreach (string value in RGBValsString)
                {
                    RGB.Add(Int32.Parse(value));
                }
                Colors.Add(RGB);
            }
        }

        internal Color RandomPalletElement()
        {
            List<int> chosenPallet = Colors[RandomNumber(Colors.Count)];
            if (chosenPallet.Count > 3)
            {
                return Color.FromArgb(255, chosenPallet[1], chosenPallet[2], chosenPallet[3]);
            }
            else
            {
                return Color.FromArgb(255, chosenPallet[0], chosenPallet[1], chosenPallet[2]);
            }
        }
        internal void addColor(Color toAdd)
        {
            Colors.Add(new List<int> { 255, toAdd.R, toAdd.G, toAdd.B });
        }
    }
}
