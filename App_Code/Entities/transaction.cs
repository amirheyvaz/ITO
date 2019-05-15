using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for transaction
/// </summary>
public class transaction
{
    public transaction()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Guid fromId;
    public Guid toId;
    public Guid id;
    public decimal amount;
    public int year;
}