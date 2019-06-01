using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFamilyMeetings.Models;

namespace MvcFamilyMeetings.Controllers
{
    public class SavingsController : Controller
    {
        private readonly MvcFamilyMeetingsContext _context;

        public SavingsController(MvcFamilyMeetingsContext context)
        {
            _context = context;
        }

        // GET: Savings
        public async Task<IActionResult> Index()
        {
            long iCash = 0;
            ViewData["Data"] = "-";

            if (App_Code.Global.LoginMember == null)
                return RedirectToAction("Index", "Login");

            ViewData["Member_Name"] = App_Code.Global.LoginMember.Id + " - "
                + App_Code.Global.LoginMember.FirstName + " " + App_Code.Global.LoginMember.LastName;

            // اعضا
            if (App_Code.Global.LoginMember.Level >= 100)
            {
                // دکمه های جزئیات/ویرایش/حذف و همچنین فیلدهای جستجو داده نمایش نمی شوند
                ViewData["DED"] = "HIDE";
                iCash = (_context.Saving.Where(d => d.Member_Id == App_Code.Global.LoginMember.Id).Sum(d => d.Payment));
                ViewData["Cash"] = iCash;
                return View(await _context.Saving.Where(d => d.Member_Id == App_Code.Global.LoginMember.Id).ToListAsync());
            }
            // ادمین و کاربران ارشد
            else
            {
                // نمایش گزینه اعضاء در قسمت منوهای بالای صفحه
                TempData["Show_Member"] = "Y";
                // دکمه های جزئیات/ویرایش/حذف و همچنین فیلدهای جستجو داده نمایش می شوند
                ViewData["DED"] = "SHOW";
                iCash = (_context.Saving.Sum(d => d.Payment));
                ViewData["Cash"] = iCash;
                return View(await _context.Saving.OrderBy(d => d.Member_Id).ToListAsync());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string member_id, string FName, string LName)
        {
            // نمایش گزینه اعضاء در قسمت منوهای بالای صفحه
            if (App_Code.Global.LoginMember.Level<100)
                TempData["Show_Member"] = "Y";

            ViewData["DED"] = "SHOW";
            long iCash = 0;
            var lstMembers = _context.Member.ToList();

            // جستجوی شناسه کاربری
            if (!string.IsNullOrWhiteSpace(member_id))
            {
                int iMember_id = int.Parse(member_id);
                lstMembers = lstMembers.Where(d => d.Id == iMember_id).ToList();
            }
            // جستجوی نام
            if (!string.IsNullOrWhiteSpace(FName))
                lstMembers = lstMembers.Where(d => d.FirstName.Contains(FName)).ToList();
            // جستجوی نام خانوادگی
            if (!string.IsNullOrWhiteSpace(LName))
                lstMembers = lstMembers.Where(d => d.LastName.Contains(LName)).ToList();

            ViewData["Data"] = lstMembers.Count().ToString();

            if (lstMembers.Count() == 0)
            {
                ViewData["Member_Name"] = "عضوی با مشخصات وارد شده ، یافت نشد";
                ViewData["Cash"] = "0";
                return View(await _context.Saving.Where(d => d.Member_Id == -1).ToListAsync());
            }
            else
            {
                Member member = lstMembers.First();
                ViewData["Member_Name"] = member.Id + " - " + member.FirstName
                    + " " + member.LastName;
                iCash = (_context.Saving.Where(d => d.Member_Id == member.Id).Sum(d => d.Payment));
                ViewData["Cash"] = iCash;
                return View(await _context.Saving.Where(d => d.Member_Id == member.Id).ToListAsync());
            }

            //ViewData["DED"] = "HIDE";
            //// دکمه های جزئیات/ویرایش/حذف نمایش داده شوند؟
            //if (App_Code.Global.LoginMember.Level < 100)
            //    ViewData["DED"] = "SHOW";

        }


    

        // GET: Savings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saving = await _context.Saving
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saving == null)
            {
                return NotFound();
            }

            return View(saving);
        }

        // GET: Savings/Create
        public IActionResult Create()
        {
            ViewData["Error_Msg"] = "OK";
            return View();
        }

        // POST: Savings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Member_Id,Date,Payment,S_C1,S_C2,S_I1,S_I2")] Saving saving)
        {
            ViewData["Error_Msg"] = "OK";

            if (_context.Member.Where(d => d.Id == saving.Member_Id).Count() < 1)
            {
                ViewData["Error_Msg"] = "<script>alert('کاربری با شناسه مورد نظر وجود ندارد');</script>";
                //"کاربری با شناسه مورد نظر وجود ندارد";
                return View();
            }

            // اگر مبلغ پرداختی ، کمتر از صفر باشد
            if (saving.Payment <= 0)
            {
                ViewData["Error_Msg"] = "<script>alert('مبلغ پرداختی صحیح نمی باشد');</script>";
                return View();
            }



            Member member = _context.Member.Find(saving.Member_Id);
            if (ModelState.IsValid)
            {
                _context.Add(saving);
                await _context.SaveChangesAsync();
                // ذخیره موجودی در جدول اعضاء برای کاربر مورد نظر
                member.Saving_Cash = _context.Saving.Where(b=>b.Member_Id==member.Id).Sum(d => d.Payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saving);
        }

        // GET: Savings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saving = await _context.Saving.FindAsync(id);
            if (saving == null)
            {
                return NotFound();
            }
            return View(saving);
        }

        // POST: Savings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Member_Id,Date,Payment,S_C1,S_C2,S_I1,S_I2")] Saving saving)
        {
            if (id != saving.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavingExists(saving.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(saving);
        }

        // GET: Savings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saving = await _context.Saving
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saving == null)
            {
                return NotFound();
            }

            return View(saving);
        }

        // POST: Savings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saving = await _context.Saving.FindAsync(id);
            _context.Saving.Remove(saving);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavingExists(int id)
        {
            return _context.Saving.Any(e => e.Id == id);
        }
    }
}
