using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeFixedMatches.Models;
using FreeFixedMatches.ViewModel;

namespace FreeFixedMatches.Controllers
{
    public class FreeTipsController : Controller
    {

        private FreeFixedDb _context;

        public FreeTipsController()
        {
            _context = new FreeFixedDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: FreeTips
        public ViewResult Index()
        {
            var freeTips = _context.FreeTips.OrderByDescending(f => f.Id).ToList();


            var freeTipsViewResult = new ViewModel.ViewModelData
            {
                FreeTips = freeTips
            };

            return View(freeTipsViewResult);
        }
    }
}