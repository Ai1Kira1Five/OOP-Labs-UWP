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
    public sealed partial class LabPage3 : Page
    {
        public LabPage3()
        {
            this.InitializeComponent();
        }

        [Serializable]
        public class Man
        {
            public string Name { get; set; }
            public int Year { get; set; }

            public Man(string name, int year)
            {
                Name = name;
                Year = year;
            }
        }

        Man[] man;
        BinaryFormatter binF = new BinaryFormatter();
    }
}
