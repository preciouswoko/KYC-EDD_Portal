using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Models
{
    public class CustomerInfo
    {

        public string Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Rc { get; set; }
        public string Incorp_Date { get; set; }
        public string Customer_Dob { get; set; }
        public string Customer_Gender { get; set; }
        public string Nationality { get; set; }
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public string Legal_Issue_Date { get; set; }
        public string Legal_Iss_Auth { get; set; }
        public string Legal_Exp_Date { get; set; }
        public string Legal_Id { get; set; }
    }
}
