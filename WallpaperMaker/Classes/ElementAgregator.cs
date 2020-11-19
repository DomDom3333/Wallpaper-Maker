using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils;

namespace WallpaperMaker.Classes
{
    class ElementAgregator
    {
        internal List<Shape> Rectangles { get; private set; }
        internal List<Shape> Squares { get; private set; }
        internal List<Shape> Ellipsies { get; private set; }
        internal List<Shape> Circles { get; private set; }
        internal List<Shape> Triangles { get; private set; }
        internal List<Shape> Pentagons { get; private set; }
        internal List<Shape> Hexagons { get; private set; }
        internal List<Shape> Ochtagons { get; private set; }
        internal List<Shape> Hourglasses { get; private set; }

        private int amoutRecs { get;  set; }
        private int amoutSquare { get;  set; }
        private int amoutElls { get;  set; }
        private int amoutCircs { get;  set; }
        private int amoutTris { get;  set; }
        private int amoutPents { get;  set; }
        private int amoutHexs { get;  set; }
        private int amoutOchs { get;  set; }
        private int amoutHours { get;  set; }
        private Size sizeRecs { get;  set; }
        private Size sizeSquare { get;  set; }
        private Size sizeElls { get;  set; }
        private Size sizeCircs { get;  set; }
        private Size sizeTris { get;  set; }
        private Size sizePents { get;  set; }
        private Size sizeHexs { get;  set; }
        private Size sizeOchs { get;  set; }
        private Size sizeHours { get;  set; }

        public int XResolution { get; set; } = grabXRes();
        public int YResolution { get; set; } = grabYRes();
        private int maxNumberOfElements { get; set; } = 50;

        internal ElementAgregator(string seed, Size targetRes)
        {
            if(seed.Length < 27)
            {
                return;
            }
            amoutRecs = Int16.Parse(seed[0].ToString());
            amoutSquare = Int16.Parse(seed[1].ToString());
            amoutElls = Int16.Parse(seed[2].ToString());
            amoutCircs = Int16.Parse(seed[3].ToString());
            amoutTris = Int16.Parse(seed[4].ToString());
            amoutPents = Int16.Parse(seed[5].ToString());
            amoutHexs = Int16.Parse(seed[6].ToString());
            amoutOchs = Int16.Parse(seed[7].ToString());
            amoutHours = Int16.Parse(seed[8].ToString());

            sizeRecs = new Size(Int16.Parse(seed[9].ToString()), Int16.Parse(seed[10].ToString()));
            sizeSquare = new Size(Int16.Parse(seed[11].ToString()), Int16.Parse(seed[12].ToString()));
            sizeElls = new Size(Int16.Parse(seed[13].ToString()), Int16.Parse(seed[14].ToString()));
            sizeCircs = new Size(Int16.Parse(seed[15].ToString()), Int16.Parse(seed[16].ToString()));
            sizeTris = new Size(Int16.Parse(seed[17].ToString()), Int16.Parse(seed[18].ToString()));
            sizePents = new Size(Int16.Parse(seed[19].ToString()), Int16.Parse(seed[20].ToString()));
            sizeHexs = new Size(Int16.Parse(seed[21].ToString()), Int16.Parse(seed[22].ToString()));
            sizeOchs = new Size(Int16.Parse(seed[23].ToString()), Int16.Parse(seed[24].ToString()));
            sizeHours = new Size(Int16.Parse(seed[25].ToString()), Int16.Parse(seed[26].ToString()));

            XResolution = targetRes.Width;
            YResolution = targetRes.Height;

        }

