using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Data;
using MySql.Data.MySqlClient;

namespace Inzynieria_Projekt
{
    class SQLBaseClass
    {
        public static MySqlConnection conn;
        public static string connetionString;
        //public static SqlConnection cnn;
        public static MySqlCommand comm;
        public static MySqlDataAdapter adapter = new MySqlDataAdapter();

        public static void openConnection()
        {
            connetionString = "server=localhost;port=3306;uid=root;pwd=R00tpass123#;database=PersonDatabase;";
            conn = new MySqlConnection(connetionString);
            conn.Open();

        }
        public static void closeConnection()
        {
            conn.Close();
        }
        public static void addToBase(string name, string surname, string login, string password, string phone, string city, string street, string house, string post, string email)
        {
            try
            {
                openConnection();
                string cmdString = "INSERT INTO user (name,surname,login,password,phonenumber,city,street,housenr,postcode,email) VALUES (@val1, @val2, @val3, @val4, @val5, @val6, @val7, @val8, @val9, @val10);";
                using (comm = new MySqlCommand())
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
                    comm.Connection = conn;
                }
                comm.ExecuteNonQuery();
                adapter = new MySqlDataAdapter(comm);
                closeConnection();
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message);
            }

        }

        public static void searchInBase(string login, string password)
        {
            
                try
                {
                    openConnection();
                    string cmdString = "SELECT COUNT(*) FROM User WHERE(login = @login AND password = @pasw);";
                    using (comm = new MySqlCommand())
                    {
                        comm.CommandText = cmdString;
                        comm.Parameters.AddWithValue("@login", login);
                        comm.Parameters.AddWithValue("@pasw", password);
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.Connection = conn;
                    }
                    MySqlDataReader nazwa = comm.ExecuteReader();
                    if (nazwa.HasRows)
                    {
                        MessageBox.Show("znaleziono");
                        adapter = new MySqlDataAdapter(comm);
                        closeConnection();
                    }
                    else
                    {
                        adapter = new MySqlDataAdapter(comm);
                        closeConnection();
                    }
                }
                catch (Exception blad)
                {
                    MessageBox.Show(blad.Message);
                }
        }
        
    }
}
