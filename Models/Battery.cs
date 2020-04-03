using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Battery
{
    public long id { get; set; }
    public string type_battery { get; set; }
    public string status { get; set; }
    public DateTime commissioning_date { get; set; }
    public DateTime last_inspection_date { get; set; }
    public string certificate_operations { get; set; }
    public string information { get; set; }
    public string notes { get; set; }
    public long employee_id { get; set; }
    
    [ForeignKey("building_id")]
    public Buildings buildings { get; set; }
}