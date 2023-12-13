using Dapper;
using KYC_EDDPortal.Data;
using KYC_EDDPortal.IServices;
using KYC_EDDPortal.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Services
{
    public class OracleDataService : IOracleDataService
    {
        private readonly DapperDbContext _context;
        private readonly IConfiguration _configuration;
        private static ILogger<OracleDataService> _logging;

        public OracleDataService(IConfiguration configuration, DapperDbContext context, ILogger<OracleDataService> logging)
        {
            _context = context;
            _configuration = configuration;
            _logging = logging;
        }

        public async Task<AccountDetailsInfo> ExecuteQuery(string accountNumber)
        {
            try
            {


                using (var connection = _context.CreateConnection())
                {
                    connection.Open();


                    string query = @"select a.currency_code currency ,a.branch_code BranchCode,  b.customer_email1 CustomerEmail, a.account_no T24AccountNo, a.nuban_account_no AccountNo,a.customer_id CustomerID
                                            ,b.customer_dob dob,b.bvn,CONCAT(CONCAT(b.customer_phone1 ,']'), b.customer_sms) CustomerPhone,b.customer_sms
                                            ,a.type_code,b.customer_name AccountName,b.customer_gender  Gender            
                                            from infobis.accounts_trans a, infobis.customers b where a.account_no=infobis.fn_gett24account(:accountNumber)
                                            and a.customer_id=b.customer_id";
                    // Use Dapper to execute the query and return a list of dynamic objects
                    var result = await connection.QueryFirstOrDefaultAsync<AccountDetailsInfo>(query, new { accountNumber });

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message);
                return null;
            }
        }

        public async Task<CustomerInfo> GetCustomerInfo(string customerId)
        {
            try
            {


                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    string query = @"select a.customer_id,a.customer_name,a.customer_rc,a.incorp_date,a.customer_dob,a.customer_gender,
                                        a.nationality,a.occupation,a.employer,a.legal_issue_date,a.legal_iss_auth,a.legal_exp_date,
                                        a.legal_id from infobis.customers a where customer_id=(:customerId)";
               
                    var result = await connection.QueryFirstOrDefaultAsync<CustomerInfo>(query, new { customerId });

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message);
                return null;
            }
        }
    }
}
