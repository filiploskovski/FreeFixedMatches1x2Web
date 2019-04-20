using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeFixedMatches.Models;
using FreeFixedMatches.ViewModel;

namespace FreeFixedMatches.Controllers
{
    public class HomeController : Controller
    {
        
            private FreeFixedDb _context;

            public HomeController()
            {
                _context = new FreeFixedDb();
            }

            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }

            private string GetDate()
            {
                return DateTime.Today.Date.ToString("D");
            }

            private string ChangeDateTicket()
            {
                DateTime today = DateTime.Today;
                // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
                int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
                DateTime nextSaturday = today.AddDays(daysUntilSaturday);
                return nextSaturday.ToString("dddd, dd MMMM yyyy");
            }

        public ActionResult Index()
        {
                var monthly = _context.MonthlySubscrations.OrderByDescending(m => m.Id).Take(18).ToList();
                var vipTickets = _context.VipTickets.OrderByDescending(v => v.Id).Take(1).ToList();
                var freeTips = _context.FreeTips.OrderByDescending(f => f.Id).Take(20).ToList();
                var ads = _context.Ads.ToList();
                var tomFreeTips = _context.TomorrowFreeTips.ToList();

                var viewResult = new ViewModelData
                {
                    FreeTips = freeTips,
                    VipTickets = vipTickets,
                    MonthlySubscrations = monthly,
                    Ads = ads,
                    TomorrowFreeTips = tomFreeTips,
                    TodayDate = GetDate(),
                    DateNewVipTicket = ChangeDateTicket()
                };

                return View(viewResult);
            }

        }
}