using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace OOP_Labs_UWP
{
    public sealed partial class LabPage2 : Page
    {
        public LabPage2()
        {
            this.InitializeComponent();
        }

        private string typeOutput;

        private class ManO
        {
            private double X { get; set; }
            private double Y { get; set; }
            public string Link { get; set; }

            public ManO(string url, double x, double y)
            {
                Link = url;
                X = x;
                Y = y;
            }

            public virtual void Print(ref Canvas onPageCanvas, double imHeight, double imWidth)
            {
                Image newImage = new Image();
                BitmapImage image = new BitmapImage(new Uri(Link));
                newImage.Source = image;

                newImage.Height = imHeight;
                newImage.Width = imWidth;
                newImage.Margin = new Thickness(X - (imWidth / 2), Y, 0, 0);
                onPageCanvas.Children.Add(newImage);
            }
        }

        private class SchoolO : ManO
        {
            public SchoolO(string url, double x, double y) : base(url, x, y) { }

            public override void Print(ref Canvas onPageCanvas, double imHeight, double imWidth)
            {
                base.Print(ref onPageCanvas, imHeight, imWidth);
            }
        }

        private class StudentO : SchoolO
        {
            public StudentO(string url, double x, double y) : base(url, x, y) { }

            public override void Print(ref Canvas onPageCanvas, double imHeight, double imWidth)
            {
                base.Print(ref onPageCanvas, imHeight, imWidth);
            }
        }

        private class ZaochO
        {
            private double X { get; set; }
            private double Y { get; set; }
            public string Link { get; set; }

            private readonly StudentO st;

            public ZaochO(string url, double x, double y)
            {
                Link = url;
                X = x; Y = y;
                string stUrl = "ms-appx:///Assets/idiot.gif";
                this.st = new StudentO(stUrl, x - 100, y);
            }

            public void Print(ref Canvas onPageCanvas, double imHeight, double imWidth)
            {
                st.Print(ref onPageCanvas, imHeight, imWidth);
                Image newImage = new Image();
                Image newChImg = new Image();
                BitmapImage image = new BitmapImage(new Uri(Link));
                BitmapImage chImg = new BitmapImage(new Uri("ms-appx:///Assets/chain.png"));

                newImage.Source = image;
                newChImg.Source = chImg;

                newImage.Height = imHeight;
                newImage.Width = imWidth;

                newChImg.Height = 15;
                newChImg.Width = 90;

                newImage.Margin = new Thickness(X , Y, 0, 0);
                newChImg.Margin = new Thickness((X - (imWidth / 3.1)), (Y + (imHeight/2)), 0, 0);
                onPageCanvas.Children.Add(newImage);
                onPageCanvas.Children.Add(newChImg);
            }
        }

        private void OnPageCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            string tp = "ms-appx:///Assets/";
            var x = e.GetCurrentPoint(onPageCanvas).Position.X;
            var y = e.GetCurrentPoint(onPageCanvas).Position.Y;
            
            var a = (sender as Canvas);

            double imH, imW;

            ManO mn; SchoolO sc; StudentO st; ZaochO zc;

            switch (typeOutput)
            {
                case "man":
                    tp += "man.png";
                    imH = 120; imW = 120;
                    mn = new ManO(tp, x, y);
                    mn.Print(ref a, imH, imW);
                    break;
                case "school":
                    tp += "school.jpg";
                    sc = new SchoolO(tp, x, y);
                    sc.Print(ref a, 120, 120);
                    break;
                case "student":
                    tp += "idiot.gif";
                    imH = 120; imW = 1.77 * imH;
                    st = new StudentO(tp, x, y);
                    st.Print(ref a, imH, imW);
                    break;
                case "zaoch":
                    tp += "zaoch.gif";
                    zc = new ZaochO(tp, x, y);
                    imH = 120; imW = 1.33 * imH;
                    zc.Print(ref a, imH, imW);
                    break;
                default:
                    tp += "test.png";
                    mn = new ManO(tp, x, y);
                    mn.Print(ref a, 50, 50);
                    break;
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            string type = ((Button)sender).Name;

            switch (type)
            {
                case "Man":
                    typeOutput = "man";
                    break;
                case "School":
                    typeOutput = "school";
                    break;
                case "Student":
                    typeOutput = "student";
                    break;
                case "Zaoch":
                    typeOutput = "zaoch";
                    break;
            }            
        }
    }
}
