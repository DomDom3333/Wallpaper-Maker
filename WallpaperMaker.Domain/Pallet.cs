using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static WallpaperMaker.Domain.Utilities;
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

        public void Rename(string newName)
        {
            Name = newName;
        }

        public Color RandomPalletElement()
        {
            List<int> chosenColor = Colors[RandomNumber(Colors.Count)];
            if (chosenColor.Count > 3)
            {
                return Color.FromArgb(255, chosenColor[1], chosenColor[2], chosenColor[3]);
            }
            else
            {
                return Color.FromArgb(255, chosenColor[0], chosenColor[1], chosenColor[2]);
            }
        }

        public void addColor(Color toAdd)
        {
            Colors.Add(new List<int> { 255, toAdd.R, toAdd.G, toAdd.B });
        }

        public List<int> findColorList(string colors)
        {
            string[] stringToFind = colors.Split(',');
            foreach (List<int> lists in Colors)
            {
                if (lists.Count > 3)
                {
                    if (Int16.Parse(stringToFind[0]) != lists[1])
                    {
                        continue;
                    }
                    if (Int16.Parse(stringToFind[1]) != lists[2])
                    {
                        continue;
                    }
                    if (Int16.Parse(stringToFind[2]) != lists[3])
                    {
                        continue;
                    }
                    return lists;
                }
                else
                {
                    if (Int16.Parse(stringToFind[0]) != lists[0])
                    {
                        continue;
                    }
                    if (Int16.Parse(stringToFind[1]) != lists[1])
                    {
                        continue;
                    }
                    if (Int16.Parse(stringToFind[2]) != lists[2])
                    {
                        continue;
                    }
                    return lists;
                }
            }
            return null;
        }
    }
}
