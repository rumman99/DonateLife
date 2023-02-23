using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class RegistrationMV
    {
        public RegistrationMV()
        {
            Seeker = new SeekerMV();
            Hospital = new HospitalMV();
            BloodBank = new BloodBankMV();
            BloodDonor = new BloodDonorMV();
            OrganBank = new OrganBankMV();
            OrganDonor = new OrganDonorMV();
            User = new UserMV();
        }
        public int UserTypeID { get; set; }
        
        public string ContactNo { get; set; }
        public int CityID { get; set; }
        public int BloodGroupID { get; set; }
        public int GenderID { get; set; }
        public int OrganTypeID { get;  set; }
        public SeekerMV Seeker { get; set; }
        public HospitalMV Hospital { get; set; }
        public BloodBankMV BloodBank { get; set; }
       public  BloodDonorMV BloodDonor { get; set; }
        public OrganBankMV OrganBank { get; set; }
        public OrganDonorMV OrganDonor{ get; set; }
        public UserMV User { get; set; }
    }
}