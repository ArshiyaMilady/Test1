using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFamilyMeetings.Models;

namespace MvcFamilyMeetings.Controllers
{
    public class LoginController : Controller
    {
        private readonly MvcFamilyMeetingsContext _context;

        public LoginController(MvcFamilyMeetingsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            App_Code.Global.LoginMember = null;
            TempData["Show_Member"] = "N";
            
            // در صفحه لاگین منوی بالای صفحه نمایش داده نشود
            ViewData["NavBar"] = "HIDE";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string sLoginName, string sPassword)
        {
            if (string.IsNullOrWhiteSpace(sLoginName) && string.IsNullOrWhiteSpace(sPassword))
                return View();

            ViewBag.Message = "";

            if (_context.Member.AsNoTracking()
                .Where(d => d.LoginName.ToLower().Equals(sLoginName))
                .Where(h => h.Login_Password.Equals(sPassword)).Count() == 0)
            {
                //return NotFound();
                ViewBag.Message = "نام کاربری یا رمز ورود صحیح نمی باشد.";
                return View();
            }


            App_Code.Global.LoginMember = _context.Member
                .Where(d => d.LoginName.ToLower().Equals(sLoginName))
                .First(h => h.Login_Password.Equals(sPassword));//.ToListAsync();

            // برای ادمین و کاربران ارشد، جدول اعضا را نشان دهد
            if (App_Code.Global.LoginMember.Level < 100)
                TempData["Show_Member"] = "Y";

            //App_Code.Global.LoginMember_Id = member.Id;
            //App_Code.Global.LoginMember_Level = member.Level;

            //ViewBag.Message = "Succeeded";
            return RedirectToAction("Index", "FamliyData");

        }


        // GET: Members/Create
        public IActionResult Check(int? id)
        {
            string sLoginName = ViewData["LoginName"].ToString();
            string sPassword = ViewData["Password"].ToString();

            if (_context.Member.AsNoTracking().Where(d => d.LoginName.ToLower().Equals(sLoginName))
                .Where(h => h.Login_Password.Equals(sPassword)).Count() == 0)
            {
                return NotFound();
            }

            var member = _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }



        private bool MemberExists(object id)
        {
            throw new NotImplementedException();
        }
    }
}