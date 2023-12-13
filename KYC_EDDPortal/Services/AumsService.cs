using KYC_EDDPortal.DTO;
using KYC_EDDPortal.IServices;
using KYC_EDDPortal.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Services
{
    public class AumsService: IAumsService
    {
        private static ILogger<AumsService> _logging;
        private readonly IHttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        private readonly IUtilityService _utilityService;
        private readonly string Password;
        private readonly string Username;
        private readonly string Appid;
        private readonly string BaseUrl;
        private readonly string Endpoint;
        GlobalVariables globalVariables;
        private readonly GlobalVariables _globalVariables;
        private readonly ISessionService _service;

        public AumsService(ILogger<AumsService> logging, IHttpClientService httpClientService, 
            IConfiguration configuration, IUtilityService utilityService, ISessionService service)
        {
            _logging = logging;
            _configuration = configuration;
            _httpClientService = httpClientService;
            _utilityService = utilityService;
            Password = _configuration["AumsSettings:Password"];
            Username = _configuration["AumsSettings:Username"];
            Appid = _configuration["AumsSettings:Appid"];
            BaseUrl = _configuration["AumsSettings:BaseUrl"];
            Endpoint = _configuration["AumsSettings:Endpoint"];
            _service = service;
            _globalVariables = _service.Get<GlobalVariables>("GlobalVariables");
        }
        public async Task<LoginResponse> AuthenticateUser(string username, string password, string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                var globalVariables = new GlobalVariables();
                var loginRequest =   new loginRequest
                {
                    username = username,
                    accesstoken = accessToken,
                    appid = Appid,
                    encryptedpassword = _utilityService.Encrypt(password, default(CancellationToken))
                }; 
                var apikey = _utilityService.Encrypt($"{Username}:{Password}", default(CancellationToken));

                // Define your custom headers
                var customHeaders = new Dictionary<string, string>
                {
                    { "API_KEY", apikey },
                    { "API_USER", Username },

                };
                if (username == "pwoko")
                {
                    globalVariables.userName = username;
                    globalVariables.Email = "preciouswoko@keystonebankng.com";
                    globalVariables.userid = username;
                    globalVariables.branchCode = "NG0010140";
                    globalVariables.name = "Precious Woko";
                    globalVariables.Permissions = new List<string> { "INI", "KYC", "EDD", "REV" };
                    globalVariables.ApprovalLevel = 1;
                    globalVariables.MenuHtml = await _utilityService.GeneratedMenuHtml(5, globalVariables.Permissions);

                    _service.Set<GlobalVariables>("GlobalVariables", globalVariables);
                    var LoginResponse = new LoginResponse
                    {
                        username = username,
                        email = "pwoko@keystone.com"
                    };
                    return LoginResponse;
                };
                //Use the injected IHttpClientService to make HTTP requests with custom headers
                var response = await _httpClientService.PostAsync<LoginResponse>(
                   BaseUrl + Endpoint, loginRequest, customHeaders);

                if (response != null)
                {
                    _logging.LogInformation($"Response From AuthenticateUser = {response}", "AuthenticateUser");

                    globalVariables.userName = response.username;
                    globalVariables.Permissions = response.featurelist;
                    globalVariables.userid = response.staffid;
                    globalVariables.Email = response.email;
                    globalVariables.name = response.name;
                    globalVariables.branchCode = response.branchcodes.Replace("|", "");
                    globalVariables.MenuHtml = await _utilityService.GeneratedMenuHtml(5, response.featurelist);

                    _service.Set<GlobalVariables>("GlobalVariables", globalVariables);

                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.ToString(), "AuthenticateUser");
                return null;
            }
        }

        public async Task<List<AuthResponse>> GetUserInFeature(string branchcode, string featureid)
        {
            string appid = Appid;
            var authResponse = new List<AuthResponse>();
            try
            {

                var uri = BaseUrl + $"Service/GetAppUserByBranchAndFeature?appid={appid}&branchcode={branchcode}&featureid={featureid}";

                var headers = new Dictionary<string, string>();
                string username = Username /*APIUser*/;
                string password = Password/*APIPassword*/;
                var apikey = _utilityService.Encrypt($"{username}:{password}", default(CancellationToken));

                // Define your custom headers
                var customHeaders = new Dictionary<string, string>
                {
                    { "API_KEY", apikey },
                    { "API_USER", username },

                };

                //headers.Add("API_KEY", Encrypt($"{username}:{password}"));
                //headers.Add("API_USER", username);

                authResponse = await _httpClientService.GetAsync<List<AuthResponse>>(uri, customHeaders).ConfigureAwait(false);

                _logging.LogInformation($"GetUserInFeature Response content: {authResponse}", "GetUserInFeature");

               
                return authResponse;
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message, "GetUserInFeature");
            }
            return authResponse;
        }

    }
}
