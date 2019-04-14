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

            private string ChangeDateTicket(string date)
            {
                if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
                {
                    return date = GetDate();
                }
                return date;
            }

        public ActionResult Index()
            {
                var dateTicket = "06/04/2019";
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
                    DateNewVipTicket = ChangeDateTicket(dateTicket)
                };

                return View(viewResult);
            }

        }
}