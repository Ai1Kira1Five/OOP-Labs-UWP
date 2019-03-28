using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalSerializerLib3;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace OOP_Labs_UWP
{
    public class Man : IComparable
    {
        [ForceSerialize]
        public string Name { get; set; }
        [ForceSerialize]
        public int Year { get; set; }

        public Man(string name, int year)
        {
            this.Name = name;
            this.Year = year;
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

        private static int size = 0, currSize = 0;
        private ulong streamSize;
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

        private async void SerializeBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (SortRb.IsChecked == true) manList.Sort();

            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            

            StorageFolder fl = await ApplicationData.Current.LocalFolder.CreateFolderAsync("LabFiles", CreationCollisionOption.OpenIfExists);
            StorageFile serFile = await fl.GetFileAsync("man_list.txt");
            
            var stream = await serFile.OpenAsync(FileAccessMode.ReadWrite);
            streamSize = stream.Size;

            using (var s = new UniversalSerializer(stream.AsStream(), SerializerFormatters.BinarySerializationFormatter))
            {
                s.Serialize(manList);
            }

            sw.Stop();
            string txt = " ";

            using (var inputStream = await serFile.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                while (!streamReader.EndOfStream)
                {
                    txt = streamReader.ReadLine();
                }
            }

            stream.Dispose();
            serTextBlock.Text = txt;
            serializedTime.Text = Convert.ToString(Math.Truncate(sw.Elapsed.TotalMilliseconds)); 
        }

        private async void DeserializeBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            StorageFolder fl = await ApplicationData.Current.LocalFolder.CreateFolderAsync("LabFiles", CreationCollisionOption.OpenIfExists);
            StorageFile serFile = await fl.GetFileAsync("man_list.txt");

            var stream = await serFile.OpenAsync(FileAccessMode.ReadWrite);
            stream.Seek(streamSize);
            using (var des = new UniversalSerializer(stream.AsStream(), SerializerFormatters.BinarySerializationFormatter))
            {
                var deser = des.Deserialize<Man>();
                desTextBlock.Text = Convert.ToString(deser);
            }
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
    }
}
