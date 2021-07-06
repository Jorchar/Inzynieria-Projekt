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

        public static bool searchInBase(string login, string password)
        {
            
                try
                {
                    openConnection();
                    string cmdString = "SELECT * FROM user WHERE(login = @login AND password = @pasw);";
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
                        adapter = new MySqlDataAdapter(comm);
                        closeConnection();
                        return true;
                    }
                    else
                    {
                        adapter = new MySqlDataAdapter(comm);
                        closeConnection();
                        return false;
                    }
                }
                catch (Exception blad)
                {
                    MessageBox.Show(blad.Message);
                    return false;
                }
        }
        
        public static DataTable fillDataGrids()
        {
            try
            {
                openConnection();
                string cmdString = "SELECT * FROM products;";
                using (comm = new MySqlCommand())
                {
                    comm.CommandText = cmdString;
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = conn;
                }
                DataTable dt = new DataTable();
                dt.Load(comm.ExecuteReader());
                closeConnection();
                return dt;
                
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message);
                return null;
            }
        }

        public static Int32 getOrderNumber(string login)
        {
            try
            {
                openConnection();
                string cmdString = "SELECT COUNT(DISTINCT(order_number)) FROM Orders WHERE(userLogin = @login);";
                using (comm = new MySqlCommand())
                {
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@login", login);
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = conn;
                }

                object nazwa = comm.ExecuteScalar();
                if (nazwa != null)
                {
                    closeConnection();
                    return Int32.Parse(nazwa.ToString());
                }
                else { return 0; }

                
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static void refreshOrder(System.Windows.Controls.DataGrid table2, string login, int ordernr)
        {
            try
            {
                openConnection();
                string cmdString = "SELECT * FROM orders WHERE(userLogin = @login AND order_number = @ordernr);";
                using (comm = new MySqlCommand())
                {
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@login", login);
                    comm.Parameters.AddWithValue("@ordernr", ordernr);
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection = conn;
                }
                DataTable dt = new DataTable();
                adapter = new MySqlDataAdapter(comm);
                adapter.Fill(dt);
                table2.DataContext = dt;
                closeConnection();
            }
            catch (Exception blad)
            {
                MessageBox.Show(blad.Message);
            }
        }
        public static void addOrder(string name, int order_number, string userLogin, string productNumber)
        {
            try
            {
                openConnection();
                string cmdString = "INSERT INTO orders (name,order_number,userLogin,productNumber) VALUES (@name, @order_number, @userLogin, @productNumber);";
                using (comm = new MySqlCommand())
                {
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@name", name);
                    comm.Parameters.AddWithValue("@order_number", order_number);
                    comm.Parameters.AddWithValue("@userLogin", userLogin);
                    comm.Parameters.AddWithValue("@productNumber", productNumber);;
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
        public static void subOrder(string name, int order_number, string userLogin, string productNumber)
        {
            try
            {
                openConnection();
                string cmdString = "DELETE FROM orders WHERE(name = @name AND order_number = @order_number AND userLogin = @userLogin AND productNumber = @productNumber);";
                using (comm = new MySqlCommand())
                {
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@name", name);
                    comm.Parameters.AddWithValue("@order_number", order_number);
                    comm.Parameters.AddWithValue("@userLogin", userLogin);
                    comm.Parameters.AddWithValue("@productNumber", productNumber); ;
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
        public static void resetOrder(string login, int ordernr)
        {
            try
            {
                openConnection();
                string cmdString = "DELETE FROM orders WHERE(userLogin = @login AND order_number = @ordernr);";
                using (comm = new MySqlCommand())
                {
                    comm.CommandText = cmdString;
                    comm.Parameters.AddWithValue("@login", login);
                    comm.Parameters.AddWithValue("@ordernr", ordernr);
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
    }
}
