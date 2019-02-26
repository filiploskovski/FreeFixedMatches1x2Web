using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreeFixedMatches.Models;
using FreeFixedMatches.ViewModel;
using Newtonsoft.Json;

namespace FreeFixedMatches.Controllers
{
    public class AdminController : Controller
    {
        private FreeFixedDb _context;

        public AdminController()
        {
            _context = new FreeFixedDb();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        private void ChangeValueToList(MonthlyOffersJson offersJson,string name,string text,int price)
        {
            offersJson.NameOffer = name;
            offersJson.TextOffer = text;
            offersJson.PriceOffer = price;
        }

        public ViewResult AddFreeTip()
        {
            var freeTips = _context.FreeTips.OrderByDescending(d => d.Date).ToList();
            var newViewModelFreeTips = new FreeTipViewModel
            {
                freeTips = freeTips
            };
            return View(newViewModelFreeTips);
        }

        [HttpPost]
        public ActionResult AddToFreeTip(FreeTipViewModel freeTipsView)
        {
            var result = Array.ConvertAll(freeTipsView.freeTip.Result.Split(':'), int.Parse).ToList();
            if (result[0] > result[1])
                freeTipsView.freeTip.Tip = Convert.ToString('1');
            else if (result[0] < result[1])
                freeTipsView.freeTip.Tip = Convert.ToString('2');
            else
                freeTipsView.freeTip.Tip = Convert.ToString('X');
            _context.FreeTips.Add(freeTipsView.freeTip);
            _context.SaveChanges();
            var freeTips = _context.FreeTips.OrderByDescending(d => d.Date).ToList();
            var newViewModelFreeTips = new FreeTipViewModel
            {
                freeTips = freeTips
            };
            return View("AddFreeTip", newViewModelFreeTips);
        }

        public ActionResult DeleteMatch(int id)
        {
            var freeTip = _context.FreeTips.FirstOrDefault(x => x.Id == id);
            _context.FreeTips.Remove(freeTip);
            _context.SaveChanges();
            var freeTips = _context.FreeTips.OrderByDescending(d => d.Date).ToList();
            var newViewModelFreeTips = new FreeTipViewModel
            {
                freeTips = freeTips
            };
            return View("AddFreeTip", newViewModelFreeTips);
        }

        public ActionResult AddMonthly()
        {
            var monthlyTable = _context.MonthlySubscrations.OrderByDescending(d => d.Date).ToList();
            var newViewModel = new MonthlyAdminViewModel
            {
                monthlySubscrations = monthlyTable
            };
            return View(newViewModel);
        }

        [HttpPost]
        public ActionResult AddMonthlyToDb(MonthlyAdminViewModel monthlyAdminView)
        {
            var result = Array.ConvertAll(monthlyAdminView.monthlySubscration.Result.Split(':'), int.Parse).ToList();
            if (result[0] > result[1])
                monthlyAdminView.monthlySubscration.Tip = Convert.ToString('1');
            else if (result[0] < result[1])
                monthlyAdminView.monthlySubscration.Tip = Convert.ToString('2');
            else
                monthlyAdminView.monthlySubscration.Tip = Convert.ToString('X');
            _context.MonthlySubscrations.Add(monthlyAdminView.monthlySubscration);
            _context.SaveChanges();
            var monthlyTable = _context.MonthlySubscrations.OrderByDescending(d => d.Date).ToList();
            var newViewModel = new MonthlyAdminViewModel
            {
                monthlySubscrations = monthlyTable
            };
            return View("AddMonthly",newViewModel);
        }

        public ActionResult DeleteMonthly(int id)
        {
            var monthlyTip = _context.MonthlySubscrations.FirstOrDefault(x => x.Id == id);
            _context.MonthlySubscrations.Remove(monthlyTip);
            _context.SaveChanges();
            var monthlyViewModel = _context.MonthlySubscrations.OrderByDescending(d => d.Date).ToList();
            var newViewModelMonthly = new MonthlyAdminViewModel
            {
                monthlySubscrations = monthlyViewModel
            };
            return View("AddMonthly",newViewModelMonthly);
        }

        public ViewResult AddVipTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVipToDb(VipTicket vipTicket)
        {
            string extention = Path.GetExtension(vipTicket.ImageFile.FileName);
            string fileName = "Tiket_" + DateTime.Now.ToString("d") + extention;
            vipTicket.ImagePath = "/Assets/VipTickets/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Assets/VipTickets/"), fileName);
            vipTicket.ImageFile.SaveAs(fileName);
            _context.VipTickets.Add(vipTicket);
            _context.SaveChanges();
            return View("AddVipTicket");
        }

        public ViewResult ChangeMonthlyOffers()
        {
            return View();
        }
        public ActionResult AddToJson(MonthlyOffersJson monthlyJson)
        {
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"));
            var result = JsonConvert.DeserializeObject<List<MonthlyOffersJson>>(fileContents);
            result.Add(new MonthlyOffersJson()
            {
                Id=monthlyJson.Id,
                NameOffer = monthlyJson.NameOffer,
                PriceOffer = monthlyJson.PriceOffer,
                TextOffer = monthlyJson.TextOffer
            });
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"),resultForSave);
            return View("ChangeMonthlyOffers");
        }

        public ActionResult UpdateMonthlyOffer(MonthlyOffersJson monthlyJson)
        {
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"));
            var result = JsonConvert.DeserializeObject<List<MonthlyOffersJson>>(fileContents);
            foreach (var rToChange in result)
            {
                if (rToChange.Id == monthlyJson.Id)
                {
                    ChangeValueToList(rToChange, monthlyJson.NameOffer, monthlyJson.TextOffer, monthlyJson.Id);
                } 
            }
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"), resultForSave);
            return View("ChangeMonthlyOffers");
        }

        public ActionResult DeleteMonthlyOffer(MonthlyOffersJson monthlyJson)
        {
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"));
            var result = JsonConvert.DeserializeObject<List<MonthlyOffersJson>>(fileContents);
            var objectToDelete = result.SingleOrDefault(x => x.Id == monthlyJson.Id);
            if (objectToDelete != null)
                result.Remove(objectToDelete);
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"), resultForSave);
            return View("ChangeMonthlyOffers");
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


       
    }
}