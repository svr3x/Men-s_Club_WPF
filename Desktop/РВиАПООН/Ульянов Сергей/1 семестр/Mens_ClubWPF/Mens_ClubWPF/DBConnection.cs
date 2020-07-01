using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Mens_ClubWPF
{
    class DBConnection
    {
        public static SqlConnection connection = new SqlConnection(
            "Data Source = LAPTOP-VEVLOJKN\\FIRST_SERVER; " +
                " Initial Catalog = Mens_Club; Persist Security Info = true;" +
                " User ID = sa; Password = \"1845\"");

        public DataTable dtDolgnost = new DataTable("Dolgnost");
        public DataTable dtSotrudniki = new DataTable("Sotrudniki");
        public DataTable dtPostavshik = new DataTable("Postavshik");
        public DataTable dtJurnal = new DataTable("Jurnal");
        public DataTable dtZakaz = new DataTable("Zakaz");
        public DataTable dtCheck = new DataTable("Check");
        //static
        public SqlDependency Dependency = new SqlDependency();

        public static string
        qrDolgnost = "SELECT [ID_Dolgnost], [Name_Dolgnost],[Oklad_Dolgnost] FROM [dbo].[Dolgnost]",

        qrSotrudniki = "SELECT [ID_Sotrudnik] ,[Name_Sotrudnik], [Midlle_Name_Sotrudnik], [Last_Name_Sotrudnik]" +
            ",[Document_Series], [Document_Number], [Password_Sotrudnika], [Login_Sotrudnika], [Name_Dolgnost], [Dolgnost_ID] FROM [dbo].[Sotrudniki] INNER JOIN [dbo].[Dolgnost] ON " +
            " [dbo].[Sotrudniki].[Dolgnost_ID] = [dbo].[Dolgnost].[ID_Dolgnost]",

        qrPostavshik = "SELECT [ID_Postavshik],[Name_Postavshik], [Midlle_Name_Postavshik], " +
         " [Last_Name_Postavshik], [Loign_Postavshika], [Password_Postavshika] FROM [dbo].[Postavshik]",

        qrJurnal = "SELECT [ID_Jurnal],[Name_Tovara], [Tip_Tovara], [Kolichestvo], [Price_of_Tovar], [Postavshik_ID], [Name_Postavshik] FROM [dbo].[Jurnal]" +
            " inner join [dbo].[Postavshik] on [dbo].[Postavshik].[ID_Postavshik] = [dbo].[Postavshik].[ID_Postavshik]",

        qrZakaz = "SELECT [ID_Zakaz],[Number_Komnati], [Tip_Kalyana], [Tip_Tabaka], [Jurnal_ID], [Name_Tovara] FROM [dbo].[Zakaz] inner join [dbo].[Jurnal] on [dbo].[Zakaz].[Jurnal_ID] = [dbo].[Jurnal].[ID_Jurnal]",

        qrCheck = "SELECT [ID_Check],[Number_Check], [Symma_Zakaza], [Data], [Zakaz_ID], [Sotrudnik_ID], [Number_Komnati], [Tip_Kalyana], [Tip_Tabaka], [Last_Name_Sotrudnik] FROM [dbo].[Check] inner join [dbo].[Zakaz] on [dbo].[Check].[Zakaz_ID] = [dbo].[Zakaz].[ID_Zakaz] inner join [dbo].[Sotrudniki] on [dbo].[Check].[Sotrudnik_ID] = [dbo].[Sotrudniki].[ID_Sotrudnik]";

        private static SqlCommand command = new SqlCommand("", connection);

        public static Int32 IDrecord, IDUser;
        //static
         void dtFill(DataTable table, string query)
         {
            command.CommandText = query;
            command.Notification = null;
            Dependency.AddCommandDependency(command);
            SqlDependency.Start(connection.ConnectionString);
            connection.Open();
            table.Load(command.ExecuteReader());
            connection.Close();
         }

        public void dbEnter(string login, string password)
        {
            command.CommandText = "SELECT COUNT(*) FROM [Mens_Club].[dbo].[Sotrudniki] WHERE [Login_Sotrudnika] = '" + login + "' and [Password_Sotrudnika] = '" + password + "'";
            connection.Open();
            IDUser = Convert.ToInt32(command.ExecuteScalar().ToString());
            connection.Close();
        }

        public void DolgnostFill()
        {
            dtFill(dtDolgnost, qrDolgnost);
        }

        public void SotrudnikiFill()
        {
            dtFill(dtSotrudniki, qrSotrudniki);
        }

        public void PostavshikFill()
        {
            dtFill(dtPostavshik, qrPostavshik);
        }

        public void JurnalFill()
        {
            dtFill(dtJurnal, qrJurnal);
        }

        public void ZakazFill()
        {
            dtFill(dtZakaz, qrZakaz);
        }

        public void CheckFill()
        {
            dtFill(dtCheck, qrCheck);
        }
    }
}

