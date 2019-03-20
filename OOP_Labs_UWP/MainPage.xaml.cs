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
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace OOP_Labs_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            myFrame.Navigate(typeof(LabPage1));
            textHeader.Text = "Lab 1";
        }

        private void gamburgerBtn_Click(object sender, RoutedEventArgs e)
        {
            mySplit.IsPaneOpen = !mySplit.IsPaneOpen;
        }

        private void listBoxButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb1Item.IsSelected)
            {
                myFrame.Navigate(typeof(LabPage1));
                textHeader.Text = "Lab 1";
            }
            if (lb2Item.IsSelected)
            {
                myFrame.Navigate(typeof(LabPage2));
                textHeader.Text = "Lab 2";
            }
            if (lb3Item.IsSelected)
            {
                myFrame.Navigate(typeof(LabPage3));
                textHeader.Text = "Lab 3";
            }
            if (lb4Item.IsSelected)
            {
                myFrame.Navigate(typeof(LabPage4));
                textHeader.Text = "Lab 4";
            }
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(typeof(LabPage1));
            textHeader.Text = "Lab 1";
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
