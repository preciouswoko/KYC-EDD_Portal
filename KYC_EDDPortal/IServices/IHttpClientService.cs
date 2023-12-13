using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYC_EDDPortal.IServices
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string uri, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task<T> PostAsync<T>(string uri, object data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task<T> PutAsync<T>(string uri, object data, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string uri, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default);
    }
}
