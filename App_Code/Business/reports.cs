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
    private string connection_String = "Integrated Security=true;Initial Catalog=stocks;Data Source=DESKTOP-RDNCF3L";
    public reports()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public List<dot> getDots(List<user> users , int fromYear , int toYear)
    {
        List<dot> dots = new List<dot>();
       
        foreach (user u in users)
        {
            int from = fromYear, to = toYear, thisYear = u.createdYear;
            double current = u.initHolding;
            if (to < u.createdYear)
            {
                continue;
            }
            List<transaction> trans = new List<transaction>();
            #region getting the trans
            SqlConnection connection;
            SqlDataReader reader;
            string commandText = "";
            SqlCommand command;
            connection = new SqlConnection(connection_String);
            commandText = String.Format("SELECT * FROM [dbo].[transactions] where fromId = '" + u.id.ToString() +"' or toId = '" +  u.id.ToString() + "' order by year" );
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
                                t.fromId = reader.GetGuid(0);
                                t.toId = reader.GetGuid(1);
                                t.year = reader.GetInt32(2);
                                t.amount = reader.GetDouble(3);
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
            dot d = new dot();
            d.user = u;
            d.name = u.fullName;
            Dictionary<int, double> dic = new Dictionary<int, double>();
            dic.Add(u.createdYear, u.initHolding);
            foreach(transaction t in trans)
            {
                if(t.year > to)
                {
                    break;
                }
                if(u.id == t.fromId)
                {
                    current -= t.amount;
                }
                else if(u.id == t.toId)
                {
                    current += t.amount;
                }
                if (dic.ContainsKey(t.year))
                {
                    dic[t.year] = current;
                }
                else
                {
                    dic.Add(t.year, current);
                }
            }
            if(u.createdYear >= from)
            {
                while(from < u.createdYear)
                {
                    d.data.Add(0);
                    from++;
                }
                while(from <= to)
                {
                    if (dic.ContainsKey(from))
                    {
                        d.data.Add(dic[from]);
                    }
                    else
                    {
                        double value = dic[u.createdYear];
                        foreach(KeyValuePair<int , double> pair in dic)
                        {
                            if(from < pair.Key)
                            {
                                break;
                            }
                            value = pair.Value;
                        }
                        d.data.Add(value);
                    }
                    from++;
                }
            }
            else
            {
                while (from <= to)
                {
                    if (dic.ContainsKey(from))
                    {
                        d.data.Add(dic[from]);
                    }
                    else
                    {
                        double value = dic[u.createdYear];
                        foreach (KeyValuePair<int, double> pair in dic)
                        {
                            if (from < pair.Key)
                            {
                                break;
                            }
                            value = pair.Value;
                        }
                        d.data.Add(value);
                    }
                    from++;
                }
            }
            dots.Add(d);
        }
        return dots;
    }

}