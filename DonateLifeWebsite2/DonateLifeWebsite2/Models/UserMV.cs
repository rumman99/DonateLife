using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class UserMV
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
      
        public int AccountStatusID { get; set; }
        public string Accountstatus { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public string Description { get; set; }
    }
}