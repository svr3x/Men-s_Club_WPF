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
using System.Data;
using System.Data.SqlClient;

namespace Mens_ClubWPF
{
    /// <summary>
    /// Логика взаимодействия для Postavshik.xaml
    /// </summary>
    public partial class Postavshik : Window
    {
        DBProcedures procedures = new DBProcedures();
        private string QR = "";
        public Postavshik()
        {
            InitializeComponent();
        }

        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrPostavshik = qr;
                connection.PostavshikFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgPostavshik.ItemsSource = connection.dtPostavshik.DefaultView;
                dgPostavshik.Columns[0].Visibility = Visibility.Collapsed;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        private void DgPostavshik_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Last_Name_Postavshik"):
                    e.Column.Header = "Фамилия";
                    break;
                case ("Name_Postavshik"):
                    e.Column.Header = "Имя";
                    break;
                case ("Midlle_Name_Postavshik"):
                    e.Column.Header = "Отчество";
                    break;
                case ("Loign_Postavshika"):
                    e.Column.Header = "Логин";
                    break;
                case ("Password_Postavshika"):
                    e.Column.Header = "Пароль";
                    break;
            }
        }

        private void Postavshik_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrPostavshik;
            dgFill(QR);
        }

        private void BtPostavshikInsertType_Click(object sender, RoutedEventArgs e)
        {
            procedures.resPostavshik_insert(tbName_Postavshik.Text.ToString(), tbMidlle_Name_Postavshik.Text.ToString(), tbLast_Name_Postavshik.Text.ToString(), 
                tbLoign_Postavshika.Text.ToString(), tbPassword_Postavshika.Text.ToString());
            dgFill(QR);
        }

        private void BtPostavshikUpdateType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgPostavshik.SelectedItems[0];
            procedures.resPostavshik_update(Convert.ToInt32(ID["ID_Postavshik"]), tbName_Postavshik.Text.ToString(), tbMidlle_Name_Postavshik.Text.ToString(),
                tbLast_Name_Postavshik.Text.ToString(), tbLoign_Postavshika.Text.ToString(), tbPassword_Postavshika.Text.ToString());
            dgFill(QR);
        }

        private void BtPostavshikDeleteType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgPostavshik.SelectedItems[0];
            procedures.resPostavshik_delete(Convert.ToInt32(ID["ID_Postavshik"]));
            dgFill(QR);
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgPostavshik.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[5].ToString() == tbSearch.Text)                    
                {
                    dgPostavshik.SelectedItem = dataRow;
                }
            }
        }

        private void ChbFilter_Checked(object sender, RoutedEventArgs e)
        {
            string newQr = QR + " where [Name_Postavshik] like '%" + tbSearch.Text + "%' or " +
                "[Midlle_Name_Postavshik] like '%" + tbSearch.Text + "%' or " +
                "[Last_Name_Postavshik] like '%" + tbSearch.Text + "%' or " +
                "[Loign_Postavshika] like '%" + tbSearch.Text + "%' or " +
                "[Password_Postavshika] like '%" + tbSearch.Text + "%'";
            dgFill(newQr);
        }
    }
}
