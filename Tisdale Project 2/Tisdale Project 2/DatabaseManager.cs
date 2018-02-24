using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tisdale_Project_2
{
    static class DatabaseManager
    {
        static string databaseString = "CTASV20R2DRW.tamuct.edu";
        static string databaseNameString = "Initial Catalog = ChristopherFirstAssignment";
        static string userString = "User ID = Christopher";
        static string passwordString = "Password = Tisdale016";

        static string connectionString = String.Format("Data Source= {0}; {1}; {2}; {3};",
            databaseString, databaseNameString, userString, passwordString);

        public static string[] getCustomerNames()
        {
            List<string> customerNames = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT p.LastName + ', ' + p.FirstName FROM ChristopherFirstAssignment.db_owner.Person AS p JOIN Customer c ON c.ID = p.ID";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                customerNames.Add(""); //blank value for combo box

                while (reader.Read())
                {
                    customerNames.Add(reader[0].ToString());
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return customerNames.ToArray();

        }

        public static string[] getEmployeeNames()
        {
            List<string> employeeNames = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT p.LastName + ', ' + p.FirstName FROM ChristopherFirstAssignment.db_owner.Person AS p JOIN Employee e ON e.ID = p.ID";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();

                employeeNames.Add(""); //blank value for combo box

                while (reader.Read())
                {
                    employeeNames.Add(reader[0].ToString());
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return employeeNames.ToArray();

        }

        public static DataTable viewCustomer(string lastName, string firstName)
        {
            DataTable dataRecord = null;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT * FROM ChristopherFirstAssignment.db_owner.Person WHERE LastName = '" + lastName + "' AND FirstName = '" + firstName + "'";

                con.Open();

                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);



                dataRecord = new DataTable();
                sqlDataAdap.Fill(dataRecord);

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return dataRecord;
        }

        public static int getNextIdNumber()
        {
            int id = 0;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT TOP 1 ID FROM ChristopherFirstAssignment.db_owner.Person ORDER BY ID DESC";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();


                while (reader.Read())
                {
                    id = Convert.ToInt16(reader[0]);
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return ++id;
        }

        public static int getPersonIdNumber(string firstName, string lastName)
        {
            int id = 0;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT ID FROM ChristopherFirstAssignment.db_owner.Person " +
                    "WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "'";

                con.Open();
                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();


                while (reader.Read())
                {
                    id = Convert.ToInt16(reader[0]);
                }

                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return id;
        }

        public static void addCustomer(string firstName, string lastName, string address, string dateOfBirth, string favoriteDepartment)
        {
            int id = DatabaseManager.getNextIdNumber();

            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = String.Format("INSERT INTO [ChristopherFirstAssignment].[db_owner].[Person]" +
                    "([ID],[FirstName],[LastName],[Address],[DateOfBirth])" +
                    "VALUES('{0}','{1}','{2}','{3}','{4}')",
                    id, firstName, lastName, address, dateOfBirth);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                sqlCmd.CommandText = String.Format("INSERT INTO [ChristopherFirstAssignment].[db_owner].[Customer]" +
                    "([ID],[FavoriteDepartment])" +
                    "VALUES('{0}','{1}')",
                    id, favoriteDepartment);

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        public static void deleteCustomer(int personID)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = String.Format("DELETE FROM [ChristopherFirstAssignment].[db_owner].[Person]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();

                sqlCmd.CommandText = String.Format("DELETE FROM [ChristopherFirstAssignment].[db_owner].[Customer]" +
                    "WHERE ID = '" + personID + "'");

                con.Open();
                sqlCmd.ExecuteNonQuery();
                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        public static string getCustomerToModify(int personID)
        {
            //st<string> customerInfo = new List<string>();
            string customerInfo = "";
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = String.Format("SELECT FirstName + '|' + LastName + '|' + Address + '|' + convert(nvarchar(10), DateOfBirth, 101) FROM [ChristopherFirstAssignment].[db_owner].[Person]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();

                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();


                while (reader.Read())
                {
                    customerInfo = (reader[0].ToString());
                }


                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return customerInfo;
        }

        public static string getCustomerFavoriteDepartment(int personID)
        {
            string customerFavDept = "";
            //List<string> customerfavDept = new List<string>();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);


                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = String.Format("SELECT FavoriteDepartment FROM [ChristopherFirstAssignment].[db_owner].[Customer]" +
                     "WHERE ID = '" + personID + "'");

                con.Open();

                SqlDataReader reader;
                reader = sqlCmd.ExecuteReader();


                while (reader.Read())
                {
                    customerFavDept = (reader[0].ToString());
                }


                con.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("The server could not be reached, please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return customerFavDept;
        }
    }

}

    




