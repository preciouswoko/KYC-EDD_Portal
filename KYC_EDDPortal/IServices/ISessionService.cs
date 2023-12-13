using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.IServices
{
    public interface ISessionService
    {
        void Set<T>(string key, T value);
        T Get<T>(string key);
        void Clear(string key);
    }
}
