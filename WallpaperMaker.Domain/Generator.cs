using System.Drawing;
using System.Drawing.Drawing2D;

namespace WallpaperMaker.Domain
{
    public class Generator
    {
        private int xRes { get; set; } = 1920;
        private int yRes { get; set; } = 1080;
        private int supersampling { get; set; }
        private Graphics g;
        private Pallet colorPalletToUse { get; set; }
        private Bitmap workingImage { get; set; }
        private ElementAgregator Maker;


        public Generator(Pallet goalColor, int horRes, int vertRes, int MS)
        {
            supersampling = MS;

            if(supersampling == 0)
            {
                xRes = horRes;
                yRes = vertRes;
                workingImage = new Bitmap(xRes, yRes);
                workingImage.SetResolution(xRes, yRes);
            }
            else
            {
                xRes = horRes * supersampling;
                yRes = vertRes * supersampling;
                workingImage = new Bitmap(xRes, yRes);
                workingImage.SetResolution(xRes / supersampling, yRes / supersampling);
            }
            g = Graphics.FromImage(workingImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            colorPalletToUse = goalColor;
        }

        public Bitmap Generate(string seed)
        {
            GenerateShapes(seed);
            DrawAll();
            return workingImage;
        }

        ~Generator()
        {
            Maker = null;
            workingImage.Dispose();
            g.Dispose(); 
        }

        private void GenerateShapes(string seed)
        {
            Size targetRes = new Size(xRes, yRes);
            Maker = new ElementAgregator(seed, targetRes);
            if (seed.Contains('0'))
            {
                bool[] boolList = new bool[9];
                for (int i = 0; i < 9; i++)
                {
                    if (seed[i] == '0')
                    {
                        boolList[i] = false;
                    }
                    else
                    {
                        boolList[i] = true;
                    }
                }
                Maker.MakeSome(boolList);
            }
            else
            {
                Maker.MakeAll();
            }
        }

        private void DrawAll()
        {
            var mainBrush = new SolidBrush(colorPalletToUse.RandomPalletElement());
            var rec = new Rectangle(0, 0, xRes, yRes);
            g.FillRectangle(mainBrush, rec);

            var drawOrder = Utilities.listOfRandomSequentialNumbers(5);
            try
            {
                foreach (int turn in drawOrder)
                {
                    switch (turn)
                    {
                        case 1:
                            if (Maker.Rectangles == null) break;
                            foreach (var shapeToDraw in Maker.Rectangles)
                            {
                                mainBrush.Color = colorPalletToUse.RandomPalletElement();
                                DrawRec(shapeToDraw.Rectangles, mainBrush, shapeToDraw.rotation);
                            }
                            break;
                        case 2:
                            if (Maker.Squares == null) break;
                            foreach (var shapeToDraw in Maker.Squares)
                            {
                                mainBrush.Color = colorPalletToUse.RandomPalletElement();
                                DrawSquares(shapeToDraw.Squares, mainBrush, shapeToDraw.rotation);
                            }
                            break;
                        case 3:
                            if (Maker.Ellipsies == null) break;
                            foreach (var shapeToDraw in Maker.Ellipsies)
                            {
                                mainBrush.Color = colorPalletToUse.RandomPalletElement();
                                DrawEllis(shapeToDraw.Ellipsies, mainBrush, shapeToDraw.rotation);
                            }
                            break;
                        case 4:
                            if (Maker.Circles == null) break;
                            foreach (var shapeToDraw in Maker.Circles)
                            {
                                mainBrush.Color = colorPalletToUse.RandomPalletElement();
                                DrawCircles(shapeToDraw.Circles, mainBrush, shapeToDraw.rotation);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            catch
            {
                //TODO: Empty catch blocks are a code-smell
            }
            mainBrush.Dispose();
            g.Dispose();
            //run through lists of Maker to draw all elements. Each type in their own method
        }

        private void DrawRec(Rectangle rec, Brush brush, int rotation)
        {
            g.RotateTransform(rotation);
            g.FillRectangle(brush, rec);
        }

        private void DrawSquares(Rectangle rec, Brush brush, int rotation)
        {
            g.RotateTransform(rotation);
            g.FillRectangle(brush, rec);
        }

        private void DrawEllis(Rectangle rec, Brush brush, int rotation)
        {
            g.RotateTransform(rotation);
            g.FillEllipse(brush, rec);
        }

        private void DrawCircles(Rectangle rec, Brush brush, int rotation)
        {
            g.RotateTransform(rotation);
            g.FillEllipse(brush, rec);
        }
    }
}
