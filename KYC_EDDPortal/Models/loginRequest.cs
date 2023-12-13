using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Models
{
    public class loginRequest
    {
        public string username { get; set; }
        public string accesstoken { get; set; }
        public string appid { get; set; }
        public string encryptedpassword { get; set; }

    }
}
