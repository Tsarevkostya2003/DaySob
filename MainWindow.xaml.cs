using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace OAP_Ex2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<zametka> maszam = new List<zametka>();
        public MainWindow()
        {
           
            InitializeComponent();
            DateTime curdate= DateTime.Today;
            kalend.SelectedDate = curdate;
            readjson("zam.json");  // добавляем данные из файла в список maszam
            Show(curdate); // показываем заметки сегодня
        }
     public void Show(DateTime dd )
        {
            List<string> sbox = new List<string>();
            sbox.Clear();
            foreach (var i in maszam)
            {
                if (dd == i.dat)
                {
                    sbox.Add(i.name);
                }
            }
            sob.ItemsSource = sbox;
        }


        private void kalend_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
                Show((DateTime)kalend.SelectedDate);     
        }

        public void savejson(string cF) // сериализация
        {
            // Сериализация в json
            string json = JsonConvert.SerializeObject(maszam);
            File.WriteAllText(cF, json);

        }


        public void readjson(string cF) // десерелизируем json
        {
            string cSt;
            cSt = File.ReadAllText(cF);
            maszam = JsonConvert.DeserializeObject<List<zametka>>(cSt);
           
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Name.Text))
            {

                Name.Text = sob.SelectedItem.ToString();

                foreach (zametka z in maszam)
                {
                    if ( Name.Text == z.name  & z.dat == (DateTime)kalend.SelectedDate)
                    {
                       
                        z.name=Name.Text;
                        z.opis = Opis.Text;
                        Show((DateTime)kalend.SelectedDate);
                        break;

                    }
                }

                savejson("zam.json");

            }

            savejson("zam.json"); // сохраняем в файл
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            //добавляем заметку в список mazam
            zametka nz= new zametka();
            nz.name = Name.Text;
            nz.opis = Opis.Text;
            nz.dat=(DateTime)kalend.SelectedDate;
            maszam.Add(nz);
            Show((DateTime)kalend.SelectedDate);
            Name.Text = "";
            Opis.Text = "";
            savejson("zam.json");

        }

        private void sob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (sob.SelectedItem != null)
            {
                Name.Text = sob.SelectedItem.ToString();
                foreach (zametka z in maszam)
                {
                    if (z.name == Name.Text & z.dat == (DateTime)kalend.SelectedDate)
                    {
                        Opis.Text = z.opis;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Name.Text)) 
            {
                foreach (zametka z in maszam)
                {
                    if (z.name == Name.Text & z.dat == (DateTime)kalend.SelectedDate )
                    {
                        maszam.Remove(z);
                        Show((DateTime)kalend.SelectedDate);
                        Name.Text = "";
                        Opis.Text = "";
                        break;

                    }
                }

                savejson("zam.json");

            }
        }
    }

   
}
