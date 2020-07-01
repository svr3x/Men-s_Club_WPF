using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Mens_ClubWPF
{
    class DBProcedures
    {
        private SqlCommand command = new SqlCommand("", DBConnection.connection);

        private void commandConfig(string config)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }

        public void resDolgnost_insert(string Name_Dolgnost, Int32 Oklad_Dolgnost)
        {
            commandConfig("Dolgnost_insert");

            command.Parameters.AddWithValue("@Name_Dolgnost", Name_Dolgnost);
            command.Parameters.AddWithValue("@Oklad_Dolgnost", Oklad_Dolgnost);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resDolgnost_update(Int32 ID_Dolgnost, string Name_Dolgnost, Int32 Oklad_Dolgnost)
        {
            commandConfig("Dolgnost_update");

            command.Parameters.AddWithValue("@ID_Dolgnost", ID_Dolgnost);
            command.Parameters.AddWithValue("@Name_Dolgnost", Name_Dolgnost);
            command.Parameters.AddWithValue("@Oklad_Dolgnost", Oklad_Dolgnost);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resDolgnost_delete(Int32 ID_Dolgnost)
        {
            commandConfig("Dolgnost_delete");

            command.Parameters.AddWithValue("@ID_Dolgnost", ID_Dolgnost);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resSotrudniki_insert(string Name_Sotrudnik,
            string Midlle_Name_Sotrudnik, string Last_Name_Sotrudnik,
            string Document_Series, string Document_Number, string Login_Sotrudnika, string Password_Sotrudnika,
            Int32 Dolgnost_ID)
        {
            commandConfig("Sotrudniki_insert");
            command.Parameters.AddWithValue("@Name_Sotrudnik", Name_Sotrudnik);
            command.Parameters.AddWithValue("@Midlle_Name_Sotrudnik", Midlle_Name_Sotrudnik);
            command.Parameters.AddWithValue("@Last_Name_Sotrudnik", Last_Name_Sotrudnik);
            command.Parameters.AddWithValue("@Document_Series", Document_Series);
            command.Parameters.AddWithValue("@Document_Number", Document_Number);
            command.Parameters.AddWithValue("@Login_Sotrudnika", Login_Sotrudnika);
            command.Parameters.AddWithValue("@Password_Sotrudnika", Password_Sotrudnika);
            command.Parameters.AddWithValue("@Dolgnost_ID", Dolgnost_ID);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resSotrudniki_update(Int32 ID_Sotrudnik, string Name_Sotrudnik, string Midlle_Name_Sotrudnik, string Last_Name_Sotrudnik,
            string Document_Series, string Document_Number, string Login_Sotrudnika, string Password_Sotrudnika,
            Int32 Dolgnost_ID)
        {
            commandConfig("Sotrudniki_update");
            command.Parameters.AddWithValue("@ID_Sotrudnik", ID_Sotrudnik);
            command.Parameters.AddWithValue("@Name_Sotrudnik", Name_Sotrudnik);
            command.Parameters.AddWithValue("@Midlle_Name_Sotrudnik", Midlle_Name_Sotrudnik);
            command.Parameters.AddWithValue("@Last_Name_Sotrudnik", Last_Name_Sotrudnik);
            command.Parameters.AddWithValue("@Document_Series", Document_Series);
            command.Parameters.AddWithValue("@Document_Number", Document_Number);
            command.Parameters.AddWithValue("@Login_Sotrudnika", Login_Sotrudnika);
            command.Parameters.AddWithValue("@Password_Sotrudnika", Password_Sotrudnika);
            command.Parameters.AddWithValue("@Dolgnost_ID", Dolgnost_ID);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        public void resSotrudniki_delete(Int32 ID_Sotrudnik)
        {
            commandConfig("Sotrudniki_delete");

            command.Parameters.AddWithValue("@ID_Sotrudnik", ID_Sotrudnik);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resPostavshik_insert(string Name_Postavshik, string Midlle_Name_Postavshik, string Last_Name_Postavshik,
            string Loign_Postavshika, string Password_Postavshika)
        {
            commandConfig("Postavshik_insert");
            command.Parameters.AddWithValue("@Name_Postavshik", Name_Postavshik);
            command.Parameters.AddWithValue("@Midlle_Name_Postavshik", Midlle_Name_Postavshik);
            command.Parameters.AddWithValue("@Last_Name_Postavshik", Last_Name_Postavshik);
            command.Parameters.AddWithValue("@Loign_Postavshika", Loign_Postavshika);
            command.Parameters.AddWithValue("@Password_Postavshika", Password_Postavshika);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resPostavshik_update(Int32 ID_Postavshik, string Name_Postavshik, string Midlle_Name_Postavshik, string Last_Name_Postavshik,
            string Loign_Postavshika, string Password_Postavshika)
        {
            commandConfig("Postavshik_update");
            command.Parameters.AddWithValue("@ID_Postavshik", ID_Postavshik);
            command.Parameters.AddWithValue("@Name_Postavshik", Name_Postavshik);
            command.Parameters.AddWithValue("@Midlle_Name_Postavshik", Midlle_Name_Postavshik);
            command.Parameters.AddWithValue("@Last_Name_Postavshik", Last_Name_Postavshik);
            command.Parameters.AddWithValue("@Loign_Postavshika", Loign_Postavshika);
            command.Parameters.AddWithValue("@Password_Postavshika", Password_Postavshika);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }


        public void resPostavshik_delete(Int32 ID_Postavshik)
        {
            commandConfig("Postavshik_delete");
            command.Parameters.AddWithValue("@ID_Postavshik", ID_Postavshik);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resJurnal_insert(string Name_Tovara, string Tip_Tovara, Int32 Kolichestvo, Int32 Price_of_Tovar, Int32 Postavshik_ID)
        {
            commandConfig("Jurnal_insert");
            command.Parameters.AddWithValue("@Name_Tovara", Name_Tovara);
            command.Parameters.AddWithValue("@Tip_Tovara", Tip_Tovara);
            command.Parameters.AddWithValue("@Kolichestvo", Kolichestvo);
            command.Parameters.AddWithValue("@Price_of_Tovar", Price_of_Tovar);
            command.Parameters.AddWithValue("@Postavshik_ID", Postavshik_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }


        public void resJurnal_update(Int32 ID_Jurnal, string Name_Tovara, string Tip_Tovara, Int32 Kolichestvo, Int32 Price_of_Tovar, Int32 Postavshik_ID)
        {
            commandConfig("Jurnal_update");
            command.Parameters.AddWithValue("@ID_Jurnal", ID_Jurnal);
            command.Parameters.AddWithValue("@Name_Tovara", Name_Tovara);
            command.Parameters.AddWithValue("@Tip_Tovara", Tip_Tovara);
            command.Parameters.AddWithValue("@Kolichestvo", Kolichestvo);
            command.Parameters.AddWithValue("@Price_of_Tovar", Price_of_Tovar);
            command.Parameters.AddWithValue("@Postavshik_ID", Postavshik_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resJurnal_delete(Int32 ID_Jurnal)
        {
            commandConfig("Jurnal_delete");
            command.Parameters.AddWithValue("@ID_Jurnal", ID_Jurnal);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resZakaz_insert(Int32 Number_Komnati, string Tip_Kalyana, string Tip_Tabaka, Int32 Jurnal_ID)
        {
            commandConfig("Zakaz_insert");
            command.Parameters.AddWithValue("@Number_Komnati", Number_Komnati);
            command.Parameters.AddWithValue("@Tip_Kalyana", Tip_Kalyana);
            command.Parameters.AddWithValue("@Tip_Tabaka", Tip_Tabaka);
            command.Parameters.AddWithValue("@Jurnal_ID", Jurnal_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resZakaz_update(Int32 ID_Zakaz, Int32 Number_Komnati, string Tip_Kalyana, string Tip_Tabaka, Int32 Jurnal_ID)
        {
            commandConfig("Zakaz_update");
            command.Parameters.AddWithValue("@ID_Zakaz", ID_Zakaz);
            command.Parameters.AddWithValue("@Number_Komnati", Number_Komnati);
            command.Parameters.AddWithValue("@Tip_Kalyana", Tip_Kalyana);
            command.Parameters.AddWithValue("@Tip_Tabaka", Tip_Tabaka);
            command.Parameters.AddWithValue("@Jurnal_ID", Jurnal_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resZakaz_delete(Int32 ID_Zakaz)
        {
            commandConfig("Zakaz_delete");
            command.Parameters.AddWithValue("@ID_Zakaz", ID_Zakaz);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }


        public void resCheck_insert(Int32 Number_Check, string Symma_Zakaza, string Data, Int32 Sotrudnik_ID, Int32 Zakaz_ID)
        {
            commandConfig("Check_insert");
            command.Parameters.AddWithValue("@Number_Check", Number_Check);
            command.Parameters.AddWithValue("@Symma_Zakaza", Symma_Zakaza);
            command.Parameters.AddWithValue("@Data", Data);
            command.Parameters.AddWithValue("@Sotrudnik_ID", Sotrudnik_ID);
            command.Parameters.AddWithValue("@Zakaz_ID", Zakaz_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resCheck_update(Int32 ID_Check, Int32 Number_Check, string Symma_Zakaza, string Data, Int32 Sotrudnik_ID, Int32 Zakaz_ID)
        {
            commandConfig("Check_update");
            command.Parameters.AddWithValue("@ID_Check", ID_Check);
            command.Parameters.AddWithValue("@Number_Check", Number_Check);
            command.Parameters.AddWithValue("@Symma_Zakaza", Symma_Zakaza);
            command.Parameters.AddWithValue("@Data", Data);
            command.Parameters.AddWithValue("@Sotrudnik_ID", Sotrudnik_ID);
            command.Parameters.AddWithValue("@Zakaz_ID", Zakaz_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void resCheck_delete(Int32 ID_Check)
        {
            commandConfig("Check_delete");
            command.Parameters.AddWithValue("@ID_Check", ID_Check);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
    
    }
}

