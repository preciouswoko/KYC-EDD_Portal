using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.IServices
{
    public interface IEmailService
    {
        void SmtpSendMail(string strTo, string StrBody, string StrSubject, string strFrom = null);
    }
}
