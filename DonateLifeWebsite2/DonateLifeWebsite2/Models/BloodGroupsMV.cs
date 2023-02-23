using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class BloodGroupsMV
    {
        public int BloodGroupID { get; set; }
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Blood Group Type")]
        public string BloodGroup { get; set; }
    }
}