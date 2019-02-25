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

            public ActionResult Index()
            {
                var monthly = _context.MonthlySubscrations.OrderByDescending(m => m.Id).Take(16).ToList();
                var vipTickets = _context.VipTickets.OrderByDescending(v => v.Id).Take(1).ToList();
                var freeTips = _context.FreeTips.OrderByDescending(f => f.Id).Take(16).ToList();

                var viewResult = new ViewModelData
                {
                    FreeTips = freeTips,
                    VipTickets = vipTickets,
                    MonthlySubscrations = monthly
                };

                return View(viewResult);
            }

        }
}