using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class FinderOrganSearchResultMV
    {
        public int OrganDonorID { get; set; }
        public int UserID { get; set; }
        public string OrganDonorName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public int UserTypeID { get; set; }
        public int OrganTypeID { get; set; }
        public string OrganName { get; set; }
    }
}