using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FreeFixedMatches.Models
{
    public class VipTicket
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Image alt tag:")]
        public string Title { get; set; }
        [Required]
        public string Alt { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string Date { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [NotMapped] public string Exception { get; set; }
    }
}