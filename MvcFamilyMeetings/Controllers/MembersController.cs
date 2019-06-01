using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFamilyMeetings.Models;

namespace MvcFamilyMeetings.Controllers
{
    public class MembersController : Controller
    {
        private readonly MvcFamilyMeetingsContext _context;

        public MembersController(MvcFamilyMeetingsContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    //return View("Result");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        // GET: Members
        public async Task<IActionResult> Index()
        {
            //int? id = int.Parse(App_Code.Global.LoginMember_Id);
            if (App_Code.Global.LoginMember.Level < 100)
            {
               TempData["Show_Member"] = "Y";
                ViewData["member_Name"] = App_Code.Global.LoginMember.FirstName;// App_Code.Global.ImportantData;
                return View(await _context.Member.OrderBy(d=>d.Meeting_Turn).ToListAsync());
            }

            //App_Code.Global.LoginMember_Id = -1;
            return RedirectToAction("Index", "FamliyData");
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Numbers,Address_Home,Mobile_No,Phone_Home,Address_Work,Phone_Work,E_Mail,LoginName,Login_Password,Login_Password_Hint,Date_Enter_Meetings,Date_Exit_Meetings,Level,Comment,Saving_Cash,Last_Loan,Last_Loan_Date,M_C1,M_C2,M_C3,M_C4,M_I1,M_I2,M_I3,M_I4,Meeting_Turn")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Numbers,Address_Home,Mobile_No,Phone_Home,Address_Work,Phone_Work,E_Mail,LoginName,Login_Password,Login_Password_Hint,Date_Enter_Meetings,Date_Exit_Meetings,Level,Comment,Saving_Cash,Last_Loan,Last_Loan_Date,M_C1,M_C2,M_C3,M_C4,M_I1,M_I2,M_I3,M_I4,Meeting_Turn")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);

                    // تغییر نام و نام خانوادگی در جدول جلسه ها
                    if (_context.Meeting.Where(d => d.Member_Id == member.Id).Count() > 0)
                    {
                        string sFL = member.FirstName + " " + member.LastName;
                        // اگر شناسه کاربری با این شناسه پیدا شد اما نام آن تغییر کرده بود
                        if (_context.Meeting.Where(d => d.Member_Id == member.Id)
                            .Where(d => d.Member_Name.Equals(sFL)).Count() == 0)
                        {
                            foreach (Meeting mt in _context.Meeting.Where(d => d.Member_Id == member.Id).ToList())
                            {
                                mt.Member_Name = sFL;
                                _context.Update(mt);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.FindAsync(id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }
    }
}
