using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class OrganDonorMV
    {
        public int OrganDonorID { get; set; }
        public string OrganDonorName { get; set; }
        public int OrganTypeID { get; set; }
        public string OrganName { get; set; }
        public string LastDonatedOrgan { get; set; }
        public string NID { get; set; }
        public string Location { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int GenderID { get; set; }
        public string HealthIssues { get; set; }
       
        public string Gender { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
    }
}