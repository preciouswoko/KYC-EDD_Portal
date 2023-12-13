using KYC_EDDPortal.DTO;
using KYC_EDDPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.IServices
{
    public interface IRequestService
    {
        Task<RequestInfo> FetchDetail(string Nubam);
        Task<bool> InsertRecord(AccountRequest request);
        Task<bool> InsertReview(ReviewTable request);
         Task<List<AccountRequest>> GetRecords();
       string InsertComment(Comment request);
        Task<AccountRequest> GetRecord(string requestid);
        string UpdateRecord(AccountRequest request);
        Task<ReviewTable> GetReview(string requestid);
        string UpdateReview(ReviewTable request);
        Task<List<AccountRequest>> GetRecordsbyRequestType(string type);
    }
}
