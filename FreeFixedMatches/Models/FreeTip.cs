using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreeFixedMatches.Models
{
    public class FreeTip
    {
        public int Id { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        [StringLength(70)]
        public string Match { get; set; }
        [Required]
        [StringLength(10)]
        public string Tip { get; set; }
        [Required]
        public double Odd { get; set; }
        [Required]
        [StringLength(10)]
        public string Result { get; set; }
        [Required]
        [StringLength(10)]
        public string WinLose { get; set; }
    }
}