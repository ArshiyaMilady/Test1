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
    public class LoansController : Controller
    {
        private readonly MvcFamilyMeetingsContext _context;

        public LoansController(MvcFamilyMeetingsContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            // مبلغ وام گرفته شده
            ViewData["LastLoan_amount"] = "";
            // تاریخ دریافت وام گرفته شده
            ViewData["LastLoan_date"] = "";
            // تعداد کاربران یافت شده پس از انجام جستجو
            // باید در این تابع باشه وگرنه باخطا روبرو میشه
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
                // آیا در حال حاضر این عضو دارای وام است
                bool bHasLoan = true;
                // هنوز وامی برای عضو ثبت نشده است
                if (_context.Loan.Where(d=>d.Member_Id == App_Code.Global.LoginMember.Id).Count() == 0) bHasLoan = false;
                else
                {
                    // وام قبلی تصویه شده است
                    if (_context.Loan.Where(d => d.Member_Id == App_Code.Global.LoginMember.Id)
                        .ToList().Last().Remaining == 0) bHasLoan = false;
                }

                if (bHasLoan)
                {
                    ViewData["LastLoan_amount"] = "مبلغ آخرین وام دریافت شده  : " + App_Code.Global.LoginMember.Last_Loan;
                    ViewData["LastLoan_date"] = "تاریخ آخرین وام دریافت شده : " +  App_Code.Global.LoginMember.Last_Loan_Date;

                   return View(await _context.Loan.Where(d =>
                        d.Member_Id == App_Code.Global.LoginMember.Id)
                        .Where(j=>j.Loan_Date.Equals(App_Code.Global.LoginMember.Last_Loan_Date))
                        .ToListAsync());
                }
                else
                {
                    ViewData["LastLoan_amount"] = "در حال حاضر وامی برای شما ثبت نشده است";
                    return View(await _context.Loan.Where(d => d.Id == -1).ToListAsync());
                }
            }
            // ادمین و کاربران ارشد
            else
            {
                TempData["Show_Member"] = "Y";
                // دکمه های جزئیات/ویرایش/حذف و همچنین فیلدهای جستجو داده نمایش می شوند
                ViewData["DED"] = "SHOW";
                //iCash = (_context.Saving.Sum(d => d.Payment));
                //ViewData["Cash"] = iCash;
                return View(await _context.Loan.OrderBy(d => d.Member_Id).ToListAsync());
            }
            //return View(await _context.Loan.ToListAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string member_id, string FName, string LName)
        {
            ViewData["Member_Name"] = "";
            // مبلغ وام گرفته شده
            ViewData["LastLoan_amount"] = "";
            // تاریخ دریافت وام گرفته شده
            ViewData["LastLoan_date"] = "";
            // تعداد کاربران یافت شده پس از انجام جستجو
            // باید در این تابع باشه وگرنه باخطا روبرو میشه
            ViewData["Data"] = "-";

            ViewData["DED"] = "SHOW";
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

            // تعداد کاربران یافت شده پس از انجام جستجو
            ViewData["Data"] = lstMembers.Count().ToString();

            if (string.IsNullOrWhiteSpace(member_id) &&
                string.IsNullOrWhiteSpace(FName) &&
                string.IsNullOrWhiteSpace(LName))
            {
                return View(await _context.Loan.OrderBy(d=>d.Member_Id).ToListAsync());
            }

            if (lstMembers.Count() == 0)
            {
                ViewData["Member_Name"] = "عضوی با مشخصات وارد شده ، یافت نشد";
                return View(await _context.Loan.Where(d => d.Id == -1).ToListAsync());
            }
            else
            {
                Member member = lstMembers.First();
                ViewData["Member_Name"] = member.Id + " - " + member.FirstName
                    + " " + member.LastName;
                // آیا در حال حاضر این عضو دارای وام است
                bool bHasLoan = true;
                // هنوز وامی برای عضو ثبت نشده است
                if (_context.Loan.Where(d => d.Member_Id == member.Id).Count() == 0) bHasLoan = false;
                else
                {
                    // وام قبلی تصویه شده است
                    if (_context.Loan.Where(d => d.Member_Id == member.Id)
                        .ToList().Last().Remaining == 0) bHasLoan = false;
                }

                if (bHasLoan)
                {
                    ViewData["LastLoan_amount"] = "مبلغ آخرین وام دریافت شده  : " + member.Last_Loan;
                    ViewData["LastLoan_date"] = "تاریخ آخرین وام دریافت شده : " + member.Last_Loan_Date;

                    return View(await _context.Loan.Where(d =>d.Member_Id == member.Id)
                         .Where(j => j.Loan_Date.Equals(member.Last_Loan_Date))
                         .ToListAsync());
                }
                else
                {
                    ViewData["LastLoan_amount"] = "در حال حاضر وامی برای شما ثبت نشده است";
                    return View(await _context.Loan.Where(d => d.Id == -1).ToListAsync());
                }
                //return View(await _context.Loan.Where(d => d.Member_Id == member.Id).ToListAsync());
            }

            //ViewData["DED"] = "HIDE";
            //// دکمه های جزئیات/ویرایش/حذف نمایش داده شوند؟
            //if (App_Code.Global.LoginMember.Level < 100)
            //    ViewData["DED"] = "SHOW";

        }


        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["Error_Msg"] = "OK";
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Member_Id,Payment,Date,Remaining,Loan_Amount,Loan_Date,Comment,I_C1,I_C2,I_I1,I_I2")] Loan loan)
        {
            if (_context.Member.Where(d => d.Id == loan.Member_Id).Count() < 1)
            {
                ViewData["Error_Msg"] = "<script>alert('کاربری با شناسه مورد نظر وجود ندارد');</script>";
                //"کاربری با شناسه مورد نظر وجود ندارد";
                return View();
            }
            // عضو مورد نظر برای ثبت قسط
            Member member = _context.Member.Find(loan.Member_Id);

            ViewData["Error_Msg"] = "OK";

            // اگر مبلغ پرداختی ، کمتر از صفر باشد
            if (loan.Payment <= 0)
            {
                ViewData["Error_Msg"] = "<script>alert('مبلغ پرداختی صحیح نمی باشد');</script>";
                return View();
            }

            if (string.IsNullOrWhiteSpace(member.Last_Loan_Date))
            {
                ViewData["Error_Msg"] = "<script>alert('برای کاربر مورد نظر وامی ثبت نشده است.جهت ثبت وام به جدول اعضاء مراجعه نمایید');</script>";
                //"کاربری با شناسه مورد نظر وجود ندارد";
                return View();
            }

            try
            {
                loan.Loan_Date = member.Last_Loan_Date;
                loan.Loan_Amount = member.Last_Loan;
                // باقیمانده را محاسبه می کند
                // برای اولین پرداخت قسط
                if (_context.Loan.Where(b => b.Member_Id == member.Id)
                    .Where(d => d.Loan_Date.Equals(member.Last_Loan_Date)).Count() < 1)
                    loan.Remaining = loan.Loan_Amount - loan.Payment;
                // برای پرداختهای بعدی
                else
                {
                    // اگر مقدار باقیمانده برابر صفر باشد
                    if (_context.Loan.Where(b => b.Member_Id == member.Id)
                        .Where(d => d.Loan_Date.Equals(member.Last_Loan_Date)).Min(d => d.Remaining) <= 0)
                    {
                        ViewData["Error_Msg"] = "<script>alert('اقساط این وام به پایان رسیده است و امکان ثبت پرداخت دیگر برای آن وجود ندارد');</script>";
                        return View();
                    }

                    loan.Remaining = _context.Loan.Where(b => b.Member_Id == member.Id)
                        .Where(d => d.Loan_Date.Equals(member.Last_Loan_Date))
                        .Min(d => d.Remaining) - loan.Payment;
                }

                if (loan.Remaining < 0)
                {
                    ViewData["Error_Msg"] = "<script>alert('مبلغ پرداختی بیش از مبلغ باقیمانده است.لطفا تصحیح نمایید');</script>";
                    return View();
                }
            }
            catch
            {
                ViewData["Error_Msg"] = "<script>alert('خطای بحرانی . امکان ثبت این مورد وجود ندارد');</script>";
                return View();
            }

            if (ModelState.IsValid)
            {
                // ذخیره باقیمانده وام در جدول اعضا
                member.Last_Loan_Remainig = loan.Remaining;
                await _context.SaveChangesAsync();

                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Member_Id,Payment,Date,Remaining,Loan_Amount,Loan_Date,Comment,I_C1,I_C2,I_I1,I_I2")] Loan loan)
        {
            if (id != loan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.Id))
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
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.Id == id);
        }

        // تاریخچه وامهای گرفته شده برای هر عضو
        public async Task<IActionResult> Loan_History()
        {
            ViewData["Member_Name"] = App_Code.Global.LoginMember.Id + " - "
                + App_Code.Global.LoginMember.FirstName + " " + App_Code.Global.LoginMember.LastName;

            return View(await _context.Loan.Where(d=>d.Member_Id == App_Code.Global.LoginMember.Id)
                .GroupBy(d=>d.Loan_Date).SelectMany(g=>g.Take(1)).ToListAsync());

        }

    }
    }
