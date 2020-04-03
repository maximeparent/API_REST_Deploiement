using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Leads
{
    public long id { get; set; }
public string name { get; set; }
public long phone { get; set; }
public string email { get; set; }
public string businessname { get; set; }
public string projectname { get; set; }
public string department { get; set; }
public string description { get; set; }
public string message { get; set; }
public DateTime date { get; set; }
// public Customers customers { get; set; }
public List<Customers> customers { get; set; } = new List<Customers>();
}
