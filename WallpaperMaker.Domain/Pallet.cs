using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WallpaperMaker.Domain
{
    public class Pallet
    {
        public string Name { get; private set; }
        public List<List<int>> Colors { get; private set; }

        public Pallet(string newName, string[] colorList)
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
            List<int> chosenPallet = Colors[Utilities.RandomNumber(Colors.Count)];
            if (chosenPallet.Count > 3)
            {
                return Color.FromArgb(255, chosenPallet[1], chosenPallet[2], chosenPallet[3]);
            }
            else
            {
                return Color.FromArgb(255, chosenPallet[0], chosenPallet[1], chosenPallet[2]);
            }
        }

        public void addColor(Color toAdd)
        {
            Colors.Add(new List<int> { 255, toAdd.R, toAdd.G, toAdd.B });
        }
    }
}
