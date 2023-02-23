using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonateLifeWebsite2.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult BloodDonor()
        {
            return View();
        }

        public ActionResult BloodBank()
        {
            return View();
        }
        public ActionResult OrganDonor()
        {
            return View();
        }
        public ActionResult OrganBank()
        {
            return View();
        }
        public ActionResult Seeker()
        {
            return View();
        }
        public ActionResult Hospital()
        {
            return View();
        }
    }
}