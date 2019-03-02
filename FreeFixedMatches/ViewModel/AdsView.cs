using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeFixedMatches.Models;

namespace FreeFixedMatches.ViewModel
{
    public class AdsView
    {
        public List<Ads> TopAdsList { get; set; }
        public Ads TopAds { get; set; }
    }
}