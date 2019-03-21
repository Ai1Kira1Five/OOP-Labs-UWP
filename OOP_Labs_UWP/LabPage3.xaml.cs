using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OOP_Labs_UWP
{
    [Serializable]
    public class Man : IComparable
    {
        public string Name { get; set; }
        public int Year { get; set; }

        public Man(string name, int year)
        {
            Name = name;
            Year = year;
        }

        public int CompareTo(object obj)
        {
            if (obj is Man man)
                return this.Name.CompareTo(man.Name);
            else
                throw new Exception("Cannot compare this objects...");
        }
    }

    public sealed partial class LabPage3 : Page
    {
        public LabPage3()
        {
            this.InitializeComponent();
            inputBtn.IsEnabled = false;
        }

        BinaryFormatter binF = new BinaryFormatter();
        private static int size = 0, currSize = 0;
        List<Man> manList = new List<Man>(size);
        

        private void CreateSpaceBtn_Click(object sender, RoutedEventArgs e)
        {
            manList.AddRange(Enumerable.Repeat(default(Man), size));
            inputBtn.IsEnabled = true;
            lb3ProgressBar.Maximum = size;
            lb3slider.IsEnabled = false;
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            size = Convert.ToInt32(e.NewValue);
            progressBarAll.Text = Convert.ToString(size);
        }

        private void SerializeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SortRb.IsChecked == true)
            {
                manList.Sort();
                serTextBlock.Text = "sort done";
                Serializer();
            }
            else if (NonSortRb.IsChecked == true)
                Serializer();


        }

        private void InputBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = tb_Name.Text;
            int year = Convert.ToInt32(tb_Year.Text);

            Man newMan = new Man(name, year);

            progressBarCurr.Text = Convert.ToString(++currSize);
            lb3ProgressBar.Value = currSize;

            manList.Add(newMan);

            if (currSize == size) inputBtn.IsEnabled = false;
        }

        public void Serializer()
        {
            using (Stream fs = File.Open("ms-appx:///files/man_list.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                binF.Serialize(fs, manList);
                serTextBlock.Text = "done";
            }
        }
    }
}
