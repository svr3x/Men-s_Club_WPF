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

namespace Mens_ClubWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtDolgnost_Click(object sender, RoutedEventArgs e)
        {
            Dolgnost dolgnost = new Dolgnost();
            dolgnost.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtSotrudniki_Click(object sender, RoutedEventArgs e)
        {
            Sotrudniki Sotrudniki = new Sotrudniki();
            Sotrudniki.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtPostavshik_Click(object sender, RoutedEventArgs e)
        {
            Postavshik Postavshik = new Postavshik();
            Postavshik.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtJurnal_Click(object sender, RoutedEventArgs e)
        {
            Jurnal Jurnal = new Jurnal();
            Jurnal.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtZakaz_Click(object sender, RoutedEventArgs e)
        {
            Zakaz Zakaz = new Zakaz();
            Zakaz.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtCheck_Click(object sender, RoutedEventArgs e)
        {
            Check Check = new Check();
            Check.Show();
            Visibility = Visibility.Collapsed;
        }
    }
}
