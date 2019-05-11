using System;
using System.Collections.Generic;
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

        return new List<dot>();
    }

}