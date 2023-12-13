using KYC_EDDPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.IServices
{
    public interface IOracleDataService
    {
        Task<AccountDetailsInfo> ExecuteQuery(string accountNumber);
        Task<CustomerInfo> GetCustomerInfo(string customerId);
    }
}
