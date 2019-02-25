using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeFixedMatches.Models;

namespace FreeFixedMatches.Controllers
{
    public class MonthlyController : Controller
    {

        private FreeFixedDb _context;

        public MonthlyController()
        {
            _context = new FreeFixedDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Monthly
        public ViewResult Index()
        {

            var monthlyTips = _context.MonthlySubscrations.ToList();


            var monthlyTipsViewModel = new ViewModel.ViewModelData
            {
                MonthlySubscrations = monthlyTips
            };


            return View(monthlyTipsViewModel);
        }
    }
}