using System.Drawing;

namespace WallpaperMaker.Domain
{
    public class Shape
    {
        internal int xPos { get; private set; }
        internal int yPos { get; private set; }
        internal int rotation { get; private set; }
        internal int xSize { get; private set; }
        internal int ySize { get; private set; }
        internal string type { get; private set; }
        internal Rectangle Rectangles { get; private set; }
        internal Rectangle Squares { get; private set; }
        internal Rectangle Ellipsies { get; private set; }
        internal Rectangle Circles { get; private set; }
        internal Point[] Triangles { get; private set; }
        internal Point[] Pentagons { get; private set; }
        internal Point[] Hexagons { get; private set; }
        internal Point[] Ochtagons { get; private set; }
        internal Point[] Hourglasses { get; private set; }

        internal Shape(int rot, string Type, Rectangle rec)
        {
            rotation = rot;
            type = Type;
            switch (type)
            {
                case "Rectangle":
                    Rectangles = rec;
                    break;
                case "Square":
                    Squares = rec;
                    break;
                case "Ellipse":
                    Ellipsies = rec;
                    break;
                case "Circle":
                    Circles = rec;
                    break;
                default:
                    break;
            }
        }

        internal Shape(int rot, string Type, Point[] poly)
        {
            rotation = rot;
            type = Type;
            switch (type)
            {
                case "Triangle":
                    Triangles = poly;
                    break;
                case "Pentagons":
                    Pentagons = poly;
                    break;
                case "Hexagons":
                    Hexagons = poly;
                    break;
                case "Ochtagons":
                    Ochtagons = poly;
                    break;
                case "Hourglasses":
                    Hourglasses = poly;
                    break;
                default:
                    break;
            }
        }
    }
}
