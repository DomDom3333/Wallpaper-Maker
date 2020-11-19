using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Utils;

namespace WallpaperMaker.Classes
{
    class Generator
    {

        private int xRes { get; set; } = 1920;
        private int yRes { get; set; } = 1080;
        private int supersampling { get; set; }
        private Graphics g;
        private Pallet colorPalletToUse { get; set; }
        private Bitmap workingImage { get; set; }
        private ElementAgregator Maker;
        internal Generator(Pallet goalColor, int horRes, int vertRes, int MS)
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

        internal Bitmap Generate()
        {

            GenerateShapes();
            DrawAll();



            return workingImage;
        }

        private void GenerateShapes()
        {
            Size targetRes = new Size(xRes, yRes);
            Maker = new ElementAgregator("330999996666001199999999999", targetRes);
            Maker.MakeAll();
        }
        private void DrawAll()
        {
            SolidBrush mainBrush = new SolidBrush(colorPalletToUse.RandomPalletElement());
            Rectangle rec = new Rectangle(0, 0, xRes, yRes);
            g.FillRectangle(mainBrush, rec);

            List<int> drawOrder = listOfRandomSequentialNumbers(5);

            foreach (int turn in drawOrder)
            {
                switch (turn)
                {
                    case 1:
                        foreach (Shape shapeToDraw in Maker.Rectangles)
                        {
                            mainBrush.Color = colorPalletToUse.RandomPalletElement();
                            DrawRec(shapeToDraw.Rectangles, mainBrush, shapeToDraw.rotation);
                        }
                        break;
                    case 2:
                        foreach (Shape shapeToDraw in Maker.Squares)
                        {
                            mainBrush.Color = colorPalletToUse.RandomPalletElement();
                            DrawSquares(shapeToDraw.Squares, mainBrush, shapeToDraw.rotation);
                        }
                        break;
                    case 3:
                        foreach (Shape shapeToDraw in Maker.Ellipsies)
                        {
                            mainBrush.Color = colorPalletToUse.RandomPalletElement();
                            DrawEllis(shapeToDraw.Ellipsies, mainBrush, shapeToDraw.rotation);
                        }
                        break;
                    case 4:
                        foreach (Shape shapeToDraw in Maker.Circles)
                        {
                            mainBrush.Color = colorPalletToUse.RandomPalletElement();
                            DrawCircles(shapeToDraw.Circles, mainBrush, shapeToDraw.rotation);
                        }
                        break;
                    default:
                        break;
                }
                    
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
