using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KYC_EDDPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using KYC_EDDPortal.DTO;
using KYC_EDDPortal.IServices;
using KYC_EDDPortal.Enums;
using Microsoft.Extensions.Logging;

namespace KYC_EDDPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionService _session;
        private readonly IRequestService _service;
        private static ILogger<HomeController> _logging;

        GlobalVariables globalVariables;
        private readonly GlobalVariables _globalVariables;
        public HomeController(ISessionService session, IRequestService service, ILogger<HomeController> logging)
        {
            _session = session;
            _globalVariables = _session.Get<GlobalVariables>("GlobalVariables");
            _service = service;
            _logging = logging;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> FetchAccountDetails(string accountNumber)
        {
            
            if (accountNumber == null) return null;
            var getaccountdetail = await _service.FetchDetail(accountNumber.Trim());
            
            if (getaccountdetail == null) return null;
           // _logging.LogInformation(JsonConvert.SerializeObject(getaccountdetail), "FetchAccountDetails");

            return Json(getaccountdetail);
        }
        [HttpGet]
        public async Task<IActionResult> Initiation()
        {
            try
            {
                var model = new AccountRequest();
                
                //model.Corporate.NatureOfBusiness = "Bakery";
                return View(model);
            }
            catch(Exception ex)
            {
                _logging.LogError(ex.Message, "Initiation");
            }
            //ViewBag.SourceOfFunds = Enum.GetValues(typeof(Sourceofunds))
            //                 .Cast<Sourceofunds>()
            //                 .ToList();
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Initiation(AccountRequest model)
        {
            try
            {
                string dateTimeString = DateTime.Now.ToString("yyyy/MM/ddHHmmss");
                string formattedDateTime = dateTimeString.Replace("/", "");
                var authorize = new ReviewTable
                {
                    RequestByemail = _globalVariables.Email,
                    RequestByName = _globalVariables.name,
                    RequestByUsername = _globalVariables.userName,
                    ToBeAuthroiziedBy = _globalVariables.branchCode,
                    RequestId = formattedDateTime
                };
                
                
                model.RequestId = formattedDateTime;
                var insertreview = await _service.InsertReview(authorize);
                var insert = await _service.InsertRecord(model);
                if (insert == true && insertreview == true )
                {
                    TempData["ResultMessage"] = "Successfully Inserted Record";
                }
                else
                {
                    TempData["ResultMessage"] = "Error: Failed to Insert the record.";
                }
                return RedirectToAction("Initiation");
            }catch(Exception ex)
            {
                _logging.LogError(ex.InnerException.Message);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Review()
        {
            var getdetails = await _service.GetRecords();
            return View(getdetails);
        }
        [HttpPost]
        public async Task<IActionResult> Review(string requestId, string comment, string approvalStatus)
        {
            try
            {
                var getrecord = await _service.GetRecord(requestId);
                if(getrecord == null)
                {
                    TempData["ResultMessage"] = "Error:  record don't Exist.";
                    return RedirectToAction("Review");

                }

                var commentreq = new Comment()
                {
                    RequestID = requestId.ToString(),
                    CommentDate = DateTime.Now,
                    Remark = comment,
                    CommentBy = $"{_globalVariables.userName}",
                    Action = "Review",
                    Status = approvalStatus

                };
                var addcomment = _service.InsertComment(commentreq);
                var getreview = await _service.GetReview(requestId);
                if (getreview == null)
                {
                    TempData["ResultMessage"] = "Error:  record don't Exist.";
                    return RedirectToAction("Review");

                }

                getreview.AuthorizedByEmail = _globalVariables.Email;
                getreview.AuthorizedByName = _globalVariables.name;
                getreview.AuthorizedByUsername = _globalVariables.userName;
                getreview.AuthorizedDate = DateTime.Now;
                getreview.Status = approvalStatus;
                var updatreview = _service.UpdateReview(getreview);

                var updatrequest = _service.UpdateRecord(getrecord);
                if(approvalStatus == "Rejected")
                {
                    TempData["ResultMessage"] = "Successfully resend Record to Initiator";
                    //send email to initiator
                }
                if (approvalStatus == "Approved")
                {
                    TempData["ResultMessage"] = "Successfully updated Record";
                }
                return RedirectToAction("Review");
            }
            catch(Exception ex)
            {
                _logging.LogError(ex.Message.ToString());
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> GenerateReport()
        {

            return View();
        }
        [HttpGet]
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            _session.Clear("GlobalVariables");
            return RedirectToAction("Login", "Auth");
        }
        public class ListItem
        {
            public long Value { get; set; }
            public string Text { get; set; }
        }
        [HttpPost]
        public ActionResult KeepAlive()
        {
            List<ListItem> loan1 = new List<ListItem>();
            ListItem ln2;
            ln2 = new ListItem() { Value = 0, Text = "" };
            loan1.Add(ln2);
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new SelectList(
                        loan1.ToArray(),
                        "Value",
                        "Text"));

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EDDReview()
        {
            var getdetails = await _service.GetRecordsbyRequestType(RequestType.EDD.ToString());
            return View(getdetails);
        }
        [HttpPost]
        public async Task<IActionResult> EDDReview(string requestId, string comment, string approvalStatus)
        {
            try
            {
                var getrecord = await _service.GetRecord(requestId);
                if (getrecord == null)
                {
                    TempData["ResultMessage"] = "Error:  record don't Exist.";
                    return RedirectToAction("Review");

                }

                var commentreq = new Comment()
                {
                    RequestID = requestId.ToString(),
                    CommentDate = DateTime.Now,
                    Remark = comment,
                    CommentBy = $"{_globalVariables.userName}",
                    Action = "EDDReview",
                    Status = approvalStatus

                };
                var addcomment = _service.InsertComment(commentreq);
                var getreview = await _service.GetReview(requestId);
                if (getreview == null)
                {
                    TempData["ResultMessage"] = "Error:  record don't Exist.";
                    return RedirectToAction("Review");

                }

                getreview.ReviewerEmail = _globalVariables.Email;
                getreview.ReviewerName = _globalVariables.name;
                getreview.ReviewerUsername = _globalVariables.userName;
                getreview.ReviewDate = DateTime.Now;
                getreview.ReviewStatus = approvalStatus;
                var updatreview = _service.UpdateReview(getreview);

                var updatrequest = _service.UpdateRecord(getrecord);
                if (approvalStatus == "Rejected")
                {
                    TempData["ResultMessage"] = "Successfully resend Record to Initiator";
                    //send email to initiator
                }
                if (approvalStatus == "Approved")
                {
                    TempData["ResultMessage"] = "Successfully updated Record";
                }
                return RedirectToAction("Review");
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message.ToString());
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> KYCReview()
        {
            var getdetails = await _service.GetRecordsbyRequestType(RequestType.KYC.ToString());
            return View(getdetails);
        }
        [HttpPost]
        public async Task<IActionResult> KYCReview(string requestId, string comment, string approvalStatus)
        {
            try
            {
                var getrecord = await _service.GetRecord(requestId);
                if (getrecord == null)
                {
                    TempData["ResultMessage"] = "Error:  record don't Exist.";
                    return RedirectToAction("Review");

                }

                var commentreq = new Comment()
                {
                    RequestID = requestId.ToString(),
                    CommentDate = DateTime.Now,
                    Remark = comment,
                    CommentBy = $"{_globalVariables.userName}",
                    Action = "KYCReview",
                    Status = approvalStatus

                };
                var addcomment = _service.InsertComment(commentreq);
                var getreview = await _service.GetReview(requestId);
                if (getreview == null)
                {
                    TempData["ResultMessage"] = "Error:  record don't Exist.";
                    return RedirectToAction("Review");

                }

                getreview.ReviewerEmail = _globalVariables.Email;
                getreview.ReviewerName = _globalVariables.name;
                getreview.ReviewerUsername = _globalVariables.userName;
                getreview.ReviewDate = DateTime.Now;
                getreview.ReviewStatus = approvalStatus;
                var updatreview = _service.UpdateReview(getreview);

                var updatrequest = _service.UpdateRecord(getrecord);
                if (approvalStatus == "Rejected")
                {
                    TempData["ResultMessage"] = "Successfully resend Record to Initiator";
                    //send email to initiator
                }
                if (approvalStatus == "Approved")
                {
                    TempData["ResultMessage"] = "Successfully updated Record";
                }
                return RedirectToAction("Review");
            }
            catch (Exception ex)
            {
                _logging.LogError(ex.Message.ToString());
                throw;
            }
        }
    }
}
