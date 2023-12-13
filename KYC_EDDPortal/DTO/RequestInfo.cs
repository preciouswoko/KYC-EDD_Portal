using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.DTO
{
    public class RequestInfo
    {
        public string RCNo { get; set; }
        public string DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public string Nationality { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string IdentificationExpiryDate { get; set; }
        public string BranchCode { get; set; }

        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }

    }
}
