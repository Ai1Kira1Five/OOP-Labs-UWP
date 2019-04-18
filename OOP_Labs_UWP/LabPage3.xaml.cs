using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace OOP_Labs_UWP
{
    #region Struct to serialize
    [DataContract]
    public class Man
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Year { get; set; }
    }
    #endregion

    public sealed partial class LabPage3 : Page
    {
        public LabPage3()
        {
            this.InitializeComponent();
            inputBtn.IsEnabled = false;
        }

        #region Variables and other
        //Used by XML helper methods
        private const string XMLFILENAME = "data.xml";

        //used by JSON helper methods
        private const string JSONFILENAME = "data.json";
        private static int size = 0, currSize = 0;
        private List<Man> ManList = new List<Man>();
        #endregion

        #region List of structure
        private List<Man> buildObject()
        {
            ManList.Add(new Man() { Id = currSize, Name = tb_Name.Text, Year = Convert.ToInt32(tb_Year.Text)});
            return ManList;
        }
        #endregion

        private void CreateSpaceBtn_Click(object sender, RoutedEventArgs e)
        {
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
            Stopwatch sw = new Stopwatch();
            serFilePath.Text = ApplicationData.Current.LocalFolder.Path;
            sw.Reset();
            sw.Start();

            var serializer = new DataContractSerializer(typeof(List<Man>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                XMLFILENAME,
                CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, ManList);
            }
            sw.Stop();

            serializedTime.Text = Convert.ToString(sw.ElapsedMilliseconds);

            string content = String.Empty;

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(XMLFILENAME);
            using (StreamReader reader = new StreamReader(myStream))
            {
                content = await reader.ReadToEndAsync();
            }

            serTextBlock.Text = content;
        }

        private async void DeserializeBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            string content = String.Empty;
            List<Man> desList;
            Stopwatch sw = new Stopwatch();

            sw.Reset();
            sw.Start();
            var serializer = new DataContractSerializer(typeof(List<Man>));
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(XMLFILENAME);

            desList = (List<Man>)serializer.ReadObject(myStream);

            foreach(var man in desList)
            {
                content += String.Format("ID: {0}, Name: {1}, Year: {2} \r\n", man.Id, man.Name, man.Year);
            }
            desTextBlock.Text = content;
            sw.Stop();

            deSerializedTime.Text = Convert.ToString(sw.ElapsedMilliseconds);
        }

        private void InputBtn_Click(object sender, RoutedEventArgs e)
        {
            var Person = buildObject();
            currSize++;
            progressBarCurr.Text = Convert.ToString(currSize);
            lb3ProgressBar.Value++;
            if (currSize == size) inputBtn.IsEnabled = false;
        }
    }
}
