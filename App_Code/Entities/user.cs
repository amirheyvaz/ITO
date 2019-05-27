using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for user
/// </summary>
public class user
{
 
    public user(string first , string last)
    {
        id = new Guid();
        fisrtName = first;
        lastName = last;
        fullName = fisrtName + " " + lastName;
    }
    public user() { }
    public Guid id;
    public string fisrtName;
    public string lastName;
    public string Email;
    public int phoneNumber;
    public string fullName;
    public Decimal balance;
    public Decimal initHolding;
    public int createdYear;
}