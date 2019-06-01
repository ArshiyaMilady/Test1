using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcFamilyMeetings.Models;

namespace MvcFamilyMeetings.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int? id)
        {
            if(App_Code.Global.LoginMember == null)
                return RedirectToAction("Index", "Login");
            
            ViewData["member_Name"] = App_Code.Global.LoginMember.FirstName;// App_Code.Global.ImportantData;
            return View();
        }

 

        public IActionResult About()
        {
            ViewData["Message"] = "این نرم افزار جهت مشاهده ، ثبت و پیگیری موارد مربوط به جلسه های خانوادگی ارائه می گردد." 
                + "شاید در ابتدا وجود چنین امکانی از نظر برخی دوستان لازم نباشد،"
                + " اما نمایش وضعیت جلسات، وامهای دریافتی ، اقساط پرداخت شده و واریزی های انجام شده "
                + " به صورت لحظه ای، پیشرفتی مطلوب در جهت اطلاع رسانی خواهد بود" ;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "در صورت نیاز به اطلاعات بیشتر یا ارائه پیشنهاد و انتقاد، لطفا با شماره 09133700052 تماس" 
                + " حاصل فرمایید";

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
    }
}
