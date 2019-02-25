using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeFixedMatches.Models;

namespace FreeFixedMatches.ViewModel
{
    public class VipAdminViewModel
    {
        public VipTicket VipTicketVeiw { get; set; }
        public List<VipTicket> VipTickets { get; set; }
    }
}