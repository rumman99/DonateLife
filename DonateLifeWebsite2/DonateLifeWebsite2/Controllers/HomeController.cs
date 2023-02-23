using DatabaseLayer;
using DonateLifeWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonateLifeWebsite2.Controllers
{
    public class HomeController : Controller
    {
        DonateLifeWebsiteSDEntities DB = new DonateLifeWebsiteSDEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllCampaigns()
        {

            var date = DateTime.Now.Date;
            var allcampaigns = DB.CampaignTables.Where(c => c.CampaignDate<=date).ToList();
            return View(allcampaigns);
        }
        public ActionResult MainHome()
        {
            var message = ViewData["Message"] == null ? "Welcome to Donate Life" : ViewData["Message"];
            ViewData["Message"] = message;
            var date = DateTime.Now.Date;
            var allcampaigns = DB.CampaignTables.Where(c => c.CampaignDate <= date).ToList();
            ViewBag.AllCampaigns = allcampaigns;
            var registration = new RegistrationMV();
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut=>ut.UserTypeID>3).ToList(), "UserTypeID", "UserType", "0");
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            return View(registration);
        }

        public ActionResult Login()
        {
            var usermv = new UserMV();
            return View(usermv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserMV usermv)
        {
            if (ModelState.IsValid)
            {
                var user = DB.UserTables.Where(u => u.Password == usermv.Password && u.UserName == usermv.UserName).FirstOrDefault();
                if (user != null)
                {
                    if (user.AccountStatusID == 2)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is under Review!");
                    }
                    else if (user.AccountStatusID == 4)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is Rejected! For more details, Contact Us.");
                    }
                    else if (user.AccountStatusID == 5)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is Suspended! For more details, Contact Us.");
                    }
                    else if (user.AccountStatusID == 3)
                    {
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.UserName;
                        Session["Password"] = user.Password;
                        Session["EmailAddress"] = user.EmailAddress;
                        Session["AccountStatusID"] = user.AccountStatusID;
                        Session["Accountstatus"] = user.AccountStatusTable.AccountStatus;
                        Session["UserTypeID"] = user.UserTypeID;
                        Session["UserType"] = user.UserTypeTable.UserType;
                        Session["Description"] = user.Description;

                        if (user.UserTypeID == 3)//Admin
                        {

                            return RedirectToAction("AllNewUserRequests","Accounts");
                        }
                        else if (user.UserTypeID == 4)//Blood Donor
                        {
                            var blooddonor = DB.BloodDonorTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (blooddonor != null)
                            {
                                Session["BloodDonorID"] = blooddonor.BloodDonorID;
                                Session["FullName"] = blooddonor.FullName;
                                Session["BloodGroupID"] = blooddonor.BloodDonorID;
                                Session["BloodGroup"] = blooddonor.BloodGroupsTable.BloodGroup;

                                Session["LastDonationDate"] = blooddonor.LastDonationDate;
                                Session["ContactNo"] = blooddonor.ContactNo;
                                Session["NID"] = blooddonor.NID;
                                Session["Location"] = blooddonor.Location;
                                Session["CityID"] = blooddonor.CityID;
                                Session["City"] = blooddonor.CityTable.City;
                                Session["HealthIssues"] = blooddonor.HealthIssues;
                                Session["GenderID"] = blooddonor.GenderID;
                                Session["Gender"] = blooddonor.GenderTable.Gender;
                                return RedirectToAction("DonorRequests", "Finder");

                            }
                            else
                            {
                                
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 6)//Blood Bank
                        {
                            var bloodbank = DB.BloodBankTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (bloodbank != null)
                            {
                                Session["BloodBankID"] = bloodbank.BloodBankID;
                                Session["BloodBankName"] = bloodbank.BloodBankName;
                                Session["Address"] = bloodbank.Address;
                                Session["PhoneNo"] = bloodbank.PhoneNo;

                                Session["Location"] = bloodbank.Location;
                                Session["CityID"] = bloodbank.CityID;
                                Session["City"] = bloodbank.CityTable.City;
                                Session["Website"] = bloodbank.Website;
                                Session["Email"] = bloodbank.Email;
                                return RedirectToAction("BloodBankStock", "BloodBank");

                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 7)//Organ Donor
                        {
                            var organdonor = DB.OrganDonorTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (organdonor != null)
                            {
                                Session["OrganDonorID"] = organdonor.OrganDonorID;
                                Session["OrganDonorName"] = organdonor.OrganDonorName;
                                Session["OrganTypeID"] = organdonor.OrganTypeID;
                                Session["OrganName"] = organdonor.OrganTypeTable.OrganName;

                                Session["LastDonatedOrgan"] = organdonor.LastDonatedOrgan;
                                Session["ContactNo"] = organdonor.ContactNo;
                                Session["NID"] = organdonor.NID;
                                Session["Location"] = organdonor.Location;
                                Session["CityID"] = organdonor.CityID;
                                Session["City"] = organdonor.CityTable.City;
                                Session["HealthIssues"] = organdonor.HealthIssues;
                                Session["GenderID"] = organdonor.GenderID;
                                Session["Gender"] = organdonor.GenderTable.Gender;
                                return RedirectToAction("OrganDonor", "Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 8)//Organ Bank
                        {
                            var organbank = DB.OrganBankTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (organbank != null)
                            {
                                Session["OrganBankID"] = organbank.OrganBankID;
                                Session["OrganBankName"] = organbank.OrganBankName;
                                Session["Address"] = organbank.Address;
                                Session["PhoneNo"] = organbank.PhoneNo;

                                Session["Location"] = organbank.Location;
                                Session["CityID"] = organbank.CityID;
                                Session["City"] = organbank.CityTable.City;
                                Session["Website"] = organbank.Website;
                                Session["Email"] = organbank.Email;
                                return RedirectToAction("OrganBank", "Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 5)//Hospital
                        {
                            var hospital = DB.HospitalTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (hospital != null)
                            {
                                Session["HospitalID"] = hospital.HospitalID;
                                Session["FullName"] = hospital.FullName;
                                Session["Address"] = hospital.Address;
                                Session["PhoneNo"] = hospital.PhoneNo;

                                Session["Location"] = hospital.Location;
                                Session["CityID"] = hospital.CityID;
                                Session["City"] = hospital.CityTable.City;
                                Session["Website"] = hospital.Website;
                                Session["Email"] = hospital.Email;
                                return RedirectToAction("ShowAllRequests", "Finder");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 9)//Seeker
                        {
                            var seeker = DB.SeekerTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (seeker != null)
                            {
                                Session["SeekerID"] = seeker.SeekerID;
                                Session["FullName"] = seeker.FullName;
                                Session["Age"] = seeker.Age;

                                Session["CityID"] = seeker.CityID;
                                Session["City"] = seeker.CityTable.City;
                                Session["BloodGroupID"] = seeker.BloodGroupID;
                                Session["BloodGroup"] = seeker.BloodGroupsTable.BloodGroup;
                                Session["ContactNo"] = seeker.ContactNo;
                                Session["NID"] = seeker.NID;
                                Session["GenderID"] = seeker.GenderID;
                                Session["Gender"] = seeker.GenderTable.Gender;
                                Session["RegistrationDate"] = seeker.RegistrationDate;
                                Session["OrganTypeID"] = seeker.OrganTypeID;
                                return RedirectToAction("ShowAllRequests", "Finder");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                        }


                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide Username and Password!");
            }
            ClearSession();
            return View(usermv);
        }
        private void ClearSession()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["AccountStatusID"] = string.Empty;
            Session["Accountstatus"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["Description"] = string.Empty;
        }
        public ActionResult Logout()
        {

            ClearSession();
            return RedirectToAction("MainHome");
        }

        public ActionResult AboutUs()
        {


            return View();
        }

        public ActionResult Add(AddTableMV addTableMV)
        {
            var OrganDonor = DB.OrganDonorTables.Count();
            var BloodDonor = DB.BloodDonorTables.Count();
            var BloodBank = DB.BloodBankTables.Count();
            var OrganBank = DB.OrganBankTables.Count();
            var Hospital = DB.HospitalTables.Count();
            var Seeker = DB.SeekerTables.Count();

            return View();
        }

    }
}