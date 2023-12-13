using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KYC_EDDPortal.DTO;
using KYC_EDDPortal.IServices;
using KYC_EDDPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KYC_EDDPortal.Controllers
{
    public class MenuController : Controller
    {
        private GlobalVariables globalVariables;
        private readonly GlobalVariables _globalVariables;
        private readonly IUtilityService _utils;
        private readonly ISessionService _service;
        public MenuController(IUtilityService utils, IHttpContextAccessor hcontext, ISessionService service)
        {
            _utils = utils;
            _service = service;
            _globalVariables = _service.Get<GlobalVariables>("GlobalVariables");
        }


        //int all_ctr = 1; string str1; string tr1;

        public ActionResult ItemRun(string id)
        {
            //// check he has accesss
            string str1;
            int pos1 = id.IndexOf("[]");
            string code1 = id.Substring(0, pos1);
            //int pos2 = id.IndexOf("[]", pos1 + 2);
            //string type1 = id.Substring(pos1 + 2, pos2 - pos1 - 2);
            //string optiony = id.Substring(pos2 + 2);


            // check for self services
            var menu = MenuItemRepository.Menus().Where(x => x.Code == code1).FirstOrDefault();
            if (menu == null)
                return RedirectToAction("Index", "Home");

            if (string.IsNullOrWhiteSpace(menu.Controller))
                return RedirectToAction("Index", "home");

            if (!string.IsNullOrWhiteSpace(menu.Parameter))
            {
                string ancs = Ccheckg.convert_pass2("pc=" + menu.Parameter);
                return RedirectToAction(menu.Action.Trim(), menu.Controller.Trim(), new { anc = ancs });
            }
            else
            {
                string p1 = "pc=" + code1;
                string ancs = Ccheckg.convert_pass2(p1);
                return RedirectToAction(menu.Action.Trim(), menu.Controller.Trim(), new { anc = ancs });
            }

        }


    }
}