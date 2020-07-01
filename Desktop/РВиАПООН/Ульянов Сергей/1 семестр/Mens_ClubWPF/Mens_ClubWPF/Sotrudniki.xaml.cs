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
    /// Логика взаимодействия для Sotrudniki.xaml
    /// </summary>
    public partial class Sotrudniki : Window
    {
        DBProcedures procedures = new DBProcedures();
        private string QR = "";
        public Sotrudniki()
        {
            InitializeComponent();
            cbFill();
        }

        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrSotrudniki = qr;
                connection.SotrudnikiFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgSotrudnik.ItemsSource = connection.dtSotrudniki.DefaultView;
                dgSotrudnik.Columns[0].Visibility = Visibility.Collapsed;
                dgSotrudnik.Columns[9].Visibility = Visibility.Collapsed;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.DolgnostFill();
            cbDolgnost.ItemsSource = connection.dtDolgnost.DefaultView;
            cbDolgnost.SelectedValuePath = "ID_Dolgnost";
            cbDolgnost.DisplayMemberPath = "Name_Dolgnost";
        }

        private void DgSotrudnik_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_Sotrudnik"):
                    e.Column.Header = "Имя";
                    break;
                case ("Midlle_Name_Sotrudnik"):
                    e.Column.Header = "Отчество";
                    break;
                case ("Last_Name_Sotrudnik"):
                    e.Column.Header = "Фамилия";
                    break;
                case ("Document_Series"):
                    e.Column.Header = "Серия_паспорта";
                    break;
                case ("Document_Number"):
                    e.Column.Header = "Номер_паспорта";
                    break;
                case ("Login_Sotrudnika"):
                    e.Column.Header = "Логин";
                    break;
                case ("Password_Sotrudnika"):
                    e.Column.Header = "Пароль";
                    break;
                case ("Name_Dolgnost"):
                    e.Column.Header = "Должность";
                    break;
            }
        }

        private void Sotrudniki_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrSotrudniki;
            dgFill(QR);
        }

        private void BtSotrudnikiInsertType_Click(object sender, RoutedEventArgs e)
        {
            procedures.resSotrudniki_insert(tbName_Sotrudnik.Text.ToString(), tbMidlle_Name_Sotrudnik.Text.ToString(), tbLast_Name_Sotrudnik.Text.ToString(),
                 tbDocument_Series.Text.ToString(), tbDocument_Number.Text.ToString(), tbLogin_Sotrudnika.Text.ToString(),
                tbPassword_Sotrudnika.Text.ToString(), Convert.ToInt32(cbDolgnost.SelectedValue.ToString()));
            dgFill(QR);
        }

        private void BtSotrudnikiUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgSotrudnik.SelectedItems[0];
            procedures.resSotrudniki_update(Convert.ToInt32(ID["ID_Sotrudnik"]), tbName_Sotrudnik.Text.ToString(), tbMidlle_Name_Sotrudnik.Text.ToString(), 
                tbLast_Name_Sotrudnik.Text.ToString(),tbDocument_Series.Text.ToString(), tbDocument_Number.Text.ToString(),
                tbLogin_Sotrudnika.Text.ToString(), tbPassword_Sotrudnika.Text.ToString(),
                Convert.ToInt32(cbDolgnost.SelectedValue.ToString()));
            dgFill(QR);
        }
        private void BtSotrudnikiDeleteType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgSotrudnik.SelectedItems[0];
            procedures.resSotrudniki_delete(Convert.ToInt32(ID["ID_Sotrudnik"]));
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
            foreach (DataRowView dataRow in (DataView)dgSotrudnik.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[5].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[6].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[7].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[8].ToString() == tbSearch.Text)
                {
                    dgSotrudnik.SelectedItem = dataRow;
                }
            }
        }

        private void ChbFilter_Checked(object sender, RoutedEventArgs e)
        {
            string newQr = QR + " where [Name_Sotrudnik] like '%" + tbSearch.Text + "%' or " +
               "[Midlle_Name_Sotrudnik] like '%" + tbSearch.Text + "%' or " +
               "[Last_Name_Sotrudnik] like '%" + tbSearch.Text + "%' or " +
               "[Document_Series] like '%" + tbSearch.Text + "%' or " +
               "[Document_Number] like '%" + tbSearch.Text + "%' or " +
               "[Login_Sotrudnika] like '%" + tbSearch.Text + "%' or " +
               "[Password_Sotrudnika] like '%" + tbSearch.Text + "%' or " +
               "[Name_Dolgnost] like '%" + tbSearch.Text + "%'";
            dgFill(newQr);
        }
    }
}
