using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeFixedMatches.Models;

namespace FreeFixedMatches.ViewModel
{
    public class ViewModelData
    {
        public IEnumerable<FreeTip> FreeTips { get; set; }
        public IEnumerable<MonthlySubscration> MonthlySubscrations { get; set; }

        public IEnumerable<VipTicket> VipTickets { get; set; }
    }
}