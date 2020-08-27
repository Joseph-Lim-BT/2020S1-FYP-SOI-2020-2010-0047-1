using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmarterTomorrow.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel; 
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Routing.Constraints;
using Ubiety.Dns.Core;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using NPOI.SS.Formula.Functions;

namespace SmarterTomorrow.Controllers
{
    public class EquipmentController : Controller
    {
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult ListEquipment()
        {
            List<Equipment> list = DBUtl.GetList<Equipment>("SELECT * FROM Equipment ");
            return View(list);

        }

        [Authorize(Roles = "0,1,2,3")]
        public IActionResult ListAccessories()
        {
            List<accessories> list = DBUtl.GetList<accessories>("SELECT * FROM accessories");
            return View(list); 
        }

        
        public IActionResult AddAccessories()
        {
            ViewData["Material"] = GetListMaterial();
            return View(); 
        }

        [Authorize(Roles = "0,1,2,3")]
        [HttpPost]
        public IActionResult AddAccessories(accessories newAccessories)
        {
            if (!ModelState.IsValid)
            {
                
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("AddAccessories");
            }else
            {
                ViewData["Material"] = GetListMaterial();
                DateTime datetimenow = DateTime.Now; 
                string person = GetPerson();
                string AccessoryID = newAccessories.SERIAL_NO + "," + newAccessories.ELEMENT_MATERIAL_NO;
                string insert = @"INSERT INTO accessories (ACCESSORY_ID, ACCESSORY_NAME, ELEMENT_MATERIAL_NO, SERIAL_NO, STORAGE_LOCATION, STORAGE_BIN, QUANTITY, DTE_TIME_CR, DTE_TIME_LAST_MOD, CREATED_BY, MODIFIED_BY, BOX_LOT_NO) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7:yyyy-MM-dd hh:mm:ss}', '{8:yyyy-MM-dd hh:mm:ss}', '{9}', '{10}', '{11}')"; 

                if (DBUtl.ExecSQL(insert, AccessoryID, newAccessories.ACCESSORY_NAME, newAccessories.ELEMENT_MATERIAL_NO, newAccessories.SERIAL_NO, newAccessories.STORAGE_LOCATION, newAccessories.STORAGE_BIN, newAccessories.QUANTITY, datetimenow, datetimenow, person, person, newAccessories.BOX_LOT_NO) == 1)
                {
                    TempData["Message"] = "Accessory Added";
                    TempData["MsgType"] = "success"; 
                }else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger"; 
                }

                return RedirectToAction("ListAccessories"); 
            }
        }

        [HttpGet]
        public IActionResult EditAccessory(string id)
        {
            ViewData["Material"] = GetListMaterial();
            string accessorySQL = "SELECT * FROM ACCESSORIES WHERE ACCESSORY_ID = '{0}'";
            List<accessories> lstAccessory = DBUtl.GetList<accessories>(accessorySQL, id);

            if (lstAccessory.Count == 1)
            {
                return View(lstAccessory[0]);
            }
            else
            {
                TempData["Message"] = "Equipment not found";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListAccessories");
            }
        }

