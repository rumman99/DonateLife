using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class AddTableMV
    {
        public AddTableMV()
        {
            Seeker = new SeekerMV();
            Hospital = new HospitalMV();
            BloodBank = new BloodBankMV();
            BloodDonor = new BloodDonorMV();
            OrganBank = new OrganBankMV();
            OrganDonor = new OrganDonorMV();
            User = new UserMV();
        }
        public SeekerMV Seeker { get; set; }
        public HospitalMV Hospital { get; set; }
        public BloodBankMV BloodBank { get; set; }
        public BloodDonorMV BloodDonor { get; set; }
        public OrganBankMV OrganBank { get; set; }
        public OrganDonorMV OrganDonor { get; set; }
        public UserMV User { get; set; }
    }
}