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
using System.IO;

namespace WpfSnooker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Versenyzo> adatok = new List<Versenyzo>();

        public MainWindow()
        {
            InitializeComponent();
            dgTablazat.ItemsSource = adatok;
            File.ReadAllLines("snooker.txt").Skip(1).ToList().ForEach(x => adatok.Add(new Versenyzo(x)));
            List<string> orszagok = new List<string>();
            adatok.GroupBy(x => x.Orszag).ToList().ForEach(x => orszagok.Add(x.Key));
            cbOrszagok.ItemsSource = orszagok;
            cbOrszagok.SelectedIndex = 0;
        }
       private void btnF3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"A világranglistán{ adatok.Count()} versenyző szerepel");
        }
        private void btnF4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{Math.Round(adatok.Average(x => x.Nyeremeny), 2)}");
        }
        private void btnF5_Click(object sender, RoutedEventArgs e)
        {
            var legjobbanKereso = adatok.Where(x => x.Orszag == cbOrszagok.SelectedItem).OrderByDescending(x => x.Nyeremeny).First();
            lblHelyezes.Content = legjobbanKereso.Helyezes;
            lblNev.Content = legjobbanKereso.Nev;
            lblOrszag.Content = legjobbanKereso.Orszag;
            lblNyeremeny.Content = legjobbanKereso.Nyeremeny * int.Parse(txtArfolyam.Text);
        }
        private void btnF6_Click(object sender, RoutedEventArgs e)
        {
          var vanIlyenOrszag =  adatok.Select(x => x.Orszag).Contains(txtVanIlyenOrszag.Text);
            if (vanIlyenOrszag == true)
                MessageBox.Show("Van ilyen ország");
            else
                MessageBox.Show("Nincs ilyen ország");
       
        }

        private void btnF7_Click(object sender, RoutedEventArgs e)
        {
            List<string> orszagonkentVersenyzo = new List<string>();
            adatok.GroupBy(x => x.Orszag).Where(x => x.Count() > sliMinLetszam.Value).ToList().ForEach(x=> orszagonkentVersenyzo.Add($"{x.Key} : {x.Count()} fő"));
            lbStatisztika.ItemsSource = orszagonkentVersenyzo;
        }
    }
}
