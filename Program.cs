using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;

public class Program
{
    static string connectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=sa123456;"; // Replace with your actual connection string

    static void PrintAllContacts()
    {
            
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Contacts";

            SqlCommand command = new SqlCommand(query, connection);
            
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int contactID = (int)reader["ContactID"];
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];
                        string email = (string)reader["Email"];
                        string phone = (string)reader["Phone"];
                        string address = (string)reader["Address"];
                        int countryID = (int)reader["CountryID"];

                        Console.WriteLine($"Contact ID: {contactID}");
                        Console.WriteLine($"Name: {firstName} {lastName}");
                        Console.WriteLine($"Email: {email}");
                        Console.WriteLine($"Phone: {phone}");
                        Console.WriteLine($"Address: {address}");
                        Console.WriteLine($"Country ID: {countryID}");
                        Console.WriteLine();
                    }

                    reader.Close();
                    connection.Close();

                }


                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            
    }

    static void PrintAllContactByFirstNameAndCountryID(string FirstName,int CountryID)
    {
        string query = "SELECT * FROM Contacts WHERE FirstName = @FirstName AND CountryID = @CountryID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            // Reglage des parametre (x,y)
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())


                    {
                        int contactID = (int)reader["ContactID"];
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];
                        // handle null value returns
                        string email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];

                        string phone = (string)reader["Phone"];
                        string address = (string)reader["Address"];
                        int countryID = (int)reader["CountryID"];

                        Console.WriteLine($"Contact ID: {contactID}");
                        Console.WriteLine($"Name: {firstName} {lastName}");
                        Console.WriteLine($"Email: {email}");
                        Console.WriteLine($"Phone: {phone}");
                        Console.WriteLine($"Address: {address}");
                        Console.WriteLine($"Country ID: {countryID}");
                        Console.WriteLine();
                    }

                    // WE CAN ALSO NOT USE CLOSE BECAUSE USING () {} DOSE THAT FOR US 
                   // reader.Close();
                    //conn.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur:  " + ex.Message);
            }
        }
    }


    static void SearchContactStartWith(string StartWith)
    {
        string query = "SELECT * FROM Contacts WHERE FirstName LIKE '' + @StartWith + '%'";
        using (SqlConnection cnx = new SqlConnection(connectionString))

        using (SqlCommand _command = new SqlCommand(query, cnx))
        {
           
            _command.Parameters.AddWithValue("@StartWith", StartWith);

            try
            {
                cnx.Open();
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    int contactID = (int)reader["ContactID"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    // handle null value returns
                    string email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];

                    string phone = (string)reader["Phone"];
                    string address = (string)reader["Address"];
                    int countryID = (int)reader["CountryID"];

                    Console.WriteLine($"Contact ID: {contactID}");
                    Console.WriteLine($"Name: {firstName} {lastName}");
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine($"Phone: {phone}");
                    Console.WriteLine($"Address: {address}");
                    Console.WriteLine($"Country ID: {countryID}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }




        }



    }

    static void SearchContactEndtWith(string EndWith)
    {
        string query = "SELECT * FROM Contacts WHERE FirstName LIKE '%' + @EndWith + ''";
        using (SqlConnection cnx = new SqlConnection(connectionString))

        using (SqlCommand _command = new SqlCommand(query, cnx))
        {

            _command.Parameters.AddWithValue("@EndWith", EndWith);

            try
            {
                cnx.Open();
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    int contactID = (int)reader["ContactID"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    // handle null value returns
                    string email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];

                    string phone = (string)reader["Phone"];
                    string address = (string)reader["Address"];
                    int countryID = (int)reader["CountryID"];

                    Console.WriteLine($"Contact ID: {contactID}");
                    Console.WriteLine($"Name: {firstName} {lastName}");
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine($"Phone: {phone}");
                    Console.WriteLine($"Address: {address}");
                    Console.WriteLine($"Country ID: {countryID}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }




        }



    }


    // GEt only one value 
    static string GetFirstName (int ContactID)
    {
        
        string Query = "SELECT FirstName FROM Contacts WHERE ContactID = @ContactID";

        using (SqlConnection cnx = new SqlConnection(connectionString))
        
      
        using (SqlCommand CMD = new SqlCommand(Query, cnx))
        {
            CMD.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;

            try
            {
                cnx.Open();
                object Result = CMD.ExecuteScalar();

                return Result?.ToString() ?? string.Empty;



            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                return null;
            }
        }

        
    }

    public static void Main()
    {
        // PRINT ALL 
        //Console.WriteLine("__________ALL________");
        //PrintAllContacts();
        //Console.WriteLine("__________By First Name & Country________");
        //// PRINT BY PARAMETER 
        //PrintAllContactByFirstNameAndCountryID("Jane", 1);
        //Console.WriteLine("________Start With 'j%'__________");
        ////'LIKE' StatWith search 

        //SearchContactStartWith("J");
        //Console.WriteLine("_________End with '%e'_________");

        //SearchContactEndtWith("e");

        Console.WriteLine("_________Search only one value _________");
        GetFirstName(2);
        Console.ReadKey();
    }

}
