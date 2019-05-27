using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for accounts
/// </summary>
public class accounts
{
    private string connection_String = "Integrated Security=true;Initial Catalog=stocks;Data Source=DESKTOP-RDNCF3L";
    public accounts()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public List<user> getAllUsers()
    {
        List<user> users = new List<user>();
        #region getting the users
        SqlConnection connection;
        SqlDataReader reader;
        string commandText = "";
        SqlCommand command;
        connection = new SqlConnection(connection_String);
        commandText = "SELECT * FROM [dbo].[account]";
        command = new SqlCommand();
        try
        {
            using (connection)
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = commandText;
                reader = command.ExecuteReader();
                using (reader)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                               user u = new user();
                            
                               u.id  = reader.GetGuid(0);
                            
                            
                                u.fisrtName = reader.GetString(1);
                            
                            
                                u.lastName = reader.GetString(2);
                            
                            
                                u.balance = reader.GetDouble(3);
                            u.initHolding = reader.GetDouble(4);
                            u.createdYear = reader.GetInt32(5);
                            u.fullName = u.fisrtName + " " + u.lastName;
                            users.Add(u);
                        }
                    }
                    else
                    {
                        throw new Exception("There are no rows in account");
                    }
                }
            }
        }
        catch (Exception e)
        {

        }
        #endregion
        return users;
    }
}