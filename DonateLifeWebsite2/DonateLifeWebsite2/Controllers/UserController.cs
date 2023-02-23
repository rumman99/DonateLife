using DatabaseLayer;
using DonateLifeWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonateLifeWebsite2.Controllers
{
    public class UserController : Controller
    {
        DonateLifeWebsiteSDEntities DB = new DonateLifeWebsiteSDEntities();
        // GET: User
        public ActionResult UserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
    var user = DB.UserTables.Find(id);
            return View(user);
        }

        public ActionResult EditUserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
           
            var userprofile = new RegistrationMV();
            var user = DB.UserTables.Find(id);

            userprofile.UserTypeID = user.UserTypeID;


            userprofile.User.UserID = user.UserID;
            userprofile.User.UserName = user.UserName;
            userprofile.User.Password = user.Password;
            userprofile.User.EmailAddress = user.EmailAddress;
            userprofile.User.AccountStatusID = user.AccountStatusID;

            userprofile.User.UserTypeID = user.UserTypeID;

            userprofile.User.Description = user.Description;
            if (user.SeekerTables.Count>0)
            {
                var seeker = user.SeekerTables.FirstOrDefault();
                userprofile.Seeker.SeekerID = seeker.SeekerID;
                userprofile.Seeker.FullName = seeker.FullName;

                userprofile.Seeker.Age = seeker.Age;
                userprofile.Seeker.CityID = seeker.CityID;
               
                userprofile.Seeker.BloodGroupID = seeker.BloodGroupID;
              
                userprofile.Seeker.ContactNo = seeker.ContactNo;
                userprofile.Seeker.NID = seeker.NID;
                userprofile.Seeker.GenderID = seeker.GenderID;
               

                userprofile.Seeker.RegistrationDate = seeker.RegistrationDate;
                userprofile.Seeker.OrganTypeID = seeker.OrganTypeID;
               
                userprofile.Seeker.Address = seeker.Address;
                userprofile.Seeker.UserID = seeker.UserID;



                userprofile.ContactNo = seeker.ContactNo;
                userprofile.CityID = seeker.CityID;
              
                userprofile.GenderID = seeker.GenderID;
            }
            else if (user.HospitalTables.Count > 0)
            {
                var hospital = user.HospitalTables.FirstOrDefault();
                userprofile.Hospital.HospitalID = hospital.HospitalID;
                userprofile.Hospital.FullName = hospital.FullName;

                userprofile.Hospital.Address = hospital.Address;
                userprofile.Hospital.PhoneNo = hospital.PhoneNo;

                userprofile.Hospital.Website = hospital.Website;

                userprofile.Hospital.Email = hospital.Email;
                userprofile.Hospital.Location = hospital.Location;
                userprofile.Hospital.CityID = hospital.CityID;
                userprofile.Hospital.UserID = hospital.UserID;
                userprofile.ContactNo = hospital.PhoneNo;
                userprofile.CityID = hospital.CityID;

            }
            else if (user.BloodBankTables.Count > 0)
            {
                var bloodbank = user.BloodBankTables.FirstOrDefault();
                userprofile.BloodBank.BloodBankID = bloodbank.BloodBankID;
                userprofile.BloodBank.BloodBankName = bloodbank.BloodBankName;

                userprofile.BloodBank.Address = bloodbank.Address;
                userprofile.BloodBank.PhoneNo = bloodbank.PhoneNo;
                userprofile.BloodBank.Location = bloodbank.Location;
                userprofile.BloodBank.Website = bloodbank.Website;

                userprofile.BloodBank.Email = bloodbank.Email;
               
                userprofile.BloodBank.CityID = bloodbank.CityID;
                userprofile.BloodBank.UserID = bloodbank.UserID;
                userprofile.ContactNo = bloodbank.PhoneNo;
                userprofile.CityID = bloodbank.CityID;

            }
            else if (user.BloodDonorTables.Count > 0)
            {
                var blooddonor = user.BloodDonorTables.FirstOrDefault();
                userprofile.BloodDonor.BloodDonorID = blooddonor.BloodDonorID;
                userprofile.BloodDonor.FullName = blooddonor.FullName;

                userprofile.BloodDonor.BloodGroupID = blooddonor.BloodGroupID;
                userprofile.BloodDonor.LastDonationDate = blooddonor.LastDonationDate;
                userprofile.BloodDonor.ContactNo = blooddonor.ContactNo;
                userprofile.BloodDonor.NID = blooddonor.NID;

                userprofile.BloodDonor.Location = blooddonor.Location;

                userprofile.BloodDonor.CityID = blooddonor.CityID;
                userprofile.BloodDonor.HealthIssues = blooddonor.HealthIssues;
                userprofile.BloodDonor.GenderID = blooddonor.GenderID;
                userprofile.BloodDonor.UserID = blooddonor.UserID;
                userprofile.ContactNo = blooddonor.ContactNo;
                userprofile.CityID = blooddonor.CityID;
                userprofile.BloodGroupID = blooddonor.BloodGroupID;
                userprofile.GenderID = blooddonor.GenderID;
            }
            else if (user.OrganDonorTables.Count > 0)
            {
                var organdonor = user.OrganDonorTables.FirstOrDefault();
                userprofile.OrganDonor.OrganDonorID = organdonor.OrganDonorID;
                userprofile.OrganDonor.OrganDonorName = organdonor.OrganDonorName;

                userprofile.OrganDonor.OrganTypeID = organdonor.OrganTypeID;
                userprofile.OrganDonor.LastDonatedOrgan = organdonor.LastDonatedOrgan;
                userprofile.OrganDonor.NID = organdonor.NID;
                userprofile.OrganDonor.Location = organdonor.Location;
                userprofile.OrganDonor.CityID = organdonor.CityID;
                userprofile.OrganDonor.GenderID = organdonor.GenderID;
                userprofile.OrganDonor.HealthIssues = organdonor.HealthIssues;
                userprofile.OrganDonor.ContactNo = organdonor.ContactNo;
                userprofile.OrganDonor.UserID = organdonor.UserID;



                userprofile.ContactNo = organdonor.ContactNo;
                userprofile.CityID = organdonor.CityID;
                userprofile.GenderID = organdonor.GenderID;
                userprofile.OrganTypeID = organdonor.OrganTypeID;






            }
            else if (user.OrganBankTables.Count > 0)
            {
                var organbank = user.OrganBankTables.FirstOrDefault();
                userprofile.OrganBank.OrganBankID = organbank.OrganBankID;
                userprofile.OrganBank.OrganBankName = organbank.OrganBankName;

                userprofile.OrganBank.Address = organbank.Address;
                userprofile.OrganBank.PhoneNo = organbank.PhoneNo;
                userprofile.OrganBank.Location = organbank.Location;
                userprofile.OrganBank.Website = organbank.Website;

                userprofile.OrganBank.Email = organbank.Email;

                userprofile.OrganBank.CityID = organbank.CityID;
                userprofile.OrganBank.UserID = organbank.UserID;
                userprofile.ContactNo = organbank.PhoneNo;
                userprofile.CityID = organbank.CityID;

            }


            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", userprofile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userprofile.GenderID);
            ViewBag.OrganTypeID = new SelectList(DB.OrganTypeTables.ToList(), "OrganTypeID", "OrganName", userprofile.OrganTypeID);


            return View(userprofile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(RegistrationMV userprofile)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if(ModelState.IsValid)
            {

               var checkuseremail = DB.UserTables.Where(u => u.EmailAddress == userprofile.User.EmailAddress && u.UserID != userprofile.User.UserID).FirstOrDefault();
                if(checkuseremail==null)
                {
                    try
                    {
                        var user = DB.UserTables.Find(userprofile.User.UserID);
                        user.EmailAddress = userprofile.User.EmailAddress;
                        DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        if (userprofile.BloodDonor.BloodDonorID > 0)
                        {
                            var blooddonor = DB.BloodDonorTables.Find(userprofile.BloodDonor.BloodDonorID);
                            blooddonor.FullName = userprofile.BloodDonor.FullName;
                            blooddonor.BloodGroupID = userprofile.BloodGroupID;
                            blooddonor.GenderID = userprofile.GenderID;
                            blooddonor.ContactNo = userprofile.BloodDonor.ContactNo;
                            blooddonor.NID = userprofile.BloodDonor.NID;
                            blooddonor.CityID = userprofile.CityID;
                            blooddonor.Location = userprofile.BloodDonor.Location;
                            DB.Entry(blooddonor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.OrganDonor.OrganDonorID > 0)
                        {
                            var organdonor = DB.OrganDonorTables.Find(userprofile.OrganDonor.OrganDonorID);
                            organdonor.OrganDonorName = userprofile.OrganDonor.OrganDonorName;

                            organdonor.GenderID = userprofile.GenderID;
                            organdonor.ContactNo = userprofile.OrganDonor.ContactNo;
                            organdonor.NID = userprofile.OrganDonor.NID;
                            organdonor.CityID = userprofile.CityID;
                            organdonor.Location = userprofile.OrganDonor.Location;
                            organdonor.OrganTypeID = userprofile.OrganTypeID;
                            DB.Entry(organdonor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.Seeker.SeekerID > 0)
                        {
                            var seeker = DB.SeekerTables.Find(userprofile.Seeker.SeekerID);
                            seeker.FullName = userprofile.Seeker.FullName;
                            seeker.Age = userprofile.Seeker.Age;
                            seeker.BloodGroupID = userprofile.BloodGroupID;
                            seeker.GenderID = userprofile.GenderID;
                            seeker.ContactNo = userprofile.Seeker.ContactNo;
                            seeker.NID = userprofile.Seeker.NID;
                            seeker.CityID = userprofile.CityID;
                            seeker.Address = userprofile.Seeker.Address;
                            seeker.OrganTypeID = userprofile.OrganTypeID;
                            DB.Entry(seeker).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.BloodBank.BloodBankID > 0)
                        {
                            var bloodbank = DB.BloodBankTables.Find(userprofile.BloodBank.BloodBankID);
                            bloodbank.BloodBankName = userprofile.BloodBank.BloodBankName;
                            bloodbank.Location = userprofile.BloodBank.Location;

                            bloodbank.PhoneNo = userprofile.BloodBank.PhoneNo;

                            bloodbank.CityID = userprofile.CityID;
                            bloodbank.Address = userprofile.BloodBank.Address;
                            bloodbank.Website = userprofile.BloodBank.Website;
                            bloodbank.Email = userprofile.BloodBank.Email;
                            DB.Entry(bloodbank).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.OrganBank.OrganBankID > 0)
                        {
                            var organbank = DB.OrganBankTables.Find(userprofile.OrganBank.OrganBankID);
                            organbank.OrganBankName = userprofile.OrganBank.OrganBankName;
                            organbank.Location = userprofile.OrganBank.Location;

                            organbank.PhoneNo = userprofile.OrganBank.PhoneNo;

                            organbank.CityID = userprofile.CityID;
                            organbank.Address = userprofile.OrganBank.Address;
                            organbank.Website = userprofile.OrganBank.Website;
                            organbank.Email = userprofile.OrganBank.Email;
                            DB.Entry(organbank).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.Hospital.HospitalID > 0)
                        {
                            var hospital = DB.HospitalTables.Find(userprofile.Hospital.HospitalID);
                            hospital.FullName = userprofile.Hospital.FullName;


                            hospital.PhoneNo = userprofile.Hospital.PhoneNo;

                            hospital.CityID = userprofile.CityID;
                            hospital.Address = userprofile.Hospital.Address;
                            hospital.Website = userprofile.Hospital.Website;
                            hospital.Email = userprofile.Hospital.Email;
                            DB.Entry(hospital).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        return RedirectToAction("UserProfile", "User", new { id = userprofile.User.UserID });
                    }
                    catch
                    {

                        ModelState.AddModelError(string.Empty, "Some data is Incorrect! Please Provide Details");
                    }










                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Email already exists");
                  
                }


              
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Some data is Incorrect! Please Provide Details");
            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", userprofile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userprofile.GenderID);
            ViewBag.OrganTypeID = new SelectList(DB.OrganTypeTables.ToList(), "OrganTypeID", "OrganName", userprofile.OrganTypeID);
            //var user = DB.UserTables.Find(id);
            return View(userprofile);
        }



    }

}