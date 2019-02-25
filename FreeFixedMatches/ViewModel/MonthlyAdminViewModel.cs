using FreeFixedMatches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeFixedMatches.ViewModel
{
    public class MonthlyAdminViewModel
    {
        public MonthlySubscration monthlySubscration { get; set; }
        public List<MonthlySubscration>monthlySubscrations { get; set; }
    }
}