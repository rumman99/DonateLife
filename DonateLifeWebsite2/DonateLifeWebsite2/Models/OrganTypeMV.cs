using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class OrganTypeMV
    {
        public int OrganTypeID { get; set; }
        [Required(ErrorMessage="Required")]
        [Display(Name="Organ Type")]
        public string OrganName { get; set; }
    }
}