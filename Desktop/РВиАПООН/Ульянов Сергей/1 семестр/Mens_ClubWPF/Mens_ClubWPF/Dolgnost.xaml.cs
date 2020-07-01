using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Mens_ClubWPF
{
    /// <summary>
    /// Логика взаимодействия для Dolgnost.xaml
    /// </summary>
    public partial class Dolgnost : Window
    {
        DBProcedures procedures = new DBProcedures();
        private string QR = "";
        public Dolgnost()
        {
            InitializeComponent();
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgDolgnost.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tbSearch.Text)                   
                {
                    dgDolgnost.SelectedItem = dataRow;
                }
            }
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            Visibility = Visibility.Collapsed;
        }

        private void DolgnostWPF_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrDolgnost;
            dgFill(QR);
        }

        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrDolgnost = qr;
                connection.DolgnostFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgDolgnost.ItemsSource = connection.dtDolgnost.DefaultView;
                dgDolgnost.Columns[0].Visibility = Visibility.Collapsed;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        private void BtDolgnostInsertType_Click(object sender, RoutedEventArgs e)
        {
            procedures.resDolgnost_insert(tbName_Dolgnost.Text.ToString(), Convert.ToInt32(tbOklad.Text.ToString()));
            dgFill(QR);
        }

        private void BtDolgnostUpdateType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgDolgnost.SelectedItems[0];
            procedures.resDolgnost_update(Convert.ToInt32(ID["ID_Dolgnost"]), tbName_Dolgnost.Text.ToString(), Convert.ToInt32(tbOklad.Text.ToString()));
            dgFill(QR);

        }

        private void BtDolgnostDeleteType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgDolgnost.SelectedItems[0];
            procedures.resDolgnost_delete(Convert.ToInt32(ID["ID_Dolgnost"]));
            dgFill(QR);
        }

        private void DgDolgnost_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_Dolgnost"):
                    e.Column.Header = "Наименование";
                    break;
                case ("Oklad_Dolgnost"):
                    e.Column.Header = "Оклад";
                    break;
            }
        }

        private void DgDolgnost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChbFilter_Checked(object sender, RoutedEventArgs e)
        {
            string newQr = QR + " where [Name_Dolgnost] like '%" + tbSearch.Text + "%' or " +
                "[Oklad_Dolgnost] like '%" + tbSearch.Text + "%'";
            dgFill(newQr);
        }
    }
}
