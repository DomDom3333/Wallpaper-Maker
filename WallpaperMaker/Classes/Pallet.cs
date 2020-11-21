using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        internal void Rename(string newName)
        {
            Name = newName;
        }

        internal Color RandomPalletElement()
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
        internal void addColor(Color toAdd)
        {
            Colors.Add(new List<int> { 255, toAdd.R, toAdd.G, toAdd.B });
        }

        internal List<int> findColorList(string colors)
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
