using KYC_EDDPortal.Data;
using KYC_EDDPortal.DTO;
using KYC_EDDPortal.IServices;
using KYC_EDDPortal.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Services
{
    public class RequestService : IRequestService
    {
        private readonly GlobalVariables _globalVariables;
        private readonly IOracleDataService _oracleDataService;
        private readonly ISessionService _session;
        private static ILogger<RequestService> _logging;
        private readonly ApplicationDbContext _dbcontext;

        public RequestService(ISessionService session, IOracleDataService oracleDataService,
            ILogger<RequestService> logging, ApplicationDbContext dbcontext)
        {
            _oracleDataService = oracleDataService;
            _session = session;
            _globalVariables = _session.Get<GlobalVariables>("GlobalVariables");
            _logging = logging;
            _dbcontext = dbcontext;
        }
        public async Task<RequestInfo> FetchDetail(string Nubam)
        {
            try
            {
                var customer = await _oracleDataService.GetCustomerInfo(Nubam);
                //var cancellationTokenSource = new CancellationToken();
                //CancellationToken cancellationToken = cancellationTokenSource;

                var dataTable = await _oracleDataService.ExecuteQuery(Nubam);
                
                if (dataTable == null) return null;
                var result = new RequestInfo
                {
                    RCNo = customer.Customer_Rc,
                    AccountName = dataTable.AccountName,
                    AccountNumber = Nubam,
                    BranchCode = dataTable.BranchCode,
                    CustomerID = dataTable.CustomerID,
                    CustomerName = customer.Customer_Name,
                    DateOfBirth = ParseDate(customer.Customer_Dob),
                    IdentificationExpiryDate = ParseDate(customer.Legal_Exp_Date),
                    Employer = customer.Employer,
                    IdentificationNumber = customer.Legal_Id,
                    IdentificationType = customer.Legal_Iss_Auth,
                    Nationality = customer.Nationality,
                    Occupation = customer.Occupation



                };

                return result;
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.ToString(), "FetchDetail");
                return null;
            }
        }
        public static string ParseDate(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                

                return dateOfBirth.ToString("MM/dd/yyyy");
            }
            else
            {
                // Handle invalid date format
               _logging.LogError("Invalid date format", "ParseDate");
                return null;
            }
        }
        public async Task<bool> InsertRecord(AccountRequest request)
        {
            try
            {
                await _dbcontext.AccountRequests.AddAsync(request);
                int numberOfChanges = _dbcontext.SaveChanges();

                if (numberOfChanges > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                _logging.LogError(ex.Message.ToString());
                return false;
            }
        }
        public async Task<bool> InsertReview(ReviewTable request)
        {
            try
            {
                await _dbcontext.ReviewTables.AddAsync(request);
                int numberOfChanges = _dbcontext.SaveChanges();

                if (numberOfChanges > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message.ToString());
                return false;
            }
        }
        public async Task<List<AccountRequest>> GetRecords()
        {
            try
            {
                var requests = new List<AccountRequest>();
                var getreview = _dbcontext.ReviewTables.Where(x => x.ToBeAuthroiziedBy == _globalVariables.branchCode && x.Status != "Approved").ToList();
                foreach (var item in getreview)
                {
                    var getrequests = _dbcontext.AccountRequests.FirstOrDefault(x => x.RequestId == item.RequestId);
                    if (getrequests == null)
                    {
                        continue;
                    }
                    requests.Add(getrequests);
                }
                return requests;
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message.ToString());
                throw;
            }
        }
        public string InsertComment(Comment request)
        {
            try
            {
                 _dbcontext.Comments.Add(request);
                int numberOfChanges = _dbcontext.SaveChanges();

                if (numberOfChanges > 0)
                {
                    return "Successfully";
                }
                return "UnSuccessful";

            }
            catch (Exception ex)
            {
                _logging.LogError(ex.ToString(), "InsertComment");
                return "Error";
            }

        }
        public async Task<AccountRequest> GetRecord(string requestid)
        {
            return _dbcontext.AccountRequests.FirstOrDefault(x => x.RequestId == requestid);
        }
        public async Task<ReviewTable> GetReview(string requestid)
        {
            return _dbcontext.ReviewTables.FirstOrDefault(x => x.RequestId == requestid);
        }
        public string UpdateRecord(AccountRequest request)
        {
            try
            {
                _dbcontext.AccountRequests.Update(request);
                int numberOfChanges = _dbcontext.SaveChanges();

                if (numberOfChanges > 0)
                {
                    return "Successfully";
                }
                return "UnSuccessful";

            }
            catch (Exception ex)
            {
                _logging.LogError(ex.ToString(), "InsertComment");
                return "Error";
            }

        }
        public string UpdateReview(ReviewTable request)
        {
            try
            {
                _dbcontext.ReviewTables.Update(request);
                int numberOfChanges = _dbcontext.SaveChanges();

                if (numberOfChanges > 0)
                {
                    return "Successfully";
                }
                return "UnSuccessful";

            }
            catch (Exception ex)
            {
                _logging.LogError(ex.ToString(), "InsertComment");
                return "Error";
            }

        }
        public async Task<List<AccountRequest>> GetRecordsbyRequestType(string type)
        {
            try
            {
                var requests = new List<AccountRequest>();

                var getreview = _dbcontext.ReviewTables.Where(x => x.Status == "Approved" && x.AuthorizedByName != null && x.ReviewStatus != "Approved").ToList();
                foreach (var item in getreview)
                {
                    var getrequests = _dbcontext.AccountRequests.FirstOrDefault(x => x.RequestId == item.RequestId && x.RequestType == type);
                    if(getrequests == null)
                    {
                        continue;
                    }
                    requests.Add(getrequests);
                }
                return requests;
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message.ToString());
                throw;
            }
        }
    }
}