        [HttpPost]
        public IActionResult EditAccessory(accessories ac)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Material"] = GetListMaterial();
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("EditAccessory");
            }

            string person = GetPerson();

            string update = "UPDATE accessories SET ACCESSORY_NAME = '{1}', SERIAL_NO = '{2}', STORAGE_LOCATION = '{3}', STORAGE_BIN = '{4}', QUANTITY = {5}, DTE_TIME_LAST_MOD = '{6:yyyy-MM-dd}', MODIFIED_BY = '{7}', BOX_LOT_NO = '{8}' WHERE ACCESSORY_ID = '{0}'"; 

            if (DBUtl.ExecSQL(update, ac.ACCESSORY_ID, ac.ACCESSORY_NAME, ac.SERIAL_NO, ac.STORAGE_LOCATION, ac.STORAGE_BIN, ac.QUANTITY, ac.DTE_TIME_LAST_MOD, person, ac.BOX_LOT_NO) == 1)
            {
                TempData["Message"] = "Accessory updated";
                TempData["MsgType"] = "Success"; 
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";

            }
            return RedirectToAction("ListAccessories"); 


        }

        public IActionResult DeleteAccessory (string id)
        {
            string select = "SELECT *  FROM accessories WHERE ACCESSORY_ID = '{0}'";
            DataTable ds = DBUtl.GetTable(select, id); 
            if (ds.Rows.Count != 1)
            {
                TempData["Message"] = "Accessory record no longer exists.";
                TempData["MsgType"] = "warning";
            }
            else
            {
                string delete = "DELETE FROM accessories WHERE ACCESSORY_ID = '{0}'"; 
                int res = DBUtl.ExecSQL(delete, id);
                if (res == 1)
                {
                    TempData["Message"] = "Accessory Deleted";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
            }
            return RedirectToAction("ListAccessories");
        }
    


        [Authorize(Roles = "0,1,2,3")]
        [HttpGet]
        public IActionResult AddEquipment()
        {
            ViewData["Material"] = GetListMaterial();
            return View();
        }

        [Authorize(Roles = "0,1,2,3")]
        [HttpPost]
        public IActionResult AddEquipment(Equipment newEquipment)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Material"] = GetListMaterial();
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("AddEquipment");
            }
            else
            {
                string person = GetPerson();
                string equipmentID = newEquipment.SERIAL_NO + "," + newEquipment.ELEMENT_MATERIAL_NO;
                string insert = @"INSERT INTO EQUIPMENT(SERIAL_NO, EQUIPMENT_TYPE_ID, ELEMENT_MATERIAL_NO, STORAGE_LOCATION, STORAGE_BIN, BOX_LOT_NO, BUTT_NO, QUANTITY, DTE_TIME_CR, DTE_TIME_LAST_MOD, CREATED_BY, MODIFIED_BY, EQUIPMENT_NAME, EQUIPMENT_ID, STATUS) VALUES('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8:yyyy-MM-dd}', '{9:yyyy-MM-dd}', '{10}', '{11}', '{12}', '{13}', {14})";

                if (DBUtl.ExecSQL(insert, newEquipment.SERIAL_NO, newEquipment.EQUIPMENT_TYPE_ID, newEquipment.ELEMENT_MATERIAL_NO, newEquipment.STORAGE_LOCATION, newEquipment.STORAGE_BIN, newEquipment.BOX_LOT_NO, newEquipment.BUTT_NO, newEquipment.QUANTITY, newEquipment.DTE_TIME_CR, newEquipment.DTE_TIME_LAST_MOD, person, person, newEquipment.EQUIPMENT_NAME, equipmentID, 1) == 1)
                {
                    TempData["Message"] = "Equipment Added";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListEquipment");
            }

        }


       

        [Authorize(Roles = "0,1,2,3")]
        [HttpGet]
        public IActionResult EditEquipment(string id)
        {
            ViewData["Material"] = GetListMaterial();
            string equipmentSQL = @"SELECT * FROM Equipment WHERE Equipment_ID = '{0}'";
            List<Equipment> lstEquipment = DBUtl.GetList<Equipment>(equipmentSQL, id);

            // If the record is found, pass the model to the View
            if (lstEquipment.Count == 1)
            {
                return View(lstEquipment[0]);
            }
            else
            // Otherwise redirect to the list page
            {
                TempData["Message"] = "Equipment not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListEquipment");
            }
        }

        [Authorize(Roles = "0,1,2,3")]
        [HttpPost]
        public IActionResult EditEquipment(Equipment eq)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Material"] = GetListMaterial();
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("EditEquipment");
            }

            string person = GetPerson();

            string update = @"UPDATE EQUIPMENT SET SERIAL_NO='{1}', EQUIPMENT_TYPE_ID={2}, ELEMENT_MATERIAL_NO='{3}', STORAGE_LOCATION='{4}', STORAGE_BIN='{5}', BOX_LOT_NO='{6}', BUTT_NO='{7}', QUANTITY={8}, MODIFIED_BY='{9}', DTE_TIME_LAST_MOD='{10:yyyy-MM-dd}', EQUIPMENT_NAME = '{11}', STATUS = {12} WHERE EQUIPMENT_ID='{0}'";

            if (DBUtl.ExecSQL(update, eq.EQUIPMENT_ID, eq.SERIAL_NO, eq.EQUIPMENT_TYPE_ID, eq.ELEMENT_MATERIAL_NO, eq.STORAGE_LOCATION, eq.STORAGE_BIN, eq.BOX_LOT_NO, eq.BUTT_NO, eq.QUANTITY, person, eq.DTE_TIME_LAST_MOD, eq.EQUIPMENT_NAME, eq.STATUS) == 1)
            {
                TempData["Message"] = "Equipment Updated";
                TempData["MsgType"] = "Success";
            } else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }
            return RedirectToAction("ListEquipment");
        }

        [Authorize(Roles = "0,1")]
        public IActionResult DeleteEquipment(string id)
        {
            string select = @"SELECT * FROM Equipment 
                              WHERE EQUIPMENT_ID='{0}'";
            DataTable ds = DBUtl.GetTable(select, id);
            if (ds.Rows.Count != 1)
            {
                TempData["Message"] = "Equipment record no longer exists.";
                TempData["MsgType"] = "warning";
            }
            else
            {
                string delete = "DELETE FROM Equipment WHERE EQUIPMENT_ID='{0}'";
                int res = DBUtl.ExecSQL(delete, id);
                if (res == 1)
                {
                    TempData["Message"] = "Equipment Deleted";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
            }
            return RedirectToAction("ListEquipment");

        }


        private string GetPerson()
        {
            string personSql = @"SELECT *
                                FROM usr WHERE NRIC='" + User.Identity.Name + "'";
            List<User> assigned = DBUtl.GetList<User>(personSql);
            string person = assigned[0].FULL_NAME;
            return person;
        }

        [HttpGet]
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult AddMaterial()
        {
            //ViewData["Material"] = GetListMaterial();
            return View();

        }

        [HttpPost]
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult AddMaterial(material newMaterial)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("AddMaterial");
            }
            else
            {
                DateTime dateTimeNow = DateTime.Now;
                string person = GetPerson();
                string insert =
                   @"INSERT INTO Material(ELEMENT_MATERIAL_NO,ELEMENT_MATERIAL_DESC,GROUP_NSN_NO,GROUP_NSN_DESC,MATERIAL_TYPE,MATERIAL_UNIT,DTE_TIME_CR,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY) 
                 VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6:yyyy-MM-dd hh:mm:ss}','{7:yyyy-MM-dd hh:mm:ss}','{8}', '{9}')";


                int result = DBUtl.ExecSQL(insert, newMaterial.ELEMENT_MATERIAL_NO, newMaterial.ELEMENT_MATERIAL_DESC, newMaterial.GROUP_NSN_NO, newMaterial.GROUP_NSN_DESC, newMaterial.MATERIAL_TYPE, newMaterial.MATERIAL_UNIT, dateTimeNow, dateTimeNow,
                    person, person);
                if (result == 1)
                {
                    TempData["Message"] = "Material Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = "You cannot submit a record that has the same element material number as an existing record!";
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListMaterials");
            }
        }

        [HttpGet]
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult EditMaterial(string id)
        {

            // Get the record from the database using the id
            string MaterialSql = @"SELECT * FROM Material WHERE ELEMENT_MATERIAL_NO='{0}'";
            List<material> lstMaterial = DBUtl.GetList<material>(MaterialSql, id);

            // If the record is found, pass the model to the View
            if (lstMaterial.Count == 1)
            {
                return View(lstMaterial[0]);
            }
            else
            // Otherwise redirect to the material list page
            {
                TempData["Message"] = "Material not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListMaterials");
            }

        }


        [HttpPost]
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult EditMaterial(material newMaterial)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Please fill up all the blanks";
                ViewData["MsgTye"] = "warning";
                return View();
            }

            DateTime dateTimeNow = DateTime.Now;
            string person = GetPerson();
            // Write the SQL statement
            string update = "UPDATE Material SET ELEMENT_MATERIAL_DESC='{1}',GROUP_NSN_NO='{2}',GROUP_NSN_DESC='{3}',MATERIAL_TYPE ='{4}',MATERIAL_UNIT='{5}',DTE_TIME_LAST_MOD='{6:yyyy-MM-dd hh:mm:ss}',MODIFIED_BY ='{7}' WHERE ELEMENT_MATERIAL_NO='{0}'";

            // Check the result and branch
            // If successful set a TempData success Message and MsgType
            // If unsuccessful, set a TempData message that equals the DBUtl error message

            if (DBUtl.ExecSQL(update, newMaterial.ELEMENT_MATERIAL_NO, newMaterial.ELEMENT_MATERIAL_DESC, newMaterial.GROUP_NSN_NO, newMaterial.GROUP_NSN_DESC, newMaterial.MATERIAL_TYPE, newMaterial.MATERIAL_UNIT, dateTimeNow, person) == 1)
            {
                TempData["Message"] = "Successfully updated Material";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = "You cannot submit a record that has the same element material number as an existing record!";
                TempData["MsgType"] = "danger";
            }

            // Call the action ListMaterials to show the result of the update
            return RedirectToAction("ListMaterials");
        }

        [Authorize(Roles = "0,1")]
        public IActionResult DeleteMaterial(string id)
        {

            string delete = "DELETE FROM Material WHERE ELEMENT_MATERIAL_NO='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "Material Record Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = "Please delete any related records before deleting this record!";
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("ListMaterials");
        }

        [Authorize(Roles = "0,1,2")]
        [HttpGet]
        public IActionResult AddEquipmentAssigned()
        {
            List<Equipment> remainingEq = DBUtl.GetList<Equipment>(@"SELECT * FROM equipment WHERE QUANTITY > 0 AND STATUS=1");
            ViewData["PackageID"] = GetListPackage();
            ViewData["Assign"] = GetListAssigned();
            ViewData["EquipID"] = remainingEq;
            ViewData["ActivityID"] = GetActivityID();
            return View();

        }

        [HttpPost]
        [Authorize(Roles = "0,1,2")]
        public IActionResult AddEquipmentAssigned(assignment newEquipment)
        {

            if (!ModelState.IsValid)
            {
                List<Equipment> remainingEq = DBUtl.GetList<Equipment>(@"SELECT * FROM equipment WHERE QUANTITY > 0 AND STATUS=1");
                ViewData["PackageID"] = GetListPackage();
                ViewData["Assign"] = GetListAssigned();
                ViewData["EquipID"] = remainingEq;
                ViewData["ActivityID"] = GetActivityID();
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "warning";
                return View("AddEquipmentAssigned");
            }
            else
            {
                DateTime dateTimeNow = DateTime.Now;

                string person = GetPerson();

                string insert;
                int result;
                int result2;
                string update;

                    insert =
    @"INSERT INTO assignment(
                EQUIPMENT_ID,NRIC,
                DTE_TIME_CR,DTE_TIME_LAST_MOD,
                CREATED_BY,MODIFIED_BY,ACTIVITY_ID) 
                                 VALUES('{0}','{1}',
                '{2:yyyy-MM-dd hh:mm:ss}','{3:yyyy-MM-dd hh:mm:ss}','{4}',
                '{5}','{6}')";
                    update = @"UPDATE equipment SET STATUS=2 WHERE EQUIPMENT_ID='" + newEquipment.EQUIPMENT_ID + "'";
                    result = DBUtl.ExecSQL(insert, newEquipment.EQUIPMENT_ID, newEquipment.NRIC, dateTimeNow, dateTimeNow,
person, person, newEquipment.ACTIVITY_ID);
                    result2 = DBUtl.ExecSQL(update);

                


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
                return RedirectToAction("ListEquipmentAssigned");
            }
        }


        [HttpGet]
        [Authorize(Roles = "0,1,2")]
        public IActionResult EditEquipmentAssigned(int id)
        {

            // Get the record from the database using the id

            string assignedSql = @"SELECT * 
                               FROM assignment
                              WHERE ASSIGNMENT_ID= '{0}'";
            List<assignment> Iassigned = DBUtl.GetList<assignment>(assignedSql, id);

            // If the record is found, pass the model to the View
            if (Iassigned.Count == 1)
            {
                ViewData["ActivityID"] = GetActivityID();
                ViewData["Assign"] = GetListAssigned();
                return View(Iassigned[0]);
            }
            else
            // Otherwise redirect to the Equipment Assigned list page
            {
                TempData["Message"] = "Equipment Assigned not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListEquipmentAssigned");
            }

        }


        [HttpPost]
        [Authorize(Roles = "0,1,2")]
        public IActionResult EditEquipmentAssigned(assignment assigned)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Please fill up all the blanks";
                ViewData["MsgTye"] = "warning";
                return View("EditEquipmentAssigned");
            }
            else
            {
                DateTime dateTimeNow = DateTime.Now;
                string person = GetPerson();
                // Write the SQL statement
                string update = @"UPDATE assignment SET NRIC='{1}',DTE_TIME_LAST_MOD ='{2:yyyy-MM-dd hh:mm:ss}',MODIFIED_BY='{3}',ACTIVITY_ID={4} WHERE ASSIGNMENT_ID='{0}'";

                // Check the result and branch
                // If successful set a TempData success Message and MsgType
                // If unsuccessful, set a TempData message that equals the DBUtl error message

                if (DBUtl.ExecSQL(update, assigned.ASSIGNMENT_ID, assigned.NRIC, dateTimeNow, person, assigned.ACTIVITY_ID) == 1)
                {
                    TempData["Message"] = "Record Updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }

                // Call the action ListEquipmentAssigned to show the result of the update
                return RedirectToAction("ListEquipmentAssigned");
            }
        }


        [Authorize(Roles = "0,1,2")]
        public IActionResult ListEquipmentAssigned()
        {
            List<assignment> list = DBUtl.GetList<assignment>("SELECT * FROM assignment");
            ViewData["equipment"] = DBUtl.GetList<Equipment>("SELECT * FROM equipment");
            ViewData["package"] = DBUtl.GetList<package>("SELECT * FROM package");
            return View(list);
        }

        [Authorize(Roles = "0,1,2")]
        public IActionResult Delete(int id)
        {
            string delete;
            string update;
            string update2;
            List<assignment> list = DBUtl.GetList<assignment>("SELECT * FROM assignment");
            List<package> listpkg = DBUtl.GetList<package>("SELECT * FROM accessories");
            List<Equipment> listEq = DBUtl.GetList<Equipment>("SELECT * FROM equipment");
            foreach (assignment assi in list)
            {
                if(assi.ASSIGNMENT_ID == id)
                {
                    if (assi.EQUIPMENT_ID != null)
                    {
                        delete = "DELETE FROM assignment WHERE EQUIPMENT_ID='{0}'";
                        update = "UPDATE equipment SET STATUS=1 WHERE EQUIPMENT_ID='" + assi.EQUIPMENT_ID + "'";

                        int res = DBUtl.ExecSQL(delete, assi.EQUIPMENT_ID);
                        int res2 = DBUtl.ExecSQL(update);
                        if (res == 1 && res2 == 1)
                        {
                            TempData["Message"] = "Equipment Un-Assigned";
                            TempData["MsgType"] = "success";
                        }
                        else
                        {
                            TempData["Message"] = DBUtl.DB_Message;
                            TempData["MsgType"] = "danger";
                        }

                        return RedirectToAction("ListEquipmentAssigned");
                    } else
                    {
                        delete = "DELETE FROM assignment WHERE PACKAGE_ID='"+ assi.PACKAGE_ID +"' AND ASSIGNMENT_ID={0}";
                        update2 = "";
                        update = "";
                        foreach (package pk in listpkg)
                        {
                            if (pk.ACCESSORY_ID.Substring(0,5).Equals(assi.PACKAGE_ID.Substring(7,5)))
                            {
                                update2 = "UPDATE accessories SET QUANTITY=QUANTITY+1 WHERE ACCESSORY_ID LIKE '%" + assi.PACKAGE_ID.Substring(7, 5) + "%'";
                                break;
                            }
                        }
                        foreach (Equipment ee in listEq)
                        {
                            if (ee.EQUIPMENT_ID.Substring(0,6).Equals(assi.PACKAGE_ID.Substring(0,6)))
                            {
                                List<Equipment> lstEq2 = DBUtl.GetList<Equipment>(@"SELECT * FROM equipment WHERE STATUS=2 AND EQUIPMENT_NAME='" + ee.EQUIPMENT_NAME + "'");
                                if (lstEq2.Count > 0)
                                {
                                    update = @"UPDATE equipment SET STATUS=1 WHERE BUTT_NO='" + assi.CREATED_BY.Substring(Math.Max(0,assi.CREATED_BY.Length-5),4) + "' AND EQUIPMENT_NAME='" + ee.EQUIPMENT_NAME + "'";
                                }
                            }
                        }
                        
                        int res = DBUtl.ExecSQL(delete, id);
                        int res2 = DBUtl.ExecSQL(update);
                        int res3 = DBUtl.ExecSQL(update2);
                        if (res == 1 && res2 == 1 && res3 == 1)
                        {
                            TempData["Message"] = "Equipment Un-Assigned";
                            TempData["MsgType"] = "success";
                        }
                        else
                        {
                            TempData["Message"] = DBUtl.DB_Message;
                            TempData["MsgType"] = "danger";
                        }

                        return RedirectToAction("ListEquipmentAssigned");
                    }
                }
            }

            return RedirectToAction("ListEquipmentAssigned");
        }
        private object GetListAssigned()
        {
            // Get a list of all usr from the database
            string AssSql = @"SELECT * FROM usr ORDER BY FULL_NAME";
            List<User> lstAss = DBUtl.GetList<User>(AssSql);
            return lstAss;
        }
 
        public IActionResult ListMaterials()
        {
            List<material> materialList = DBUtl.GetList<material>(
                  @"SELECT * FROM Material");
            return View(materialList);
        }

        private List<material> GetListMaterial()
        {
            string MaterialSql = @"SELECT ELEMENT_MATERIAL_NO,ELEMENT_MATERIAL_DESC FROM Material";
            List<material> lstMaterial = DBUtl.GetList<material>(MaterialSql);
            return lstMaterial;
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


