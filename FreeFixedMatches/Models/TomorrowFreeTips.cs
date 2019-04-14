using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeFixedMatches.Models
{
    public class TomorrowFreeTips
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public string Match { get; set; }

        public string Tip { get; set; }

        public double Odd { get; set; }

        public string Time { get; set; }
    }
}