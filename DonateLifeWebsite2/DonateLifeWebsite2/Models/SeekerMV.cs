using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class SeekerMV
    {
        public int SeekerID { get; set; }
        public string FullName { get; set; }
       
        public int Age { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public Nullable<int> BloodGroupID { get; set; }
        public string BloodGroup { get; set; }
   
        [Display(Name = "Phone")]
        [RegularExpression(@"(^([+]{1}[8]{2}|0088)?(01){1}[3-9]{1}\d{8})$", ErrorMessage = "Invalid Number")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNo { get; set; }
        public string NID { get; set; }
        public int GenderID { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime RegistrationDate { get; set; }
        public Nullable<int> OrganTypeID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }

    }
}