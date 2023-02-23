using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class FinderSearchResultMV
    {
        public int BloodDonorID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ContactNo{ get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public int UserTypeID { get; set; }
        public int BloodGroupID { get; set; }
        public string BloodGroup { get; set; }
    }
}