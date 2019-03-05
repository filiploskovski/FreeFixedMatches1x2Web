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
        private void ChangeValueToListVip(VipTicketOffer offersJson, string name, string text, int price)
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

            //proveruva dali ima element so toj datum i ako nema totalOdd go stava da bide na novozadadeniot odd
            if (_context.MonthlySubscrations.Where(date => date.Date == monthlyAdminView.monthlySubscration.Date)
                    .ToList().Count == 0)
                monthlyAdminView.monthlySubscration.TotalOdd = monthlyAdminView.monthlySubscration.Odd;
            //ima elementi => gi zima site na toj den i proveruva dali e eden => ako e eden togas na noviot Total oDD mu go dodava stariot i do mnozi so noviot Odd
            else
            {
                var getAllOnDateQ = _context.MonthlySubscrations
                    .Where(date => date.Date == monthlyAdminView.monthlySubscration.Date).ToList();
                if (getAllOnDateQ.Count == 1)
                    monthlyAdminView.monthlySubscration.TotalOdd = getAllOnDateQ[0].TotalOdd * monthlyAdminView.monthlySubscration.Odd;
                else
                {  //znaci ima poise od eden element na toj datum i gi proga niz loop i gi dadava site 
                    foreach (var odd in getAllOnDateQ)
                    {
                         monthlyAdminView.monthlySubscration.TotalOdd = odd.TotalOdd * monthlyAdminView.monthlySubscration.Odd;
                    }
                }
            }
            //pravi update vo baza za na starite TotalOdd da go dodade noviot TotalOdd
            _context.MonthlySubscrations.Where(date => date.Date == monthlyAdminView.monthlySubscration.Date).ToList()
                .ForEach( totalOdd => totalOdd.TotalOdd = monthlyAdminView.monthlySubscration.TotalOdd);
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
        public ViewResult DeleteVipTicket()
        {
            var vipTickets = _context.VipTickets.ToList();
            var vipTicketsList = new VipAdminViewModel
            {
                VipTickets = vipTickets
            };
            return View(vipTicketsList);
        }
        public ActionResult DeleteVip(int id)
        {
            var ticketToDelete = _context.VipTickets.FirstOrDefault(x => x.Id == id);
            _context.VipTickets.Remove(ticketToDelete);
            _context.SaveChanges();

            var vipTickets = _context.VipTickets.ToList();
            var vipTicketsList = new VipAdminViewModel
            {
                VipTickets = vipTickets
            };

            return View("DeleteVipTicket", vipTicketsList);
        }


        //----------------------------------------RABOTA SO JSON ---------------------------------------------------------------------------------


        //-------------------------------------------------------- MONTHLY OFFERS ----------------------------------------------------------------
        public ViewResult ChangeMonthlyOffers()
        {
            return View();
        }
        
        public ActionResult AddMonthlyOffer(MonthlyOffersJson monthlyJson)
        {
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"));
            var result = JsonConvert.DeserializeObject<List<MonthlyOffersJson>>(fileContents);
            result.Add(new MonthlyOffersJson
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
                    ChangeValueToList(rToChange, monthlyJson.NameOffer, monthlyJson.TextOffer, monthlyJson.PriceOffer);
                } 
            }
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"), resultForSave);
            return View("ChangeMonthlyOffers");
        }

        public ActionResult DeleteMonthlyOffer(string id)
        {
            var idInt = Int32.Parse(id);
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"));
            var result = JsonConvert.DeserializeObject<List<MonthlyOffersJson>>(fileContents);
            var objectToDelete = result.SingleOrDefault(x => x.Id == idInt);
            var count = 0;
            if (objectToDelete != null)
                result.Remove(objectToDelete);
            foreach (var sortOffers in result)
            {
                sortOffers.Id = count;
                count++;
            }
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/MonthlyOffers.json"), resultForSave);
            return View("ChangeMonthlyOffers");
        }

        //END MONTHLY OFFER
        //----------------------------------------ADS------------------------------------------------------------------

        public ViewResult Ads()
        {
            return View();
        }

        public ActionResult AdsAdd(AdsView adsView)
        {
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/Ads.json"));
            var result = JsonConvert.DeserializeObject<List<Ads>>(fileContents);
            result.Add(new Ads
            {
                Id = adsView.TopAds.Id,
                Alt = adsView.TopAds.Alt,
                Title = adsView.TopAds.Title,
                ImgUrl = adsView.TopAds.ImgUrl,
                TopBottom = adsView.TopAds.TopBottom
            });
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/Ads.json"), resultForSave);
            return View("Ads");
        }

        public ActionResult DeleteAdd(string id)
        {
            var idInt = Int32.Parse(id);
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/Ads.json"));
            var result = JsonConvert.DeserializeObject<List<Ads>>(fileContents);
            var objectToDelete = result.SingleOrDefault(x => x.Id == idInt);
            var count = 0;
            if (objectToDelete != null)
                result.Remove(objectToDelete);
            foreach (var sortAdds in result)
            {
                sortAdds.Id = count;
                count++;
            }
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/Ads.json"), resultForSave);

            return View("Ads");

        }
        //----------------------------------------ADS END ---------------------------------------------------------------
        //Free tips update
        public ViewResult freeTips()
        {
            return View();
        }

        public ActionResult addFreeTips(NewFreeTips newTips)
        {
            var tipsUnChecked = newTips.freeTip.Split(Convert.ToChar(","));
            var resultForSave = JsonConvert.SerializeObject(tipsUnChecked);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/FreeTips.json"), resultForSave);
            return View("freeTips");
        }

        public ViewResult FreeTipsDate()
        {
            return View();
        }

        public ActionResult FreeTipsDateChangeActionResult(FreeTipsDateChange dateFreeChange)
        {
            var resultForSave = JsonConvert.SerializeObject(dateFreeChange);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/FreeTipDateChange.json"), resultForSave);
            return View("FreeTipsDate");
        }

        //---------------------------------FREE TIPS END -----------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //Vip ticket update offers

        public ViewResult vipTicketOffer()
        {
            return View();
        }

        public ActionResult VipTicketOfferAdd(VipTicketOffer vipOffer)
        {

            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/vipTicketOffers.json"));
            var result = JsonConvert.DeserializeObject<List<VipTicketOffer>>(fileContents);
            result.Add(new VipTicketOffer
            {
                Id = vipOffer.Id,
                NameOffer = vipOffer.NameOffer,
                PriceOffer = vipOffer.PriceOffer,
                TextOffer = vipOffer.TextOffer
            });
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/vipTicketOffers.json"), resultForSave);

            return View("VipTicketOffer");
        }

        public ActionResult UpdateVipTicketOffer(VipTicketOffer vipOffer)
        {
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/vipTicketOffers.json"));
            var result = JsonConvert.DeserializeObject<List<VipTicketOffer>>(fileContents);
            foreach (var rToChange in result)
            {
                if (rToChange.Id == vipOffer.Id)
                {
                    ChangeValueToListVip(rToChange, vipOffer.NameOffer, vipOffer.TextOffer, vipOffer.PriceOffer);
                }
            }
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/vipTicketOffers.json"), resultForSave);

            return View("VipTicketOffer");
        }

        public ActionResult DeleteVipTicketOffer(string id)
        {
            var idInt = Int32.Parse(id);
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/JsonFiles/vipTicketOffers.json"));
            var result = JsonConvert.DeserializeObject<List<VipTicketOffer>>(fileContents);
            var objectToDelete = result.SingleOrDefault(x => x.Id == idInt);
            var count = 0;
            if (objectToDelete != null)
                result.Remove(objectToDelete);
            foreach (var sortOffers in result)
            {
                sortOffers.Id = count;
                count++;
            }
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/vipTicketOffers.json"), resultForSave);

            return View("VipTicketOffer");
        }

        public ViewResult VipTicketDate()
        {
            return View();
        }

        public ActionResult ChangeVipTicketDate(VipTicketDate ticketDate)
        {
            var resultForSave = JsonConvert.SerializeObject(ticketDate);
            System.IO.File.WriteAllText(Server.MapPath(@"~/JsonFiles/VipTicketDate.json"), resultForSave);
            return View("VipTicketDate");
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


       
    }
}