        internal void MakeAll()
        {
            Task.Run(() => MakeRecs(smallNumberScaler(amoutRecs,maxNumberOfElements),sizeRecs)); //errors here since it calls random number with (9,9). Numbers need to be converted to % of screeen res of sort (scaled?)
            Task.Run(() => MakeSquares(smallNumberScaler(amoutSquare,maxNumberOfElements), sizeSquare));
            Task.Run(() => MakeElls(smallNumberScaler(amoutElls,maxNumberOfElements), sizeElls));
            Task.Run(() => MakeCircs(smallNumberScaler(amoutCircs,maxNumberOfElements), sizeCircs));

            Task.WhenAll();
        }
        internal void MakeSome(bool Rec, bool Ell, bool Cir, bool Tri, bool Pent, bool Hex, bool Och, bool Hour)
        {

        }




        private void MakeRecs(int amout, Size maxSize)//Sample Creating function
        {
            Rectangles = new List<Shape> { };
            int xRes = XResolution;
            int yRes = YResolution;
            int rotation = 0;
            Rectangle rec = new Rectangle(0, 0, xRes, yRes);

            for (int i = 0; i <= amout; i++)
            {
                rotation = RandomNumber(90);
                rec.X = RandomNumber(xRes - (xRes / 10), xRes / 10);
                rec.Y = RandomNumber(yRes - (yRes / 10), yRes / 100);
                rec.Width = RandomNumber(smallNumberScaler(maxSize.Width,xRes));
                rec.Height = RandomNumber(smallNumberScaler(maxSize.Height, xRes));
                Rectangles.Add(new Shape(rotation, "Rectangle", rec));
            }
        }
        private void MakeSquares(int amout, Size maxSize)
        {
            Squares = new List<Shape> { };
            int xRes = XResolution;
            int yRes = YResolution;
            int size = RandomNumber(smallNumberScaler(maxSize.Width, xRes));
            int rotation = 00;
            Rectangle rec = new Rectangle(0, 0, xRes, yRes);

            for (int i = 0; i <= amout; i++)
            {
                rotation = RandomNumber(90);
                rec.X = RandomNumber(xRes - (xRes / 10), xRes / 10);
                rec.Y = RandomNumber(yRes - (yRes / 10), yRes / 100);
                rec.Width = size;
                rec.Height = size;
                Squares.Add(new Shape(rotation, "Square", rec));
            }
        }
        private void MakeElls(int amout, Size maxSize)
        {
            Ellipsies = new List<Shape> { };
            int xRes = XResolution;
            int yRes = YResolution;
            int rotation = 0;
            Rectangle rec = new Rectangle(0, 0, xRes, yRes);

            for (int i = 0; i <= amout; i++)
            {
                rotation = RandomNumber(90);
                rec.X = RandomNumber(xRes - (xRes / 10), xRes / 10);
                rec.Y = RandomNumber(yRes - (yRes / 10), yRes / 100);
                rec.Width = RandomNumber(smallNumberScaler(maxSize.Width, xRes));
                rec.Height = RandomNumber(smallNumberScaler(maxSize.Height, xRes));
                Ellipsies.Add(new Shape(rotation, "Ellipse", rec));
            }
        }
        private void MakeCircs(int amout, Size maxSize)
        {
            Circles = new List<Shape> { };
            int xRes = XResolution;
            int yRes = YResolution;
            int size = RandomNumber(smallNumberScaler(maxSize.Width, xRes));
            int rotation = 0;
            Rectangle rec = new Rectangle(0, 0, xRes, yRes);

            for (int i = 0; i <= amout; i++)
            {
                rotation = RandomNumber(90);
                rec.X = RandomNumber(xRes - (xRes / 10), xRes / 10);
                rec.Y = RandomNumber(yRes - (yRes / 10), yRes / 100);
                rec.Width = size;
                rec.Height = size;
                Circles.Add(new Shape(rotation, "Circle", rec));
            }
        }
        private void MakeTris(int amout, Size maxSize)
        {

        }
        private void MakePents(int amout, Size maxSize)
        {

        }
        private void MakeHexs(int amout, Size maxSize)
        {

        }
        private void MakeOchs(int amout, Size maxSize)
        {

        }
        private void makeHours(int amout, Size maxSize)
        {

        }        
    }
}
