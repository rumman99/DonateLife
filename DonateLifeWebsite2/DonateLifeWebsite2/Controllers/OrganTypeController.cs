using DatabaseLayer;
using DonateLifeWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DonateLifeWebsite2.Controllers
{
    public class OrganTypeController : Controller
    {
        DonateLifeWebsiteSDEntities DB = new DonateLifeWebsiteSDEntities();



        public ActionResult AllOrganType()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var organtypes = DB.OrganTypeTables.ToList();
            var listorgantypes = new List<OrganTypeMV>();
            foreach (var organtype in organtypes)
            {
                var addorgantype = new OrganTypeMV();
                addorgantype.OrganTypeID = organtype.OrganTypeID;
                addorgantype.OrganName = organtype.OrganName;
                listorgantypes.Add(addorgantype);
            }
            return View(listorgantypes);
        }

        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var organtype = new OrganTypeMV();
            return View(organtype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrganTypeMV organTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var organTypeTable = new OrganTypeTable();
                organTypeTable.OrganTypeID = organTypeMV.OrganTypeID;
                organTypeTable.OrganName= organTypeMV.OrganName;
                DB.OrganTypeTables.Add(organTypeTable);
                DB.SaveChanges();
                return RedirectToAction("AllOrganType");
            }
            return View(organTypeMV);
        }
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var organtype = DB.OrganTypeTables.Find(id);
            if (organtype == null)
            {
                return HttpNotFound();
            }
            var organtypemv = new OrganTypeMV();
            organtypemv.OrganTypeID = organtype.OrganTypeID;
            organtypemv.OrganName = organtype.OrganName;
            return View(organtypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrganTypeMV organTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var organTypeTable = new OrganTypeTable();
                organTypeTable.OrganTypeID = organTypeMV.OrganTypeID;
                organTypeTable.OrganName = organTypeMV.OrganName;
                DB.Entry(organTypeTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllOrganType");
            }
            return View(organTypeMV);
        }
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var organtype = DB.OrganTypeTables.Find(id);
            if (organtype == null)
            {
                return HttpNotFound();
            }
            var organtypemv = new OrganTypeMV();
            organtypemv.OrganTypeID = organtype.OrganTypeID;
            organtypemv.OrganName = organtype.OrganName;
            return View(organtypemv);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var organtype = DB.OrganTypeTables.Find(id);
            DB.OrganTypeTables.Remove(organtype);
            DB.SaveChanges();
            return RedirectToAction("AllOrganType");
        }



    }
}