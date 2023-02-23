using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class FinderOrganMV
    {
        public FinderOrganMV()
            {
            SearchResult = new List<FinderOrganSearchResultMV>();
            }

        public int OrganTypeID { get; set; }
        public int CityID { get; set; }

        public List<FinderOrganSearchResultMV> SearchResult { get; set; }
    }
}