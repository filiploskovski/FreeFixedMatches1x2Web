using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeFixedMatches.Models
{
    public class MonthlySubscration
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string TotalOdd { get; set; }
        public string Match { get; set; }
        public string Tip { get; set; }
        public double Odd { get; set; }
        public string Result { get; set; }
        public string WinLose { get; set; }
    }
}