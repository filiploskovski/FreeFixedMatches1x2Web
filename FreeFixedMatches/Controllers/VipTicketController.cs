using FreeFixedMatches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeFixedMatches.ViewModel;

namespace FreeFixedMatches.Controllers
{
    public class VipTicketController : Controller
    {
        private FreeFixedDb _context;

        public VipTicketController()
        {
            _context = new FreeFixedDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: VipTicket
        public ViewResult Index()
        {

            var vipTickets = _context.VipTickets.OrderByDescending(v => v.Id).ToList();

            var viewResult = new ViewModelData
            {
                VipTickets = vipTickets
            };

            return View(viewResult);
        }
    }
}