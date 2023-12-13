using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYC_EDDPortal.IServices
{
    public interface IUtilityService
    {
        string Encrypt(string plaintext, CancellationToken cancellation);
        Task<string> GeneratedMenuHtml(int RoleId, List<string> permissions);
    }
}
