using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Ude;
using UniversalSerializerLib3;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OOP_Labs_UWP
{
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

        private async void SerializeBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            StorageFolder fl = await ApplicationData.Current.LocalFolder.CreateFolderAsync("LabFiles", CreationCollisionOption.OpenIfExists);

            StorageFile serFile = await fl.CreateFileAsync("man_list.txt", CreationCollisionOption.ReplaceExisting);
            

            var stream = await serFile.OpenAsync(FileAccessMode.ReadWrite);
            

            using (var s = new UniversalSerializer(stream.AsStream(), SerializerFormatters.BinarySerializationFormatter))
            {
                s.Serialize(manList);
            }
            stream.Dispose();
            sw.Stop();
            string txt = " ";


            //IBuffer buf = await FileIO.ReadBufferAsync(serFile);
            //DataReader reader = DataReader.FromBuffer(buf);
            //byte[] fileContent = new byte[reader.UnconsumedBufferLength];
            //ICharsetDetector cdet = new CharsetDetector();
            //cdet.Feed(fileContent, 0, fileContent.Length);
            //cdet.DataEnd();
            //if (cdet.Charset != null)
            //    txt = Portable.Text.Encoding.GetEncoding(cdet.Charset).GetString(fileContent, 0, fileContent.Length);

            txt = await Windows.Storage.FileIO.ReadTextAsync(serFile, Windows.Storage.Streams.UnicodeEncoding.Utf16BE);
            
            serTextBlock.Text = txt;
            serializedTime.Text = Convert.ToString(Math.Truncate(sw.Elapsed.TotalMilliseconds)); 
        }

        private async void DeserializeBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            StorageFolder fl = await ApplicationData.Current.LocalFolder.CreateFolderAsync("LabFiles", CreationCollisionOption.OpenIfExists);

            StorageFile serFile = await fl.CreateFileAsync("man_list.bin", CreationCollisionOption.OpenIfExists);

            Stream str = await serFile.OpenStreamForReadAsync();

            using (var des = new UniversalSerializer(str))
            {
                var deser = des.Deserialize<Man>();
                desTextBlock.Text = Convert.ToString(deser);
            }

            //string txt = serFile.Path;

            //serTextBlock.Text = txt;
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
