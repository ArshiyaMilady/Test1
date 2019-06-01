using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcFamilyMeetings.Controllers
{
    public class FamliyDataController : Controller
    {
        public IActionResult Index()
        {
            // برای ادمین و کاربران ارشد، جدول اعضا را نشان دهد
            if (App_Code.Global.LoginMember.Level < 100)
                TempData["Show_Member"] = "Y";
            //ViewData["NavBar"] = "";
            //if (App_Code.Global.LoginMember == null)
            //{
            //    ViewData["NavBar"] = "HIDE";
            //    return RedirectToAction("Index", "Login");
            //}

            //ViewData["Show_Member"] = "N";
            //// برای ادمین و کاربران ارشد، جدول اعضا را نشان دهد
            //if (App_Code.Global.LoginMember.Level < 100)
            //    ViewData["Show_Member"] = "Y";

            ViewData["Member_Name"] = App_Code.Global.LoginMember.Id + " - "
                + App_Code.Global.LoginMember.FirstName + " " + App_Code.Global.LoginMember.LastName;
            return View();
        }




    }
}