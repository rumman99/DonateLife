using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
namespace DonateLifeWebsite2.Models
{
    public class CollectBloodMV
    {
        public CollectBloodMV()
            {
            DonorDetails=new ColllectBloodDonorDetailMV();
            }
        public int BloodBankStockDetailID { get; set; }
        public int BloodBankStockID { get; set; }
        public int BloodGroupID { get; set; }
        public double Quantity { get; set; }
        public int BloodDonorID { get; set; }
        public int GenderID { get; set; }
        public int CityID { get; set; }
        public System.DateTime BloodDonateDateTime { get; set; }
        public int CampaignID { get; set; }

        public ColllectBloodDonorDetailMV DonorDetails { get; set; }
    }
}