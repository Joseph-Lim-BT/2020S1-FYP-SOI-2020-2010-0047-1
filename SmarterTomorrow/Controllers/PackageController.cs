using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.OpenXmlFormats.Spreadsheet;
using Org.BouncyCastle.Asn1.Mozilla;
using SmarterTomorrow.Models;

namespace SmarterTomorrow.Controllers
{
    public class PackageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListPackage()
        {
            List<package> list = DBUtl.GetList<package>("SELECT * FROM Package ");
            ViewData["equipment"] = GetListEquipment();
            ViewData["accessories"] = GetListAccessories();
            return View(list);

        }

        public IActionResult AddPackage()
        {
            ViewData["Equipment"] = GetListEquipment();
            ViewData["Accessories"] = GetListAccessories();
            return View();
        }

        [Authorize(Roles = "0,1,2,3")]
        [HttpPost]
        public IActionResult AddPackage(package newEquipment)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Equipment"] = GetListEquipment();
                ViewData["Accessories"] = GetListAccessories();
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("AddPackage");
            }
            else
            {
                string person = GetPerson();
                string PackageID = newEquipment.EQUIPMENT_ID.Substring(0,6) + "," + newEquipment.ACCESSORY_ID.Substring(0,5);
                string insert = "INSERT INTO PACKAGE(EQUIPMENT_ID, ACCESSORY_ID, PACKAGE_TYPE, PACKAGE_DESC, DTE_TIME_CR, DTE_TIME_LAST_MOD, CREATED_BY, MODIFIED_BY, PACKAGE_ID) VALUES('{0}', '{1}', '{2}', '{3}', '{4:yyyy-MM-dd}', '{5:yyyy-MM-dd}', '{6}', '{7}', '{8}')";

                if (DBUtl.ExecSQL(insert, newEquipment.EQUIPMENT_ID, newEquipment.ACCESSORY_ID, newEquipment.PACKAGE_TYPE, newEquipment.PACKAGE_DESC, newEquipment.DTE_TIME_CR, newEquipment.DTE_TIME_LAST_MOD, person, person, PackageID) == 1)
                {
                    TempData["Message"] = "Equipment Added to Package";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListPackage");
            }


        }
        [Authorize(Roles = "0,1,2,3")]
        [HttpGet]
        public IActionResult UpdatePackage(string id)
        {
            ViewData["Equipment"] = GetListEquipment();
            ViewData["Accessories"] = GetListAccessories();
            string packageSQL = @"SELECT * FROM Package WHERE Package_ID = '{0}'";
            List<package> lstPackage = DBUtl.GetList<package>(packageSQL, id);

            // If the record is found, pass the model to the View
            if (lstPackage.Count == 1)
            {
                return View(lstPackage[0]);
            }
            else
            // Otherwise redirect to the list page
            {
                TempData["Message"] = "Package not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListPackage");
            }
        }

        [Authorize(Roles = "0,1,2,3")]
        [HttpPost]
        public IActionResult UpdatePackage(package pg)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Equipment"] = GetListEquipment();
                ViewData["Accessories"] = GetListAccessories();
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("ListPackage");
            }

            string person = GetPerson();

            string update = @"UPDATE PACKAGE SET EQUIPMENT_ID='{1}', ACCESSORYID={2}, PACKAGE_TYPE='{3}', PACKAGE_DESC='{4}', MODIFIED_BY='{5}', DTE_TIME_LAST_MOD='{6:yyyy-MM-dd}' WHERE PACKAGE_ID='{0}'";

            if (DBUtl.ExecSQL(update, pg.PACKAGE_ID, pg.EQUIPMENT_ID, pg.ACCESSORY_ID, pg.PACKAGE_TYPE, pg.PACKAGE_DESC, person, pg.DTE_TIME_LAST_MOD) == 1)
            {
                TempData["Message"] = "Package Updated";
                TempData["MsgType"] = "Success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }
            return RedirectToAction("ListPackage");
        }

        [Authorize(Roles = "0,1")]
        public IActionResult DeletePackage(string id)
        {
            string select = @"SELECT * FROM Package 
                              WHERE PACKAGE_ID='{0}'";
            DataTable ds = DBUtl.GetTable(select, id);
            if (ds.Rows.Count != 1)
            {
                TempData["Message"] = "Package record no longer exists.";
                TempData["MsgType"] = "warning";
            }
            else
            {
                string delete = "DELETE FROM Package WHERE PACKAGE_ID='{0}'";
                int res = DBUtl.ExecSQL(delete, id);
                if (res == 1)
                {
                    TempData["Message"] = "Package Deleted";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
            }
            return RedirectToAction("ListPackage");

        }

        [Authorize(Roles = "0,1,2")]
        [HttpGet]
        public IActionResult AddPackageAssigned()
        {
            ViewData["PackageID"] = GetListPackage();
            ViewData["Assign"] = GetListAssigned();
            ViewData["ActivityID"] = GetActivityID();
            return View();

        }

        [HttpPost]
        [Authorize(Roles = "0,1,2")]
        public IActionResult AddPackageAssigned(assignment newEquipment)
        {
            if (!ModelState.IsValid)
            {
                ViewData["PackageID"] = GetListPackage();
                ViewData["Assign"] = GetListAssigned();
                ViewData["ActivityID"] = GetActivityID();
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("AddPackageAssigned");
            }
            else
            {
                DateTime dateTimeNow = DateTime.Now;

                string person = GetPerson();

                string buttID = "";
                string insert;
                int result;
                int result2;
                string updateEq1;
                string updateAcc1;
                List<package> lstPackage = GetListPackage();
                List<Equipment> lstEq = GetListEquipment();
                List<Equipment> lstEq2;
                List<accessories> lstAcc = GetListAccessories();
                List<accessories> lstAcc2;
                foreach (package pk in lstPackage)
                {                    
                    foreach (accessories acid in lstAcc)
                    {
                        if (acid.ACCESSORY_ID.Equals(pk.ACCESSORY_ID))
                        {
                            lstAcc2 = DBUtl.GetList<accessories>(@"SELECT * FROM accessories WHERE QUANTITY > 0 AND ACCESSORY_NAME='" + acid.ACCESSORY_NAME + "'");
                            if (lstAcc2.Count > 0)
                            {
                                updateAcc1 = @"UPDATE accessories SET QUANTITY=QUANTITY-1 WHERE ACCESSORY_ID='" + pk.ACCESSORY_ID + "'";
                                result2 = DBUtl.ExecSQL(updateAcc1);
                                break;
                            }
                        }
                    }

                    if (pk.PACKAGE_ID.Equals(newEquipment.PACKAGE_ID))
                    {
                        foreach (Equipment eqid in lstEq)
                        {
                            if (eqid.EQUIPMENT_ID.Equals(pk.EQUIPMENT_ID))
                            {
                                lstEq2 = DBUtl.GetList<Equipment>(@"SELECT * FROM equipment WHERE QUANTITY > 0 AND STATUS=1 AND EQUIPMENT_NAME='" + eqid.EQUIPMENT_NAME + "'");
                                if (lstEq2.Count > 0)
                                {
                                    updateEq1 = @"UPDATE equipment SET STATUS=2 WHERE BUTT_NO='" + lstEq2[0].BUTT_NO + "' AND EQUIPMENT_NAME='" + eqid.EQUIPMENT_NAME + "'";
                                    result2 = DBUtl.ExecSQL(updateEq1);
                                    buttID = person + "[BN:" + lstEq2[0].BUTT_NO + "]";
                                }
                            }
                        }
                    }

                }
                insert =
@"INSERT INTO assignment(NRIC,
                DTE_TIME_CR,DTE_TIME_LAST_MOD,
                CREATED_BY,MODIFIED_BY,ACTIVITY_ID,PACKAGE_ID) 
                                 VALUES('{0}',
                '{1:yyyy-MM-dd hh:mm:ss}','{2:yyyy-MM-dd hh:mm:ss}','{3}',
                '{4}','{5}','{6}')";
                result = DBUtl.ExecSQL(insert, newEquipment.NRIC, dateTimeNow, dateTimeNow, buttID, person, newEquipment.ACTIVITY_ID, newEquipment.PACKAGE_ID);
                result2 = 1;

                if (result == 1 && result2 == 1)
                {
                    TempData["Message"] = "Equipment Assigned Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListEquipmentAssigned", "Equipment");
            }
                
        }


        private string GetPerson()
        {
            string personSql = @"SELECT *
                                FROM usr WHERE NRIC='" + User.Identity.Name + "'";
            List<User> assigned = DBUtl.GetList<User>(personSql);
            string person = assigned[0].FULL_NAME;
            return person;
        }

        private object GetListAssigned()
        {
            // Get a list of all usr from the database
            string AssSql = @"SELECT * FROM usr ORDER BY FULL_NAME";
            List<User> lstAss = DBUtl.GetList<User>(AssSql);
            return lstAss;
        }

        private List<Equipment> GetListEquipment()
        {
            string MaterialSql = @"SELECT * FROM equipment";
            List<Equipment> lstMaterial = DBUtl.GetList<Equipment>(MaterialSql);
            return lstMaterial;
        }

        private List<activity> GetActivityID()
        {
            string activitySql = @"SELECT * FROM activity";
            List<activity> lstactivity = DBUtl.GetList<activity>(activitySql);
            return lstactivity;
        }

        private List<package> GetListPackage()
        {
            string PackageSql = @"SELECT * FROM package";
            List<package> lstPackage = DBUtl.GetList<package>(PackageSql);
            return lstPackage;
        }
        private List<accessories> GetListAccessories()
        {
            string MaterialSql = @"SELECT * FROM accessories";
            List<accessories> lstMaterial = DBUtl.GetList<accessories>(MaterialSql);
            return lstMaterial;
        }


    }
}