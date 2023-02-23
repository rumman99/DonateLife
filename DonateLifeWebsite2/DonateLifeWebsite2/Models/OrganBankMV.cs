﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class OrganBankMV
    {
        public int OrganBankID { get; set; }
        public string OrganBankName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}