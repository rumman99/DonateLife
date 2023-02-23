using DatabaseLayer;
using DonateLifeWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonateLifeWebsite2.Controllers
{
    public class AccountsController : Controller
    {
        DonateLifeWebsiteSDEntities DB = new DonateLifeWebsiteSDEntities();
        // GET: Accounts
        public ActionResult AllNewUserRequests()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var users = DB.UserTables.Where(u => u.AccountStatusID == 2).ToList();
         

            return View(users);
        }
        public ActionResult UserDetails(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            return View(user);
        }
        public ActionResult UserApproved(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 3;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();

            return RedirectToAction("AllNewUserRequests");
        }
        public ActionResult UserRejected(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 4;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();

            return RedirectToAction("AllNewUserRequests");
        }

        public ActionResult AddNewDonorByBloodBank()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var collectbloodMV = new CollectBloodMV();

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");
            return View(collectbloodMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddNewDonorByBloodBank(CollectBloodMV collectBloodMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int bloodbankID = 0;
            
            string bloodbankid = Convert.ToString(Session["BloodBankID"]);
            int.TryParse(bloodbankid, out bloodbankID);
            var currentdate = DateTime.Now.Date;
            var currentcampaign = DB.CampaignTables.Where(c => c.CampaignDate == currentdate  && c.BloodBankID == bloodbankID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                using (var transaction = DB.Database.BeginTransaction())
                {

                    try
                    {
                        var checkDonor = DB.BloodDonorTables.Where(d => d.NID.Trim().Replace("-", "") == collectBloodMV.DonorDetails.NID.Trim().Replace("-", "")).FirstOrDefault();
                        if (checkDonor == null)
                        {
                            var user = new UserTable();

                            user.UserName = collectBloodMV.DonorDetails.FullName.Trim();
                            user.Password = "12345";
                            user.EmailAddress = collectBloodMV.DonorDetails.EmailAddress;
                            user.AccountStatusID = 3;
                            user.UserTypeID = 4;
                            user.Description = "Added By Blood Bank";
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var bloodDonor = new BloodDonorTable();

                            bloodDonor.FullName = collectBloodMV.DonorDetails.FullName;
                            bloodDonor.BloodGroupID = collectBloodMV.BloodGroupID;
                            bloodDonor.LastDonationDate = DateTime.Now;
                            bloodDonor.ContactNo = collectBloodMV.DonorDetails.ContactNo;
                            bloodDonor.NID = collectBloodMV.DonorDetails.NID;
                            bloodDonor.Location = collectBloodMV.DonorDetails.Location;
                            bloodDonor.CityID = collectBloodMV.CityID;
                            bloodDonor.HealthIssues = collectBloodMV.DonorDetails.HealthIssues;
                            bloodDonor.GenderID = collectBloodMV.GenderID;

                            bloodDonor.UserID = user.UserID;


                            DB.BloodDonorTables.Add(bloodDonor);
                            DB.SaveChanges();
                            checkDonor = DB.BloodDonorTables.Where(d => d.NID.Trim().Replace("-", "") == collectBloodMV.DonorDetails.NID.Trim().Replace("-", "")).FirstOrDefault();

                        }



                       
                            var checkbloodgroupstock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodbankID && s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();
                            if (checkbloodgroupstock == null)
                            {
                                var bloodbankstock = new BloodBankStockTable();
                                bloodbankstock.BloodBankID = bloodbankID;
                                bloodbankstock.BloodGroupID = collectBloodMV.BloodGroupID;
                                bloodbankstock.Status = true;
                                bloodbankstock.Quantity = 0;
                                bloodbankstock.Description = "";

                                DB.BloodBankStockTables.Add(bloodbankstock);
                                DB.SaveChanges();
                                checkbloodgroupstock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodbankID && s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();
                            }

                            checkbloodgroupstock.Quantity += collectBloodMV.Quantity;
                            DB.Entry(checkbloodgroupstock).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();

                            var collectblooddetail = new BloodBankStockDetailTable();

                            collectblooddetail.BloodBankStockID = checkbloodgroupstock.BloodBankStockID;
                            collectblooddetail.BloodGroupID = collectBloodMV.BloodGroupID;
                            collectblooddetail.Quantity = collectBloodMV.Quantity;
                            collectblooddetail.BloodDonorID = checkDonor.BloodDonorID;

                            collectblooddetail.BloodDonateDateTime = DateTime.Now;
                            collectblooddetail.CampaignID = currentcampaign.CampaignID;
                            DB.BloodBankStockDetailTables.Add(collectblooddetail);
                            DB.SaveChanges();

                            checkDonor.LastDonationDate = DateTime.Now;
                            DB.Entry(checkDonor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();

                            transaction.Commit();


                            return RedirectToAction("BloodBankStock", "BloodBank");

                        
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Please Provide Correct Information");
                        transaction.Rollback();
                    }

                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide Donor Full Information");
            }
                ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", collectBloodMV.CityID);
                ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", collectBloodMV.BloodGroupID);
                ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", collectBloodMV.GenderID);

                return View(collectBloodMV);
            }
        
    
    }
    }
