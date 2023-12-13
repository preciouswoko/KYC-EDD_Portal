using KYC_EDDPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYC_EDDPortal.IServices
{
    public interface IAumsService
    {
        Task<LoginResponse> AuthenticateUser(string username, string password, string accessToken, CancellationToken cancellationToken);
        Task<List<AuthResponse>> GetUserInFeature(string branchcode, string featureid);
    }
}
