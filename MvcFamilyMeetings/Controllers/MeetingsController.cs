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
    public class MeetingsController : Controller
    {
        private readonly MvcFamilyMeetingsContext _context;

        public MeetingsController(MvcFamilyMeetingsContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            ViewData["DED"] = "HIDE";
            ViewData["Member_Name"] = App_Code.Global.LoginMember.Id + " - "
                + App_Code.Global.LoginMember.FirstName + " " + App_Code.Global.LoginMember.LastName;
            // دکمه های جزئیات/ویرایش/حذف نمایش داده شوند؟
            if (App_Code.Global.LoginMember.Level < 100)
            {
                TempData["Show_Member"] = "Y";
                ViewData["DED"] = "SHOW";
            }

            ViewData["Next_Meeting"] = "";
            if (_context.Member.Where(d => d.Meeting_Turn > 0).Count() > 0)
            {
                //  جلسه آینده - طبق تاریخچه جلسات برگزار شده
                if (_context.Meeting.Count() > 0)
                {
                    Meeting mt = _context.Meeting.ToList().Last();
                    int iTurn = _context.Member.Find(mt.Member_Id).Meeting_Turn;
                    Member member;
                    // جستجوی نوبت بعدی
                    if (_context.Member.Where(d => d.Meeting_Turn > iTurn).Count() > 0)
                    {
                        member = _context.Member.Where(d => d.Meeting_Turn > iTurn)
                            .OrderBy(j => j.Meeting_Turn).First();
                    }
                    else   // اگر آخرین نوبت رسیده باشد، نوبتها باید از اول شروع شوند
                    {
                        iTurn = _context.Member.Where(d => d.Meeting_Turn > 0).Min(j => j.Meeting_Turn);
                        member = _context.Member.First(d => d.Meeting_Turn == iTurn);
                    }

                    ViewData["Next_Meeting"] = "میزبان جلسه آینده : " + member.FirstName + " " + member.LastName;
                }
            }
            return View(await _context.Meeting.ToListAsync());
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create1()
        {
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("Id,Member_Id,Login_Date,Comment,Mt_C1,Mt_C2,Mt_I1,Mt_I2")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                Member member = _context.Member.Find(meeting.Member_Id);
                meeting.Member_Name = member.FirstName + " " + member.LastName;
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Member_Id,Login_Date,Comment,Mt_C1,Mt_C2,Mt_I1,Mt_I2")] Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.Id))
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
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _context.Meeting.FindAsync(id);
            _context.Meeting.Remove(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meeting.Any(e => e.Id == id);
        }
    }
}
