using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for reports
/// </summary>
public class reports
{
    private string connection_String = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=stocks;Data Source=DESKTOP-RDNCF3L";
    public reports()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public List<dot> getDots(List<user> users , int fromYear , int toYear)
    {
        SqlConnection connection;
        SqlDataReader reader;
        string commandText = "";
        SqlCommand command;
        connection = new SqlConnection(connection_String);
        foreach (user u in users)
        {
            List<transaction> trans = new List<transaction>();
            #region getting the trans
            commandText = String.Format("SELECT * FROM [dbo].[transactions] where formId = '" + u.id.ToString() +"' or toId = '" +  u.id.ToString() + "' order by year" );
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
                                transaction t = new transaction();
                                t.id = reader.GetGuid(0);
                                t.fromId = reader.GetGuid(1);
                                t.toId = reader.GetGuid(2);
                                t.year = reader.GetInt32(3);
                                t.amount = reader.GetDecimal(4);
                                trans.Add(t);
                            }
                        }
                        else
                        {
                            throw new Exception("There are no rows in transactions");
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

            #endregion
            int from = fromYear , to = toYear , i =0;
            if(trans[trans.Count - 1].year < from || trans[0].year > to)
            {
                continue;
            }
            if(trans[i].year < from)
            {
                while (trans[i].year < from)
                {
                    i++;
                }
                
            }
            while (from <= to)
            {
                if(trans[i].year == from)
                {
                     i++;
                }
                from++;
            }
        }
        return new List<dot>();
    }

}