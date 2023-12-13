using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KYC_EDDPortal.DTO;
using KYC_EDDPortal.IServices;
using KYC_EDDPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KYC_EDDPortal.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAumsService _authService;
        private static ILogger<AuthController> _logging;
       
        public AuthController(IAumsService authService, ILogger<AuthController> logging)
        {
            _authService = authService;
            _logging = logging;


        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            try
            {
                _logging.LogInformation($"Inside LoginUser  at {DateTime.Now}", "LoginUser");
                if (!ModelState.IsValid)
                {
                    var responseModel = new LoginResponseModel
                    {
                        AccessToken = null,
                        Message = "Invalid request data."
                    };
                    ViewBag.Message = "Invalid request data.";
                    _logging.LogInformation(responseModel.ToString(), "Login");

                    _logging.LogInformation($"Outside LoginUser  at {DateTime.Now}", "LoginUser");

                    return View("Login");
                }


                // Call the authentication service to validate the user
                var accessToken = await _authService.AuthenticateUser(
                    loginRequest.Username,
                    loginRequest.Password,
                    loginRequest.AccessToken,
                    HttpContext.RequestAborted
                );

                if (accessToken != null)
                {
                    // Authentication successful, return the access token or user data
                    var responseModel = new LoginResponseModel
                    {
                        AccessToken = accessToken.username,
                        Message = "Authentication successful."
                    };
                    _logging.LogInformation(responseModel.ToString(), "Login");
                    ViewBag.Message = "Authentication successful.";

                    //return Ok(responseModel);
                    _logging.LogInformation($"Outside LoginUser  at {DateTime.Now}", "LoginUser");

                   // return RedirectToAction("Initiation", "Home");
                     return RedirectToAction("Index");
                }
                else
                {
                    // Authentication failed
                    var responseModel = new LoginResponseModel
                    {
                        AccessToken = null,
                        Message = "Invalid credentials."
                    };
                    ViewBag.Message = "Invalid credentials.";
                    _logging.LogInformation(responseModel.ToString(), "Login");

                    _logging.LogInformation($"Outside LoginUser  at {DateTime.Now}", "LoginUser");

                    return View("Login");
                }

            }
            catch (Exception ex)
            {
                _logging.LogError($"Exception{ex.ToString()} at {DateTime.Now}", "LoginUser");
                throw;
            }

        }

    }
}