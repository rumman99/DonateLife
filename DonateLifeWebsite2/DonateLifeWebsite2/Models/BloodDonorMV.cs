using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class BloodDonorMV
    {
        public int BloodDonorID { get; set; }
        public string FullName { get; set; }
        public int BloodGroupID { get; set; }
        public string BloodGroup { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> LastDonationDate { get; set; }
        public string ContactNo { get; set; }
        public string NID { get; set; }
        public string Location { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public string HealthIssues { get; set; }
        public int GenderID { get; set; }
        public string Gender { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}