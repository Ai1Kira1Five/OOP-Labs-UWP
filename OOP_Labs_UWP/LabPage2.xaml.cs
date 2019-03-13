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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace OOP_Labs_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LabPage2 : Page
    {
        public LabPage2()
        {
            this.InitializeComponent();
        }

        private string typeOutput;

        private void onPageCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            string tp = "ms-appx:///Assets/";
            var x = e.GetCurrentPoint(onPageCanvas).Position.X;
            var y = e.GetCurrentPoint(onPageCanvas).Position.Y;

            var bh = ((Canvas)sender).Height;
            var bw = ((Canvas)sender).Width;

            Image newImage = new Image();

            switch (typeOutput)
            {
                case "man":
                    tp += "man.png";
                    newImage.Height = 120;
                    newImage.Width = 120;
                    break;
                case "school":
                    tp += "school.jpg";
                    newImage.Height = 120;
                    newImage.Width = 120;
                    break;
                case "student":
                    tp += "idiot.gif";
                    newImage.Height = 100;
                    newImage.Width = newImage.Height * 1.78;
                    break;
                case "zaoch":
                    tp += "zaoch.gif";
                    newImage.Height = 100;
                    newImage.Width = newImage.Height * 1.33;
                    break;
                default:
                    tp += "test.png";
                    newImage.Height = 48;
                    newImage.Width = 48;
                    break;
            }

            BitmapImage image = new BitmapImage(new Uri(tp));
            newImage.Source = image;

            newImage.Margin = (bw - newImage.Width) > y ? new Thickness(x - newImage.Width, y, 0, 0) : new Thickness(x, y, 0, 0);

            onPageCanvas.Children.Add(newImage);
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
