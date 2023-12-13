using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Models
{
    public class AccountRequest
    {
        [Key]
        public int Id { get; set; }
        public string RequestType { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string AccountType { get; set; }
        public string AccountProduct { get; set; }
        public string Address { get; set; }
        public string BranchCode { get; set; }
        public string CustomerType { get; set; }


        //public AccountRequest()
        //{
        //    Individual = new IndividualAccount();
        //    Corporate = new CorporateAccount();
        //}

        public DateTime? DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public string Nationality { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime? IdentificationExpiryDate { get; set; }
        public string RCNo { get; set; }
        public string NatureOfBusiness { get; set; }
        public bool SanctionList  { get; set; }
        public bool PEPList  { get; set; }
        public bool BlackList { get; set; }
        public string PurposeOfAccount { get; set; }
        public string Sourceoffunds { get; set; }
        public string AddressVerified { get; set; }
        public DateTime DateOfAddress { get; set; }
        public string AnticipatedVolume { get; set; }
        public string Typeofactivity { get; set; }
        public string Comment { get; set; }
        public string RequestId { get; set; }

    }
  

}
