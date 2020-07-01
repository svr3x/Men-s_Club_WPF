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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Mens_ClubWPF
{
    /// <summary>
    /// Логика взаимодействия для Authorizacia.xaml
    /// </summary>
    public partial class Authorizacia : Window
    {
        public Authorizacia()
        {
            SystemCheck();
            switch (startup)

            {
                case true:
                InitializeComponent();
                    tbEnterLogin.Clear();
                    tbEnterPassword.Clear();
                    ChangeSize((int)Width, (int)Height);
                    break;
                case false:
                    Close();
                    break;
            }
        }

        bool startup = true;
        private void SystemCheck()
        {
            int Major = Environment.OSVersion.Version.Major;
            int Minor = Environment.OSVersion.Version.Minor;
            if ((Major >= 6) && (Minor >= 0))
            {
                RegistryKey registrySQL =
                    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\MICROSOFT\Microsoft SQL Server");
                if (registrySQL == null)
                {
                    MessageBox.Show("Запуск системы не возможен," +
                        "в системе отсутсвует Microsoft SQL Server",
                        "Men’s club");
                        startup = false;
                }
                else
                {
                    try
                    {
                        DBConnection.connection.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Не возможно подключится к " +
                            "Источнику данных", "Men’s club");
                        startup = false;
                    }
                    finally
                    {
                        DBConnection.connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Данная операционная система " +
                    "не предназначена для запуска приложения!", "Men’s club");
                startup = false;
            }
        }

        private void ChangeSize(int X, int Y)
        {
            if ((X >= 0 ) && (X< 800 && Y < 600))
            {
                lblEnterLogin.FontSize = 12;
                lblEnterPassword.FontSize = 12;
                tbEnterLogin.FontSize = 12;
                tbEnterPassword.FontSize = 12;
                btEnter.FontSize = 12;
                btCancel.FontSize = 12;
            }
            else
            {
                if ((X >= 800 && Y >= 600) && (X <= 1200 && Y <= 1024))
                {
                    lblEnterLogin.FontSize = 18;
                    lblEnterPassword.FontSize = 18;
                    tbEnterLogin.FontSize = 18;
                    tbEnterPassword.FontSize = 18;
                    btEnter.FontSize = 18;
                    btCancel.FontSize = 18;
                }
                else
                {
                    if ((X >= 1200 && Y >= 1024) && (X <= 1680 && Y <= 1050))
                    {
                        lblEnterLogin.FontSize = 24;
                        lblEnterPassword.FontSize = 24;
                        tbEnterLogin.FontSize = 24;
                        tbEnterPassword.FontSize = 24;
                        btEnter.FontSize = 24;
                        btCancel.FontSize = 24;
                    }
                    else
                    {
                        if (X >= 1680 && Y > 1050)
                        {
                            lblEnterLogin.FontSize = 36;
                            lblEnterPassword.FontSize = 36;
                            tbEnterLogin.FontSize = 36;
                            tbEnterPassword.FontSize = 36;
                            btEnter.FontSize = 36;
                            btCancel.FontSize = 36;
                        }
                    }
                }
            }
        }

        private void BtEnter_Click(object sender, RoutedEventArgs e)
        {
            DBConnection connection = new DBConnection();
            MainWindow MainWindow = new MainWindow();
            connection.dbEnter(tbEnterLogin.Password, tbEnterPassword.Password);
            switch (DBConnection.IDUser)
            {
                case (0):                  
                    MessageBox.Show("Введён неверный логин или пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbEnterLogin.Password = "";
                    tbEnterPassword.Password = "";
                    break;
                default:
                    MainWindow.Show();
                    Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize((int)e.NewSize.Width, (int)e.NewSize.Height);
        }
    }
}
