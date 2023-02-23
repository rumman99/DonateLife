using DatabaseLayer;
using DonateLifeWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonateLifeWebsite2.Controllers
{
    public class RegistrationController : Controller
    {
        DonateLifeWebsiteSDEntities DB = new DonateLifeWebsiteSDEntities();
        static RegistrationMV registrationmv;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectUser(RegistrationMV registrationMV)
        {
            registrationmv = registrationMV;
            if(registrationMV.UserTypeID==9)
            {
                return RedirectToAction("SeekerUser" );
            }
            else if (registrationMV.UserTypeID == 4)
            {
                return RedirectToAction("BloodDonorUser");
            }
            else if (registrationMV.UserTypeID == 5)
            {
                return RedirectToAction("HospitalUser");
            }
            else if (registrationMV.UserTypeID == 6)
            {
                return RedirectToAction("BloodBankUser");
            }
            else if (registrationMV.UserTypeID == 7)
            {
                return RedirectToAction("OrganDonorUser");
            }
            else if (registrationMV.UserTypeID == 8)
            {
                return RedirectToAction("OrganBankUser");
            }
            else
            {
                return RedirectToAction("MainHome", "Home");
            }
          
        }
        public ActionResult HospitalUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HospitalUser(RegistrationMV registrationMV)
        {
            if(ModelState.IsValid)
            {
                var checktitle = DB.HospitalTables.Where(h => h.FullName == registrationMV.Hospital.FullName.Trim()).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {

                        try
                        {
                            var user = new UserTable();

                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                           
                            user.AccountStatusID = 2;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();
                            var hospital = new HospitalTable();

                            hospital.FullName = registrationMV.Hospital.FullName;
                            hospital.Address = registrationMV.Hospital.Address;
                            hospital.PhoneNo = registrationMV.ContactNo;
                            hospital.Website = registrationMV.Hospital.Website;
                            hospital.Email = registrationMV.Hospital.Email;
                            hospital.Location = registrationMV.Hospital.Address;
                            hospital.CityID = registrationMV.CityID;
                            hospital.UserID = user.UserID;
                            DB.HospitalTables.Add(hospital);
                            try
                            {
                                DB.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting
                                        // the current instance as InnerException
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For Registration, Your Query will be Reviewed Shortly";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch 
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information");
                            transaction.Rollback();
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Hospital Already Registered!");
             
            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationMV);
        }
        public ActionResult BloodDonorUser()
        {
           
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");

            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodDonorUser(RegistrationMV registrationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.BloodDonorTables.Where(h => h.FullName == registrationMV.BloodDonor.FullName.Trim() && h.NID==registrationMV.BloodDonor.NID).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {

                        try
                        {
                            var user = new UserTable();

                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 2;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var bloodDonor = new BloodDonorTable();

                            bloodDonor.FullName = registrationMV.User.UserName;
                            bloodDonor.BloodGroupID = registrationMV.BloodGroupID;
                            bloodDonor.LastDonationDate = registrationMV.BloodDonor.LastDonationDate;
                            bloodDonor.ContactNo = registrationMV.ContactNo;
                            bloodDonor.NID = registrationMV.BloodDonor.NID;
                            bloodDonor.Location = registrationMV.BloodDonor.Location;
                            bloodDonor.CityID = registrationMV.CityID;
                            bloodDonor.HealthIssues = registrationMV.BloodDonor.HealthIssues;
                            bloodDonor.GenderID = registrationMV.GenderID;

                            bloodDonor.UserID = user.UserID;
                          
                           
                            DB.BloodDonorTables.Add(bloodDonor);
                            try
                            {
                                DB.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting
                                        // the current instance as InnerException
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For Registration, Your Query will be Reviewed Shortly";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information");
                            transaction.Rollback();
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Donor Already Registered!");

            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", registrationmv.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", registrationMV.GenderID);
            return View(registrationMV);

           
        }
        public ActionResult OrganDonorUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            ViewBag.OrganTypeID = new SelectList(DB.OrganTypeTables.ToList(), "OrganTypeID", "OrganName", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrganDonorUser(RegistrationMV registrationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.OrganDonorTables.Where(h => h.OrganDonorName == registrationMV.OrganDonor.OrganDonorName.Trim() && h.NID == registrationMV.OrganDonor.NID).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {

                        try
                        {
                            var user = new UserTable();

                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 2;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var organDonor = new OrganDonorTable();

                            organDonor.OrganDonorName = registrationMV.User.UserName;
                            organDonor.OrganTypeID = registrationMV.OrganTypeID;
                            organDonor.LastDonatedOrgan = registrationMV.OrganDonor.LastDonatedOrgan;
                            
                            organDonor.NID = registrationMV.OrganDonor.NID;
                            organDonor.Location = registrationMV.OrganDonor.Location;
                            organDonor.CityID = registrationMV.CityID;
                            organDonor.HealthIssues = registrationMV.OrganDonor.HealthIssues;
                            organDonor.GenderID = registrationMV.GenderID;
                            organDonor.ContactNo = registrationMV.ContactNo;
                            organDonor.UserID = user.UserID;


                            DB.OrganDonorTables.Add(organDonor);
                            try
                            {
                                DB.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting
                                        // the current instance as InnerException
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For Registration, Your Query will be Reviewed Shortly";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information");
                            transaction.Rollback();
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Donor Already Registered!");

            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            ViewBag.OrganTypeID = new SelectList(DB.OrganTypeTables.ToList(), "OrganTypeID", "OrganName", registrationmv.OrganTypeID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", registrationMV.GenderID);
            return View(registrationMV);
        }
        public ActionResult BloodBankUser()
        {

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodBankUser(RegistrationMV registrationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.BloodBankTables.Where(h => h.BloodBankName == registrationMV.BloodBank.BloodBankName.Trim() && h.PhoneNo == registrationMV.BloodBank.PhoneNo).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {

                        try
                        {
                            var user = new UserTable();

                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 2;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var bloodBank = new BloodBankTable();

                            bloodBank.BloodBankName = registrationMV.BloodBank.BloodBankName;
                            bloodBank.Address = registrationMV.BloodBank.Location;
                            bloodBank.PhoneNo = registrationMV.ContactNo;
                            bloodBank.Location = registrationMV.BloodBank.Location;
                            bloodBank.Website = registrationMV.BloodBank.Website;
                        
                            bloodBank.CityID = registrationMV.CityID;
                            bloodBank.Email = registrationMV.BloodBank.Email;
                            

                            bloodBank.UserID = user.UserID;


                            DB.BloodBankTables.Add(bloodBank);
                            try
                            {
                                DB.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting
                                        // the current instance as InnerException
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For Registration, Your Query will be Reviewed Shortly";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information");
                            transaction.Rollback();
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Blood Bank Already Registered!");

            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
           
            return View(registrationmv);
        }
        public ActionResult OrganBankUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrganBankUser(RegistrationMV registrationMV)
        { 
        if (ModelState.IsValid)
            {
                var checktitle = DB.OrganBankTables.Where(h => h.OrganBankName == registrationMV.OrganBank.OrganBankName.Trim() && h.PhoneNo == registrationMV.OrganBank.PhoneNo).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {

                        try
                        {
                            var user = new UserTable();

                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 2;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var organBank = new OrganBankTable();

                            organBank.OrganBankName = registrationMV.OrganBank.OrganBankName;
                            organBank.Address = registrationMV.OrganBank.Location;
                            organBank.PhoneNo = registrationMV.ContactNo;
                            organBank.Location = registrationMV.OrganBank.Location;
                            organBank.Website = registrationMV.OrganBank.Website;
                        
                            organBank.CityID = registrationMV.CityID;
                            organBank.Email = registrationMV.OrganBank.Email;
                            

                            organBank.UserID = user.UserID;


                            DB.OrganBankTables.Add(organBank);
                            try
                            {
                                DB.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
    // raise a new exception nesting
    // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
}
                                }
                                throw raise;
                            }
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For Registration, Your Query will be Reviewed Shortly";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information");
                            transaction.Rollback();
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Organ Bank Already Registered!");

            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
           
            return View(registrationmv);
        }
        public ActionResult SeekerUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");
            return View(registrationmv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerUser(RegistrationMV registrationMV)
        {
            if (ModelState.IsValid)
            {
                var checktitle = DB.SeekerTables.Where(h => h.FullName == registrationMV.Seeker.FullName.Trim()).FirstOrDefault();
                if (checktitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {

                        try
                        {
                            var user = new UserTable();

                            user.UserName = registrationMV.User.UserName;
                            user.Password = registrationMV.User.Password;
                            user.EmailAddress = registrationMV.User.EmailAddress;
                            user.AccountStatusID = 2;
                            user.UserTypeID = registrationMV.UserTypeID;
                            user.Description = registrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var seeker = new SeekerTable();

                            seeker.FullName = registrationMV.User.UserName;
                            seeker.Age = registrationMV.Seeker.Age;
                            seeker.ContactNo = registrationMV.ContactNo;
                            seeker.Address = registrationMV.Seeker.Address;
                            seeker.GenderID = registrationMV.GenderID;
                            seeker.RegistrationDate = DateTime.Now;
                            seeker.CityID = registrationMV.CityID;
                            seeker.BloodGroupID = registrationMV.BloodGroupID;
                            seeker.NID = registrationMV.Seeker.NID;

                            seeker.UserID = user.UserID;


                            DB.SeekerTables.Add(seeker);
                            try
                            {
                                DB.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting
                                        // the current instance as InnerException
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                            transaction.Commit();
                            ViewData["Message"] = "Thanks For Registration, Your Query will be Reviewed Shortly";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Information");
                            transaction.Rollback();
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Seeker Already Registered!");

            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", registrationMV.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", registrationMV.GenderID);
            return View();
        }


    }
}