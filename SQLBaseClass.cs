using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Data;
using System.Data.SqlClient;

namespace Inzynieria_Projekt
{
    class SQLBaseClass
    {
        public static string connetionString;
        public static SqlConnection cnn;
        public static SqlCommand command;
        public static SqlDataAdapter adapter = new SqlDataAdapter();

        public static void openConnection()
        {
            connetionString = @"Data source=localhost;Database=PersonDatabase;Password=R00tpass123#;Integrated Security=True;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

        }
        public static void closeConnection()
        {
            cnn.Close();
        }
        public static void addToBase(string name, string surname, string login, string password, string phone, string city, string street, string house, string post, string email)
        {
            try
            {
                openConnection();
                string cmdString = "INSERT INTO users (name,surname,login,password,phonenumber,city,street,housenr,postcode,email) VALUES (@val1, @va2, @val3, @val4, @val5, @val6, @val7, @val8, @val9, @val10)";
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@val1", name);
                    comm.Parameters.AddWithValue("@val2", surname);
                    comm.Parameters.AddWithValue("@val3", login);
                    comm.Parameters.AddWithValue("@val4", password);
                    comm.Parameters.AddWithValue("@val5", phone);
                    comm.Parameters.AddWithValue("@val6", city);
                    comm.Parameters.AddWithValue("@val7", street);
                    comm.Parameters.AddWithValue("@val8", house);
                    comm.Parameters.AddWithValue("@val9", post);
                    comm.Parameters.AddWithValue("@val10", email);
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = cnn;
                }

                adapter = new SqlDataAdapter(command);
                closeConnection();
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message);
            }

        }

        public static void searchInBase()
        {
            try
            {
                openConnection();

                adapter = new SqlDataAdapter(command);
                closeConnection();
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message);
            }
        }
    }
}
