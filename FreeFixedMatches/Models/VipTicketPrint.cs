using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeFixedMatches.Models
{
    public class VipTicketPrint
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public double? TotalOdd { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Tip { get; set; }
        public double Odd { get; set; }
        public int? Code { get; set; }
        public int? FlagSite { get; set; }
        public double PaymentIn { get; set; }
        public double PaymentOut { get; set; }
        public string ControlNumber { get; set; }
        public string Worker { get; set; }
        public string TimeDate { get; set; }
    }
}