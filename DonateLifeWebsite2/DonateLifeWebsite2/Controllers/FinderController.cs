using DatabaseLayer;
using DonateLifeWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DonateLifeWebsite2.Controllers
{
    public class FinderController : Controller
    {
        DonateLifeWebsiteSDEntities DB = new DonateLifeWebsiteSDEntities();
        // GET: Finder
        public ActionResult FinderBloodDonors()
        {
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", 0);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", 0);
            return View(new FinderMV());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinderBloodDonors(FinderMV finderMV)
        {
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            var list = new List<FinderSearchResultMV>();
            var setdate = DateTime.Now.AddDays(-120);
            var donors = DB.BloodDonorTables.Where(d => d.BloodGroupID == finderMV.BloodGroupID && d.CityID == finderMV.CityID && d.LastDonationDate < setdate).ToList();
            foreach (var donor in donors)
            {

                var user = DB.UserTables.Find(donor.UserID);
                if (userid != user.UserID)
                {
                    if (user.AccountStatusID == 3)
                    {
                        var adddonor = new FinderSearchResultMV();
                        adddonor.UserID = user.UserID;
                        adddonor.BloodGroup = donor.BloodGroupsTable.BloodGroup;
                        adddonor.BloodGroupID = donor.BloodGroupID;
                        adddonor.ContactNo = donor.ContactNo;
                        adddonor.BloodDonorID = donor.BloodDonorID;
                        adddonor.FullName = donor.FullName;
                        adddonor.Address = donor.Location;
                        adddonor.UserType = "Person";
                        adddonor.UserTypeID = user.UserTypeID;
                        finderMV.SearchResult.Add(adddonor);
                    }
                }
            }

            var bloodbanks = DB.BloodBankStockTables.Where(d => d.BloodGroupID == finderMV.BloodGroupID && d.Quantity > 0).ToList();
            foreach (var bloodbank in bloodbanks)
            {
                var getbloodbank = DB.BloodBankTables.Find(bloodbank.BloodBankID);
                var user = DB.UserTables.Find(getbloodbank.UserID);
                if (userid != user.UserID)
                {
                    if (user.AccountStatusID == 3)
                    {


                        var adddonor = new FinderSearchResultMV();
                        adddonor.UserID = user.UserID;
                        adddonor.BloodGroup = bloodbank.BloodGroupsTable.BloodGroup;
                        adddonor.BloodGroupID = bloodbank.BloodGroupID;
                        adddonor.ContactNo = bloodbank.BloodBankTable.PhoneNo;
                        adddonor.Address = bloodbank.BloodBankTable.Address;
                        adddonor.BloodDonorID = bloodbank.BloodBankID;
                        adddonor.FullName = bloodbank.BloodBankTable.BloodBankName;
                        adddonor.UserType = "Blood bank";
                        adddonor.UserTypeID = user.UserTypeID;
                        finderMV.SearchResult.Add(adddonor);
                    }
                }
            }
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupsTables.ToList(), "BloodGroupID", "BloodGroup", finderMV.BloodGroupID);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", finderMV.CityID);
            return View(finderMV);
        }
        

        public ActionResult RequestForBlood(int? donorid, int? usertypeid, int? bloodgroupid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return Redirect("/Home/MainHome#registrationsection");
            }
            var request = new RequestMV();
            request.AcceptedID = (int)donorid;

            if (usertypeid == 4)
            {
                request.AcceptedTypeID = 1;
            }
            else if (usertypeid == 6)
            {
                request.AcceptedTypeID = 2;
            }

            request.RequiredBloodGroupID = (int)bloodgroupid;

            return View(request);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult RequestForBlood(RequestMV requestMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return Redirect("/Home/MainHome#registrationsection");
            }
            int UserTypeID = 0;
            int RequestTypeID = 0;
            int RequestByID = 0;
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out UserTypeID);

            if (UserTypeID == 4)//Blood Donor
            {

                int.TryParse(Convert.ToString(Session["BloodDonorID"]), out RequestByID);




            }
            else if (UserTypeID == 6)//Blood Bank
            {


                RequestTypeID = 4;
                int.TryParse(Convert.ToString(Session["BloodBankID"]), out RequestByID);




            }
            else if (UserTypeID == 7)//Organ Donor
            {
                int.TryParse(Convert.ToString(Session["OrganDonorID"]), out RequestByID);


            }
            else if (UserTypeID == 8)//Organ Bank
            {
                int.TryParse(Convert.ToString(Session["OrganBankID"]), out RequestByID);


            }
            else if (UserTypeID == 5)//Hospital
            {
                RequestTypeID = 3;
                int.TryParse(Convert.ToString(Session["HospitalID"]), out RequestByID);



            }
            else if (UserTypeID == 9)//Seeker
            {
                RequestTypeID = 2;
                int.TryParse(Convert.ToString(Session["SeekerID"]), out RequestByID);


            }


            if (ModelState.IsValid)
            {
                var request = new RequestTable();
                request.RequestDate = DateTime.Now;
                request.RequestByID = RequestByID;
                request.AcceptedID = requestMV.AcceptedID;
                request.RequiredBloodGroupID = requestMV.RequiredBloodGroupID;
                request.RequestTypeID = RequestTypeID;
                request.AcceptedTypeID = requestMV.AcceptedTypeID;
                request.RequestStatusID = 1;

                request.ExpectedDate = requestMV.ExpectedDate;
                request.RequestDetails = requestMV.RequestDetails;



                DB.RequestTables.Add(request);
                DB.SaveChanges();
                return RedirectToAction("ShowAllRequests");

            }
            return View();
        }
        public ActionResult ShowAllRequests()
        { 
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int UserTypeID = 0;
            int RequestTypeID = 0;
            int RequestByID = 0;
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out UserTypeID);

            if (UserTypeID == 4)//Blood Donor
            {

                int.TryParse(Convert.ToString(Session["BloodDonorID"]), out RequestByID);




            }
            else if (UserTypeID == 6)//Blood Bank
            {


                RequestTypeID = 4;
                int.TryParse(Convert.ToString(Session["BloodBankID"]), out RequestByID);




            }
            else if (UserTypeID == 7)//Organ Donor
            {
                int.TryParse(Convert.ToString(Session["OrganDonorID"]), out RequestByID);


            }
            else if (UserTypeID == 8)//Organ Bank
            {
                int.TryParse(Convert.ToString(Session["OrganBankID"]), out RequestByID);


            }
            else if (UserTypeID == 5)//Hospital
            {
                RequestTypeID = 3;
                int.TryParse(Convert.ToString(Session["HospitalID"]), out RequestByID);



            }
            else if (UserTypeID == 9)//Seeker
            {
                RequestTypeID = 2;
                int.TryParse(Convert.ToString(Session["SeekerID"]), out RequestByID);


            }
            var requests = DB.RequestTables.Where(r => r.RequestByID == RequestByID && r.RequestTypeID == RequestTypeID).ToList();
            var list = new List<RequestListMV>();
            foreach (var request in requests)
            {
                var addrequest = new RequestListMV();
                addrequest.RequestID = request.RequestID;
                addrequest.RequestDate = request.RequestDate.ToString("dd MMMM,yyyy");

                addrequest.RequestByID = request.RequestByID;

                addrequest.AcceptedID = request.AcceptedID;

                if (request.AcceptedTypeID == 1)//Blood Donor
                {
                    var getdonor = DB.BloodDonorTables.Find(request.AcceptedID);
                    addrequest.AcceptedFullName = getdonor.FullName;
                    addrequest.ContactNo = getdonor.ContactNo;
                    addrequest.Address = getdonor.Location;
                }
                else if (request.AcceptedTypeID == 2)//Blood Bank
                {
                    var getbloodbank = DB.BloodBankTables.Find(request.AcceptedID);
                    addrequest.AcceptedFullName = getbloodbank.BloodBankName;
                    addrequest.ContactNo = getbloodbank.PhoneNo;
                    addrequest.Address = getbloodbank.Address;
                }


                addrequest.AcceptedTypeID = request.AcceptedTypeID;
                addrequest.AcceptedType = request.AcceptedTypeTable.AcceptedType;
                addrequest.RequiredBloodGroupID = request.RequiredBloodGroupID;
                var bloodgroup = DB.BloodGroupsTables.Find(addrequest.RequiredBloodGroupID);
                addrequest.BloodGroup = bloodgroup.BloodGroup;
               
                addrequest.RequestTypeID = request.RequestID;
                addrequest.RequestType = request.RequestTypeTable.RequestType;
                addrequest.RequestStatus = request.RequestStatusTable.RequestStatus;
                addrequest.RequestStatusID = request.RequestStatusID;


                addrequest.ExpectedDate = request.ExpectedDate.ToString("dd MMMM,yyyy");

                addrequest.RequestDetails = request.RequestDetails;
                list.Add(addrequest);

               
            }
            return View(list);

        }
        public ActionResult CancelRequest(int ? id )
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var request = DB.RequestTables.Find(id);
            request.RequestStatusID = 4;
            DB.Entry(request).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("ShowAllRequests");

        }
        public ActionResult AcceptRequest(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var request = DB.RequestTables.Find(id);
            request.RequestStatusID = 2;
            DB.Entry(request).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("ShowAllRequests");

        }

        public ActionResult DonorRequests()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int UserTypeID = 0;
            int AcceptedTypeID = 0;
            int AcceptedByID = 0;
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out UserTypeID);

            if (UserTypeID == 4)//Blood Donor
            {
                AcceptedTypeID = 1;
                int.TryParse(Convert.ToString(Session["BloodDonorID"]), out AcceptedByID);




            }
            else if (UserTypeID == 6)//Blood Bank
            {


                AcceptedTypeID = 2;
                int.TryParse(Convert.ToString(Session["BloodBankID"]), out AcceptedByID);




            }
            else if (UserTypeID == 7)//Organ Donor
            {
                int.TryParse(Convert.ToString(Session["OrganDonorID"]), out AcceptedByID);


            }
            else if (UserTypeID == 8)//Organ Bank
            {
                int.TryParse(Convert.ToString(Session["OrganBankID"]), out AcceptedByID);


            }
            else if (UserTypeID == 5)//Hospital
            {
                int.TryParse(Convert.ToString(Session["HospitalID"]), out AcceptedByID);



            }
            else if (UserTypeID == 9)//Seeker
            {
         
                int.TryParse(Convert.ToString(Session["SeekerID"]), out AcceptedByID);


            }
            var requests = DB.RequestTables.Where(r => r.AcceptedID == AcceptedByID && r.AcceptedTypeID == AcceptedTypeID).ToList();
            var list = new List<RequestListMV>();
            foreach (var request in requests)
            {
                var addrequest = new RequestListMV();
                addrequest.RequestID = request.RequestID;
                addrequest.RequestDate = request.RequestDate.ToString("dd MMMM,yyyy");

                addrequest.RequestByID = request.RequestByID;

                addrequest.AcceptedID = request.AcceptedID;

                if (request.RequestTypeID == 2)//Seeker
                {
                    var getseeker = DB.SeekerTables.Find(request.RequestByID);
                    addrequest.RequestBy = getseeker.FullName;
                    addrequest.ContactNo = getseeker.ContactNo;
                    addrequest.Address = getseeker.Address;
                }
                else if (request.RequestTypeID == 3)//Hospital
                {
                    var gethospital = DB.HospitalTables.Find(request.RequestByID);
                    addrequest.AcceptedFullName = gethospital.FullName;
                    addrequest.ContactNo = gethospital.PhoneNo;
                    addrequest.Address = gethospital.Address;
                }
                else if (request.RequestTypeID == 4)//Blood Bank
                {
                    var getbloodbank = DB.BloodBankTables.Find(request.RequestByID);
                    addrequest.AcceptedFullName = getbloodbank.BloodBankName;
                    addrequest.ContactNo = getbloodbank.PhoneNo;
                    addrequest.Address = getbloodbank.Address;
                }

                addrequest.AcceptedTypeID = request.AcceptedTypeID;
                addrequest.AcceptedType = request.AcceptedTypeTable.AcceptedType;
                addrequest.RequiredBloodGroupID = request.RequiredBloodGroupID;
                var bloodgroup = DB.BloodGroupsTables.Find(addrequest.RequiredBloodGroupID);
                addrequest.BloodGroup = bloodgroup.BloodGroup;

                addrequest.RequestTypeID = request.RequestID;
                addrequest.RequestType = request.RequestTypeTable.RequestType;
                addrequest.RequestStatus = request.RequestStatusTable.RequestStatus;
                addrequest.RequestStatusID = request.RequestStatusID;


                addrequest.ExpectedDate = request.ExpectedDate.ToString("dd MMMM,yyyy");

                addrequest.RequestDetails = request.RequestDetails;
                list.Add(addrequest);


            }
            return View(list);

        }
        public ActionResult CompleteRequest(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login","Home");
            }
            var request = DB.RequestTables.Find(id);
            if(request.AcceptedTypeID==1)//Donor
            {
                var donor = DB.BloodDonorTables.Find(request.AcceptedID);
                donor.LastDonationDate = DateTime.Now;
                DB.Entry(donor).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                request.RequestStatusID = 3;
                DB.Entry(request).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("ShowAllRequests");
            }
            
                var bloodBank = DB.BloodBankTables.Find(request.AcceptedID);
                var bloodbankstockMV = new BloodBankStockMV();
                bloodbankstockMV.BloodBankID = bloodBank.BloodBankID;

                bloodbankstockMV.BloodGroupID = request.RequiredBloodGroupID;
                bloodbankstockMV.BloodBankTitle = bloodBank.BloodBankName;
                var bloodgroup = DB.BloodGroupsTables.Find(request.RequiredBloodGroupID);
                bloodbankstockMV.BloodGroup = bloodgroup.BloodGroup;
                bloodbankstockMV.Quantity = 1;


            return View(bloodbankstockMV);






    

          

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteRequest(BloodBankStockMV bloodBankStockMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                var request = DB.RequestTables.Find(bloodBankStockMV.BloodBankStockID);

                var bloodstock = DB.BloodBankStockTables.Where(b => b.BloodBankID == bloodBankStockMV.BloodBankID && b.BloodGroupID == bloodBankStockMV.BloodGroupID).FirstOrDefault();
                if(bloodstock.Quantity < bloodBankStockMV.Quantity)
                {

                    ModelState.AddModelError(string.Empty, "Available Quantity is "+bloodstock.Quantity+"!");
                    return View(bloodBankStockMV);
                }
                bloodstock.Quantity = bloodstock.Quantity - bloodBankStockMV.Quantity;
                DB.Entry(bloodstock).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();



                request.RequestStatusID = 3;
                DB.Entry(request).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("ShowAllRequests");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Please Provide Quantity!");
                return View(bloodBankStockMV);
            }

           










        }
        public ActionResult CancelRequestByDonor(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var request = DB.RequestTables.Find(id);
            request.RequestStatusID = 4;
            DB.Entry(request).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("DonorRequests");

        }
       
    }
}