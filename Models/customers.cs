using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Customers
{
public long creation_date { get; set; }
public string company_name { get; set; }
public long contact_fullname { get; set; }
public string contact_phone { get; set; }
public string company_email { get; set; }
public string company_description { get; set; }
public string service_technical_authority_fullname { get; set; }
public string service_technical_authority_phone { get; set; }
public string technical_manager_email { get; set; }
// public long user_id { get; set; }

// public string adress_id { get; set; }

// public Leads leads { get; set; }

// [ForeignKey("adress_id")]
//     public Customers adress_id { get; set; }

//     [ForeignKey("user_id")]
//     public Customers user_id { get; set; }

}

// create_table "customers", options: "ENGINE=InnoDB DEFAULT CHARSET=utf8", force: :cascade do |t|
//     t.datetime "creation_date"
//     t.string "company_name", default: "", null: false
//     t.string "contact_fullname", default: "", null: false
//     t.string "contact_phone", default: "", null: false
//     t.string "company_email", default: "", null: false
//     t.string "company_description"
//     t.string "service_technical_authority_fullname", default: "", null: false
//     t.string "service_technical_authority_phone", default: "", null: false
//     t.string "technical_manager_email", default: "", null: false
//     t.bigint "user_id"
//     t.bigint "adress_id"
//     t.index ["adress_id"], name: "index_customers_on_adress_id"
//     t.index ["user_id"], name: "index_customers_on_user_id"
//   end