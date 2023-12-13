using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.DTO
{
    public class GlobalVariables
    {
        public int saltValue { get; set; }
        public string userid { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public int TenantId { get; set; }
        public string[] sarrayt1 { get; set; }
        public string ptitle { get; set; }
        public string ReportPeriod { get; set; }
        public string viewflag { get; set; }
        public string filep { get; set; }
        public string tempvar { get; set; }
        public string branchCode { get; set; }
        public string region { get; set; }
        public string MenuHtml { get; set; }
        public string ReportHtml { get; set; }
        public string Email { get; set; }
        public List<string> Permissions { get; set; }
        public int RoleId { get; set; }
        public int ApprovalLevel { get; set; }

        public static implicit operator Guid(GlobalVariables v)
        {
            throw new NotImplementedException();
        }
    }
}
