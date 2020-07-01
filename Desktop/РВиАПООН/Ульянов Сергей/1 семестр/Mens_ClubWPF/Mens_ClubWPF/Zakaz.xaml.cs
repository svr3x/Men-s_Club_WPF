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
    /// Логика взаимодействия для Zakaz.xaml
    /// </summary>
    public partial class Zakaz : Window
    {
        DBProcedures procedures = new DBProcedures();
        private string QR = "";
        public Zakaz()
        {
            InitializeComponent();
            cbFill();
        }

        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrZakaz = qr;
                connection.ZakazFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgZakaz.ItemsSource = connection.dtZakaz.DefaultView;
                dgZakaz.Columns[0].Visibility = Visibility.Collapsed;
                dgZakaz.Columns[4].Visibility = Visibility.Collapsed;
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
            connection.JurnalFill();
            cbJurnal.ItemsSource = connection.dtJurnal.DefaultView;
            cbJurnal.SelectedValuePath = "ID_Jurnal";
            cbJurnal.DisplayMemberPath = "Name_Tovara";
        }

        private void DgZakaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Number_Komnati"):
                    e.Column.Header = "Номер зала";
                    break;
                case ("Tip_Kalyana"):
                    e.Column.Header = "Тип кальяна";
                    break;
                case ("Tip_Tabaka"):
                    e.Column.Header = "Тип табака";
                    break;
                case ("Name_Tovara"):
                    e.Column.Header = "Название товара";
                    break;
            }
        }

        private void Zakaz_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrZakaz;
            dgFill(QR);
        }

        private void BtZakazInsertType_Click(object sender, RoutedEventArgs e)
        {
            procedures.resZakaz_insert(Convert.ToInt32(tbNumber_Komnati.Text.ToString()), tbTip_Kalyana.Text.ToString(), tbTip_Tabaka.Text.ToString(),
                 Convert.ToInt32(cbJurnal.SelectedValue.ToString()));
            dgFill(QR);
        }

        private void BtZakazUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgZakaz.SelectedItems[0];
            procedures.resZakaz_update(Convert.ToInt32(ID["ID_Zakaz"]), Convert.ToInt32(tbNumber_Komnati.Text.ToString()),
            tbTip_Kalyana.Text.ToString(),  tbTip_Tabaka.Text.ToString(),
            Convert.ToInt32(cbJurnal.SelectedValue.ToString()));
            dgFill(QR);                
        }
        private void BtZakazDeleteType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgZakaz.SelectedItems[0];
            procedures.resZakaz_delete(Convert.ToInt32(ID["ID_Zakaz"]));
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
            foreach (DataRowView dataRow in (DataView)dgZakaz.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tbSearch.Text)                  
                {
                    dgZakaz.SelectedItem = dataRow;
                }
            }
        }

        private void ChbFilter_Checked(object sender, RoutedEventArgs e)
        {
            string newQr = QR + " where [Number_Komnati] like '%" + tbSearch.Text + "%' or " +
                "[Tip_Kalyana] like '%" + tbSearch.Text + "%' or " +
                "[Tip_Tabaka] like '%" + tbSearch.Text + "%' or " +
                "[Name_Tovara] like '%" + tbSearch.Text + "%'";
            dgFill(newQr);
        }
    }
}
