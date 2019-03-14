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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace OOP_Labs_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LabPage1 : Page
    {
        public LabPage1()
        {
            this.InitializeComponent();
        }

        interface IPrint { string Print(); }

        class Man : IPrint
        {
            public string Fio { get; set; }

            public Man() { }

            public Man(string name)
            {
                Fio = name;
            }

            public virtual string Print()
            {
                return "Fio: " + Fio;
            }
        }

        class School : Man, IPrint
        {
            public int Year { get; set; }

            public School(string name) : base(name)
            {
                Year = 11;
            }

            public School(string name, int year) : base(name)
            {
                Year = year;
            }

            public override string Print()
            {
                return base.Print() + " Classes: " + Year;
            }
        }

        class Stud : School, IPrint
        {
            public string Vuz { get; set; }
            public string Reason { get; set; }

            private readonly string type;
            private readonly Zaoch zch;

            public Stud(string name, int year, string vuz) : base(name, year)
            {
                Vuz = vuz;
            }

            public Stud(string name, int year, string vuz, string reas) : base(name, year)
            {
                type = "zaoch";
                Vuz = vuz;
                Reason = reas;
                this.zch = new Zaoch(name, vuz, reas);
            }

            public override string Print()
            {
                if (type == "zaoch")
                    return base.Print() + " Vuz: " + Vuz + " Rs: " + Reason;
                else
                    return base.Print() + " Vuz: " + Vuz;
            }
        }

        class Zaoch
        {
            public string Reason { get; set; }
            public string Vuz { get; set; }
            public string Fio { get; set; }

            public Zaoch(string name, string vuz, string reason)
            {
                Reason = reason;
                Vuz = vuz;
                Fio = name;
            }
        }

        Man[] men; private string type; private int size = 1, current = 0;

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < current; i++) textBox.Text = "";
            for (int i = 0; i < current; i++) textBox.Text += men[i].Print() + "\n";
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case "man":
                    if (current < size)
                    {
                        men[current].Fio = tb_Name.Text;
                        current++;
                    }
                    break;
                case "school":
                    if (current < size)
                    {
                        School sch = new School(tb_Name.Text)
                        {
                            Year = int.Parse(tb_Year.Text)
                        };
                        men[current] = sch;
                        current++;
                    }
                    break;
                case "student":
                    if (current < size)
                    {
                        Stud st = new Stud(tb_Name.Text, int.Parse(tb_Year.Text), tb_VYZ.Text);
                        men[current] = st;
                        current++;
                    }
                    break;
                case "zaoch":
                    if (current < size)
                    {
                        Stud zch = new Stud(tb_Name.Text, int.Parse(tb_Year.Text), tb_VYZ.Text, tb_Reason.Text);
                        men[current] = zch;
                        current++;
                    }
                    break;
            }
        }

        private void CreateSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            men = new Man[size];
            for (int i = 0; i < size; i++) men[i] = new Man();
            createSpaceButton.IsEnabled = false;
        }

        private void Rb_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is RadioButton rb)
            {
                string typeIn = rb.Tag.ToString();
                switch (typeIn)
                {
                    case "Man":
                        type = "man";
                        break;
                    case "School":
                        type = "school";
                        break;
                    case "Student":
                        type = "student";
                        break;
                    case "Zaoch":
                        type = "zaoch";
                        break;
                }
            }
        }

        private void SpaceSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            size = Convert.ToInt32(e.NewValue);
        }
    }
}
