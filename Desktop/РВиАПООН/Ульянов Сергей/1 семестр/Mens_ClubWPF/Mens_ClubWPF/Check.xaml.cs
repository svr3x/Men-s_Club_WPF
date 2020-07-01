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
    /// Логика взаимодействия для Check.xaml
    /// </summary>
    public partial class Check : Window
    {
        DBProcedures procedures = new DBProcedures();
        private string QR = "";
        public Check()
        {
            InitializeComponent();
            cbFill();
            cb2Fill();
        }

        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrCheck = qr;
                connection.CheckFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgCheck.ItemsSource = connection.dtCheck.DefaultView;
                dgCheck.Columns[0].Visibility = Visibility.Collapsed;
                dgCheck.Columns[4].Visibility = Visibility.Collapsed;
                dgCheck.Columns[5].Visibility = Visibility.Collapsed;
            };
            Dispatcher.Invoke(action);
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        private void DgCheck_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Number_Check"):
                    e.Column.Header = "Номер чека";
                    break;
                case ("Symma_Zakaza"):
                    e.Column.Header = "Сумма заказа";
                    break;
                case ("Data"):
                    e.Column.Header = "Дата";
                    break;
                case ("Last_Name_Sotrudnik"):
                    e.Column.Header = "Фамилия сотрудника";
                    break;
                case ("Number_Komnati"):
                    e.Column.Header = "Номер зала";
                    break;
                case ("Tip_Kalyana"):
                    e.Column.Header = "Кальян";
                    break;
                case ("Tip_Tabaka"):
                    e.Column.Header = "Табак";
                    break;
            }
        }
        private void cbFill()
        {
            DBConnection connection = new DBConnection();  
            connection.SotrudnikiFill();
            cbLast_Name_Sotrudnik.ItemsSource = connection.dtSotrudniki.DefaultView;
            cbLast_Name_Sotrudnik.SelectedValuePath = "ID_Sotrudnik";
            cbLast_Name_Sotrudnik.DisplayMemberPath = "Last_Name_Sotrudnik";
            connection.ZakazFill();
            cbNumber_Komnati.ItemsSource = connection.dtZakaz.DefaultView;
            cbNumber_Komnati.SelectedValuePath = "ID_Zakaz";
            cbNumber_Komnati.DisplayMemberPath = "Number_Komnati";
        }

        private void cb2Fill()
        {
            DBConnection connection = new DBConnection();
            cbTip_Kalyana.ItemsSource = connection.dtZakaz.DefaultView;
            cbTip_Kalyana.SelectedValuePath = "ID_Zakaz";
            cbTip_Kalyana.DisplayMemberPath = "Tip_Kalyana";
            connection.ZakazFill();
            cbTip_Tabaka.ItemsSource = connection.dtZakaz.DefaultView;
            cbTip_Tabaka.SelectedValuePath = "ID_Zakaz";
            cbTip_Tabaka.DisplayMemberPath = "Tip_Tabaka";
        }

        private void Check_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrCheck;
            dgFill(QR);
        }

        private void BtCheckInsertType_Click(object sender, RoutedEventArgs e)
        {
            procedures.resCheck_insert(Convert.ToInt32(tbNumber_Check.Text.ToString()), tbSymma_Zakaza.Text.ToString(),
            tbData.Text.ToString(), Convert.ToInt32(cbNumber_Komnati.SelectedValue.ToString()),
            Convert.ToInt32(cbLast_Name_Sotrudnik.SelectedValue.ToString()));
            dgFill(QR);
        }
        private void BtCheckUpdateType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgCheck.SelectedItems[0];
            procedures.resCheck_update(Convert.ToInt32(ID["ID_Check"]), Convert.ToInt32(tbNumber_Check.Text.ToString()), tbSymma_Zakaza.Text.ToString(),
            tbData.Text.ToString(), Convert.ToInt32(cbNumber_Komnati.SelectedValue.ToString()),
            Convert.ToInt32(cbLast_Name_Sotrudnik.SelectedValue.ToString()));
            dgFill(QR);
        }
        private void BtCheckDeleteType_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgCheck.SelectedItems[0];
            procedures.resCheck_delete(Convert.ToInt32(ID["ID_Check"]));
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
            foreach (DataRowView dataRow in (DataView)dgCheck.ItemsSource)
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
                    dgCheck.SelectedItem = dataRow;
                }
            }
        }

        private void ChbFilter_Checked(object sender, RoutedEventArgs e)
        {
            string newQr = QR + " where [Number_Check] like '%" + tbSearch.Text + "%' or " +
               "[Symma_Zakaza] like '%" + tbSearch.Text + "%' or " +
               "[Data] like '%" + tbSearch.Text + "%' or " +
               "[Number_Komnati] like '%" + tbSearch.Text + "%' or " +
               "[Last_Name_Sotrudnik] like '%" + tbSearch.Text + "%'";
            dgFill(newQr);
        }
    }
}
