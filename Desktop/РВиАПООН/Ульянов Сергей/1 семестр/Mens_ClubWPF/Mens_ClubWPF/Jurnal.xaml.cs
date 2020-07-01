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
    /// Логика взаимодействия для Jurnal.xaml
    /// </summary>
    public partial class Jurnal : Window
    {
        DBProcedures procedures = new DBProcedures();
        private string QR = "";
        public Jurnal()
        {
            InitializeComponent();
            cbFill();
        }
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrJurnal = qr;
                connection.JurnalFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgJurnal.ItemsSource = connection.dtJurnal.DefaultView;
                dgJurnal.Columns[0].Visibility = Visibility.Collapsed;
                dgJurnal.Columns[5].Visibility = Visibility.Collapsed;
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
                connection.PostavshikFill();
                cbPostavshik.ItemsSource = connection.dtPostavshik.DefaultView;
                cbPostavshik.SelectedValuePath = "ID_Postavshik";
                cbPostavshik.DisplayMemberPath = "Name_Postavshik";   
        }

        private void DgJurnal_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_Tovara"):
                    e.Column.Header = "Название товара";
                    break;
                case ("Tip_Tovara"):
                    e.Column.Header = "Тип товара";
                    break;
                case ("Kolichestvo"):
                    e.Column.Header = "Количество";
                    break;
                case ("Price_of_Tovar"):
                    e.Column.Header = "Цена товара";
                    break;
                case ("Name_Postavshik"):
                    e.Column.Header = "Имя поставщика";
                    break;
            }

        }

        private void Jurnal_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrJurnal;
            dgFill(QR);
        }

        private void BtJurnalInsertType_Click(object sender, RoutedEventArgs e)
        {
            procedures.resJurnal_insert(tbName_Tovara.Text.ToString(), tbTip_Tovara.Text.ToString(), Convert.ToInt32(tbKolichestvo.Text.ToString()),
                 Convert.ToInt32(tbPrice_of_Tovar.Text.ToString()), Convert.ToInt32(cbPostavshik.SelectedValue.ToString()));
            dgFill(QR);
        }
        private void BtJurnalUpdateType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgJurnal.SelectedItems[0];
            procedures.resJurnal_update(Convert.ToInt32(ID["ID_Jurnal"]), tbName_Tovara.Text.ToString(), tbTip_Tovara.Text.ToString(), Convert.ToInt32(tbKolichestvo.Text.ToString()),
                 Convert.ToInt32(tbPrice_of_Tovar.Text.ToString()), Convert.ToInt32(cbPostavshik.SelectedValue.ToString()));
            dgFill(QR);
        }
        private void BtJurnalDeleteType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgJurnal.SelectedItems[0];
            procedures.resJurnal_delete(Convert.ToInt32(ID["ID_Jurnal"]));
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
            foreach (DataRowView dataRow in (DataView)dgJurnal.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tbSearch.Text ||                    
                    dataRow.Row.ItemArray[6].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[7].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[8].ToString() == tbSearch.Text)
                {
                    dgJurnal.SelectedItem = dataRow;
                }
            }
        }

        private void ChbFilter_Checked(object sender, RoutedEventArgs e)
        {
            string newQr = QR + " where [Name_Tovara] like '%" + tbSearch.Text + "%' or " +
                "[Tip_Tovara] like '%" + tbSearch.Text + "%' or " +
                "[Kolichestvo] like '%" + tbSearch.Text + "%' or " +
                "[Price_of_Tovar] like '%" + tbSearch.Text + "%' or " +
                "[Name_Postavshik] like '%" + tbSearch.Text + "%'";
            dgFill(newQr);
        }
    }
}
