﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonateLifeWebsite2.Models
{
    public class UserTypeMV
    {
        public int UserTypeID { get; set; }
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "User Type")]
        public string UserType { get; set; }
    }
}