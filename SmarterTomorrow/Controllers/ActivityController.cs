using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SmarterTomorrow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FYP.Controllers
{
    public class ActivityController : Controller
    {
        //start here
        //Activity List
        [HttpGet]
        public IActionResult ActivityList()
        {
            List<activity> activitytable = DBUtl.GetList<activity>(
                  @"SELECT * FROM Activity");
            return View(activitytable);
        }
        [HttpGet]
        public IActionResult ActivityDetail(int id)
        {
            //find all those records linked to that activity
            List<assignment> activitytable = DBUtl.GetList<assignment>(
      @"SELECT * FROM assignment WHERE ACTIVITY_ID='" + id + "'");
            return View(activitytable);
        }

        [HttpGet]
        public IActionResult AddActivity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddActivity(activity newACT)
        {

            if (!ModelState.IsValid)
            {
                ViewData["ActivityID"] = GetListActivityID();
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("AddActivity");
            }
            else
            {
                DateTime dateTimeNow = DateTime.Now;
                string person = GetPerson();
                string insert =
                   @"INSERT INTO Activity(BRIGADE,TYPE,ACTIVITY_DESC,STATUS,DTE_TIME_CR,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY,ACTIVITY_START,ACTIVITY_END) 
                                 VALUES('{0}','{1}','{2}','{3}','{4:yyyy-MM-dd hh:mm:ss}','{5:yyyy-MM-dd hh:mm:ss}','{6}','{7}','{8:yyyy-MM-dd hh:mm:ss}','{9:yyyy-MM-dd hh:mm:ss}')";


                int result = DBUtl.ExecSQL(insert, newACT.BRIGADE, newACT.TYPE, newACT.ACTIVITY_DESC, newACT.STATUS, dateTimeNow, dateTimeNow, person, person, newACT.ACTIVITY_START, newACT.ACTIVITY_END);

                if (result == 1)
                {
                    TempData["Message"] = "Activity Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ActivityList");
            }
        }

        [HttpGet]
        public IActionResult UpdateActivity(string id)
        {
            // Get the record from the database using the id
            string actSql = @"SELECT * FROM activity WHERE ACTIVITY_ID= '{0}'";
            List<activity> lstActivity = DBUtl.GetList<activity>(actSql, id);

            // If the record is found, pass the model to the View
            if (lstActivity.Count == 1)
            {
                return View(lstActivity[0]);
            }
            else
            // Otherwise redirect to the Activity list page
            {
                TempData["Message"] = "Activity not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ActivityList");
            }
        }

        [HttpPost]
        public IActionResult UpdateActivity(activity act)
        {

            // Check the state of the model 
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("UpdateActivity");
            }
            else
            {
                // Write the SQL statement
                DateTime currentDateTime = DateTime.Now;
                string person = GetPerson();
                string update = @"UPDATE Activity SET BRIGADE='{1}', TYPE='{2}',ACTIVITY_DESC='{3}',STATUS='{4}',DTE_TIME_LAST_MOD='{5:yyyy-MM-dd hh:mm:ss}', MODIFIED_BY ='{6}',ACTIVITY_START='{7:yyyy-MM-dd hh:mm:ss}',ACTIVITY_END='{8:yyyy-MM-dd hh:mm:ss}' WHERE ACTIVITY_ID='{0}'";

                // Execute the SQL statement in a secure manner
                // Check the result and branch
                DateTime startDate = Convert.ToDateTime(act.ACTIVITY_START);
                DateTime endDate = Convert.ToDateTime(act.ACTIVITY_END);
                if (DBUtl.ExecSQL(update, act.ACTIVITY_ID, act.BRIGADE, act.TYPE, act.ACTIVITY_DESC, act.STATUS, currentDateTime, person, startDate, endDate) == 1)
                {
                    // If successful set a TempData success Message and MsgType
                    TempData["Message"] = "Activity Updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    // If unsuccessful, set a TempData message that equals the DBUtl error message
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                // Call the action ActivityList to show the result of the update
                return RedirectToAction("ActivityList");
            }
        }

        public IActionResult DeleteActivity(string id)
        {
            string delete = "DELETE FROM Activity WHERE ACTIVITY_ID='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "Activity Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = "Please delete any related records before deleting this record";
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("ActivityList");
        }




        [Authorize(Roles = "0")]
        public IActionResult ListTransaction()
        {
            List<activity_trans> transList = DBUtl.GetList<activity_trans>(
      @"SELECT * FROM Activity_trans");
            return View(transList);
        }

        [HttpGet]
        [Authorize(Roles = "0")]
        public IActionResult AddTransaction()
        {
            ViewData["TransID"] = GetListTransID();
            ViewData["ActivityID"] = GetListActivityID();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public IActionResult AddTransaction(activity_trans newTrans)
        {
            string uuid = GenerateUUID();
            ViewData["TransID"] = GetListTransID();
            if (!ModelState.IsValid)
            {
                ViewData["TransID"] = GetListTransID();
                ViewData["ActivityID"] = GetListActivityID();
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("AddTransaction");
            }
            else
            {
                DateTime dateTimeNow = DateTime.Now;
                string person = GetPerson();
                string insert =
                   @"INSERT INTO Activity_trans(TRANS_ID,ACTIVITY_ID,NRIC,TYPE,MACHINE_NAME,DTE_TIME_CR,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY) 
                                 VALUES('{0}',{1},'{2}','{3}','{4}','{5:yyyy-MM-dd hh:mm:ss}','{6:yyyy-MM-dd hh:mm:ss}','{7}','{8}')";

                int result = DBUtl.ExecSQL(insert, uuid, newTrans.ACTIVITY_ID, newTrans.NRIC, newTrans.TYPE, newTrans.MACHINE_NAME, dateTimeNow, dateTimeNow, person, person);

                if (result == 1)
                {
                    string insert1 =
                   @"INSERT INTO Activity_trans_dtl(TRANS_ID,FULL_DESC,SHORT_DESC,STORAGE_LOCATION,STORAGE_BIN,QUANTITY) 
                                 VALUES('{0}','{1}','{2}','{3}','{4}',{5})";
                    if (newTrans.TYPE.Equals("RG"))
                    {
                        DBUtl.ExecSQL(insert1, uuid, "Registration Successful", "Success", "Warehouse A", "Armskote 1", 1);
                    }
                    else if (newTrans.TYPE.Equals("IS"))
                    {
                        DBUtl.ExecSQL(insert1, uuid, "Issue Successful", "Success", "Warehouse B", "Armskote 2", 1);
                    }
                    else if (newTrans.TYPE.Equals("RE"))
                    {
                        DBUtl.ExecSQL(insert1, uuid, "Return Successful", "Success", "Warehouse A", "Armskote 1", 1);
                    }
                    else if (newTrans.TYPE.Equals("CA"))
                    {
                        DBUtl.ExecSQL(insert1, uuid, "Change Appointment Successful", "Success", "Warehouse A", "Armskote 2", 1);
                    }
                    else
                    {
                        DBUtl.ExecSQL(insert1, uuid, "Change Detail Successful", "Success", "Warehouse A", "Armskote 1", 1);
                    }
                    TempData["Message"] = "Transaction ID " + uuid + " Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListTransaction");
            }
        }

        [HttpGet]
        [Authorize(Roles = "0")]
        public IActionResult UpdateTransaction(string id)
        {
            // Get the record from the database using the id
            string transSql = @"SELECT * FROM Activity_trans WHERE TRANS_ID= '{0}'";
            List<activity_trans> lstTrans = DBUtl.GetList<activity_trans>(transSql, id);

            // If the record is found, pass the model to the View
            if (lstTrans.Count == 1)
            {
                return View(lstTrans[0]);
            }
            else
            // Otherwise redirect to the ListTransaction
            {
                TempData["Message"] = "Appointment record not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListTransaction");
            }
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public IActionResult UpdateTransaction(activity_trans trans)
        {

            // Check the state of the model ((Ref Week 9). 
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("UpdateTransaction");
            }
            else
            {
                // Write the SQL statement
                DateTime currentDateTime = DateTime.Now;
                string person = GetPerson();
                string update = @"UPDATE Activity_trans SET ACTIVITY_ID={1},NRIC='{2}',TYPE='{3}',MACHINE_NAME='{4}',DTE_TIME_LAST_MOD='{5:yyyy-MM-dd hh:mm:ss}', MODIFIED_BY ='{6}' WHERE TRANS_ID='{0}'";

                // Execute the SQL statement in a secure manner
                // Check the result and branch
                if (DBUtl.ExecSQL(update, trans.TRANS_ID, trans.ACTIVITY_ID, trans.NRIC, trans.TYPE, trans.MACHINE_NAME, currentDateTime, person) == 1)
                {
                    // If successful set a TempData success Message and MsgType
                    TempData["Message"] = "Transaction Updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    // If unsuccessful, set a TempData message that equals the DBUtl error message
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                // Call the action ListTransaction to show the result of the update
                return RedirectToAction("ListTransaction");
            }
        }
        [Authorize(Roles = "0")]
        public IActionResult DeleteTransaction(string id)
        {
            int status = 1;
            string delete1 = "DELETE FROM Activity_trans_dtl WHERE TRANS_ID='{0}'";
            status--;
            DBUtl.ExecSQL(delete1, id);
            string delete = "DELETE FROM Activity_trans WHERE TRANS_ID='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "Transaction Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("ListTransaction");
        }



        //Transaction Detail
        [Authorize(Roles = "0")]
        public IActionResult ListTransactionDTL(string id)
        {
            List<activity_trans_dtl> transdtlList = DBUtl.GetList<activity_trans_dtl>(@"SELECT * FROM Activity_trans_dtl WHERE TRANS_ID='" + id + "'");
            return View(transdtlList);
        }
        [Authorize(Roles = "0")]
        [HttpGet]
        public IActionResult AddTransactionDetail()
        {
            ViewData["TransID"] = GetListTransID();
            return View();
        }
        [Authorize(Roles = "0")]
        [HttpPost]
        public IActionResult AddTransactionDetail(activity_trans_dtl newTransDetail)
        {
            string uuid = GenerateUUID();

            if (!ModelState.IsValid)
            {
                ViewData["TransID"] = GetListTransID();
                ViewData["RecordID"] = GetListRecordID();
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("AddTransactionDetail");
            }
            else
            {
                DateTime dateTimeNow = DateTime.Now;
                string person = GetPerson();
                string insert =
                   @"INSERT INTO Activity_trans_dtl(TRANS_ID,ELEMENT_MATERIAL_NO,ELEMENT_MATERIAL_DESC,FULL_DESC,SHORT_DESC,SERIAL_NO,
BOM_NO,STORAGE_LOCATION,STORAGE_BIN,BOX_LOT_NO,QUANTITY) 
                                 VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10})";


                int result = DBUtl.ExecSQL(insert, newTransDetail.TRANS_ID, newTransDetail.ELEMENT_MATERIAL_NO, newTransDetail.ELEMENT_MATERIAL_DESC,
                    newTransDetail.FULL_DESC, newTransDetail.SHORT_DESC, newTransDetail.SERIAL_NO, newTransDetail.BOM_NO, newTransDetail.STORAGE_LOCATION,
                    newTransDetail.STORAGE_BIN, newTransDetail.BOX_LOT_NO, newTransDetail.QUANTITY);

                if (result == 1)
                {
                    TempData["Message"] = "Transaction Detail Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListTransactionDTL");
            }
        }
        [Authorize(Roles = "0")]
        [HttpGet]
        public IActionResult UpdateTransactionDetail(string id)
        {
            // Get the record from the database using the id
            string transDtlSql = @"SELECT * FROM Activity_trans_dtl WHERE RECORD_ID= '{0}'";
            List<activity_trans_dtl> lstTransDtl = DBUtl.GetList<activity_trans_dtl>(transDtlSql, id);

            // If the record is found, pass the model to the View
            if (lstTransDtl.Count == 1)
            {
                return View(lstTransDtl[0]);
            }
            else
            // Otherwise redirect to the movie list page
            {
                TempData["Message"] = "Appointment record not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListTransactionDTL");
            }
        }
        [Authorize(Roles = "0")]
        [HttpPost]
        public IActionResult UpdateTransactionDetail(activity_trans_dtl dtl)
        {
            string uuid = GenerateUUID();
            // Check the state of the model ((Ref Week 9). 
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("UpdateTransactionDetail");
            }
            else
            {
                // Write the SQL statement
                DateTime currentDateTime = DateTime.Now;
                string person = GetPerson();
                string update = @"UPDATE Activity_trans_dtl SET TRANS_ID='{0}', ELEMENT_MATERIAL_NO='{1}',ELEMENT_MATERIAL_DESC='{2}',FULL_DESC='{3}',SHORT_DESC='{4}',
SERIAL_NO='{5}',BOM_NO='{6}',STORAGE_LOCATION='{7}',STORAGE_BIN='{8}',BOX_LOT_NO='{9}',QUANTITY='{10}' WHERE RECORD_ID='{11}'";

                // Execute the SQL statement in a secure manner
                // Check the result and branch
                if (DBUtl.ExecSQL(update, dtl.TRANS_ID, dtl.ELEMENT_MATERIAL_NO, dtl.ELEMENT_MATERIAL_DESC, dtl.FULL_DESC, dtl.SHORT_DESC, dtl.SERIAL_NO, dtl.BOM_NO,
                    dtl.STORAGE_LOCATION, dtl.STORAGE_BIN, dtl.BOX_LOT_NO, dtl.QUANTITY, dtl.RECORD_ID) == 1)
                {
                    string insert1 = @"INSERT INTO Activity_trans(TRANS_ID,ACTIVITY_ID,NRIC,TYPE,MACHINE_NAME,DTE_TIME_CR,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY)
VALUES('{0}',1,'{1}','CD','Machine 1','{2:yyyy-MM-dd hh:mm:ss}','{3:yyyy-MM-dd hh:mm:ss}','{4}','{5}')";
                    DBUtl.ExecSQL(insert1, uuid, User.Identity.Name, currentDateTime, currentDateTime, person, person);
                    // If successful set a TempData success Message and MsgType
                    TempData["Message"] = "Transaction Updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    // If unsuccessful, set a TempData message that equals the DBUtl error message
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                // Call the action ListMovies to show the result of the update
                return RedirectToAction("ListTransactionDTL");
            }
        }

        [Authorize(Roles = "0")]
        public IActionResult DeleteTransactionDTL(string id)
        {
            string delete = "DELETE FROM Activity_trans_dtl WHERE RECORD_ID='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "Transaction Detail Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("ListTransactionDTL");
        }


        [HttpGet]
        [Authorize(Roles = "0")]
        public IActionResult AddExceptionDTL()
        {

            List<activity> exceptionAvail = DBUtl.GetList<activity>("SELECT * FROM activity WHERE TYPE='M'");
            ViewData["id"] = exceptionAvail;
            List<assignment> tweaponList = new List<assignment>();
            foreach (activity a in exceptionAvail)
            {
                List<assignment> weaponList = DBUtl.GetList<assignment>("SELECT * FROM assignment WHERE activity_id='" + a.ACTIVITY_ID + "'");
                foreach (assignment asg in weaponList)
                {
                    tweaponList.Add(asg);
                }
            }

            ViewData["Weapon"] = tweaponList;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public IActionResult AddExceptionDTL(activity_exception_dtl exceptionDtl)
        {

            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {

                string insert =
                   @"INSERT INTO activity_exception_dtl(ACTIVITY_ID,EQUIPMENT_ID,QUANTITY,STATUS,REMARKS) 
                                 VALUES({0},'{1}',{2},'{3}','{4}')";


                int result = DBUtl.ExecSQL(insert, exceptionDtl.ACTIVITY_ID, exceptionDtl.EQUIPMENT_ID, exceptionDtl.QUANTITY, exceptionDtl.STATUS, exceptionDtl.REMARKS);

                if (result == 1)
                {
                    TempData["Message"] = "Exception Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                return RedirectToAction("ListExceptionDTL");
            }
        }

        [HttpGet]
        [Authorize(Roles = "0")]
        public IActionResult UpdateExceptionDTL(int id)
        {

            // Get the record from the database using the id
            ViewData["record"] = id;

            string exceptionSql = @"SELECT * FROM activity_exception_dtl WHERE RECORD_ID={0}";
            List<activity_exception_dtl> lstException = DBUtl.GetList<activity_exception_dtl>(exceptionSql, id);

            // If the record is found, pass the model to the View
            if (lstException.Count == 1)
            {
                List<activity> exceptionAvail = DBUtl.GetList<activity>("SELECT * FROM activity WHERE TYPE='M'");
                ViewData["id"] = exceptionAvail;
                List<assignment> tweaponList = new List<assignment>();
                foreach (activity a in exceptionAvail)
                {
                    List<assignment> weaponList = DBUtl.GetList<assignment>("SELECT * FROM assignment WHERE activity_id='" + a.ACTIVITY_ID + "'");
                    foreach (assignment asg in weaponList)
                    {
                        tweaponList.Add(asg);
                    }
                }

                ViewData["Weapon"] = tweaponList;
                return View(lstException[0]);
            }
            else
            {
                TempData["Message"] = "Exception Recprd not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("ListExceptionDTL");
            }

        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public IActionResult UpdateExceptionDTL(activity_exception_dtl exceptionDTL)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Please fill up all the blanks";
                ViewData["MsgTye"] = "warning";
                return View();
            }

            string update = "UPDATE activity_exception_dtl SET EQUIPMENT_ID='{1}',STATUS='{2}',QUANTITY ={3},REMARKS='{4}',ACTIVITY_ID={5} WHERE RECORD_ID={0}";

            // Check the result and branch
            // If successful set a TempData success Message and MsgType
            // If unsuccessful, set a TempData message that equals the DBUtl error message

            if (DBUtl.ExecSQL(update, exceptionDTL.RECORD_ID, exceptionDTL.EQUIPMENT_ID, exceptionDTL.STATUS, exceptionDTL.QUANTITY, exceptionDTL.REMARKS, exceptionDTL.ACTIVITY_ID) == 1)
            {
                TempData["Message"] = "Successfully updated exception record";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }


            // Call the action ListMovies to show the result of the update
            return RedirectToAction("ListExceptionDTL");
        }
        [Authorize(Roles = "0,1")]
        public IActionResult DeleteExceptionDTL(string id)
        {
            string select = @"SELECT * FROM activity_exception_dtl 
                              WHERE RECORD_ID='{0}'";
            DataTable ds = DBUtl.GetTable(select, id);
            if (ds.Rows.Count != 1)
            {
                TempData["Message"] = "Exception record no longer exists.";
                TempData["MsgType"] = "warning";
            }
            else
            {
                string delete = "DELETE FROM activity_exception_dtl WHERE RECORD_ID='{0}'";
                int res = DBUtl.ExecSQL(delete, id);
                if (res == 1)
                {
                    TempData["Message"] = "Exception record Deleted";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
            }
            return RedirectToAction("ListExceptionDTL");
        }


        //Exception Detail
        [Authorize(Roles = "0")]
        public IActionResult ListExceptionDTL(string id)
        {
            List<activity_exception_dtl> excdtlList = DBUtl.GetList<activity_exception_dtl>(
      @"SELECT * FROM Activity_exception_dtl ");
            return View(excdtlList);
        }

        [Authorize(Roles = "0")]
        public IActionResult DeleteExceptionDetail(string id)
        {
            string delete = "DELETE FROM Activity_exception_dtl WHERE RECORD_ID='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "Exception Detail Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("ListExceptionDTL");
        }



        //start here
        private string GetPerson()
        {
            string personSql = @"SELECT *
                                FROM usr WHERE NRIC='" + User.Identity.Name + "'";
            List<User> assigned = DBUtl.GetList<User>(personSql);
            string person = assigned[0].FULL_NAME;
            return person;
        }

        private List<activity> GetListActivityID()
        {
            string ActivityIDSql = @"SELECT ACTIVITY_ID FROM Activity";
            List<activity> lstActivityID = DBUtl.GetList<activity>(ActivityIDSql);
            return lstActivityID;
        }



        private List<activity_trans> GetListTransID()
        {
            string TransIDSql = @"SELECT TRANS_ID FROM Activity_trans";
            List<activity_trans> lstTransID = DBUtl.GetList<activity_trans>(TransIDSql);
            return lstTransID;
        }

        private List<activity_trans_dtl> GetListRecordID()
        {
            string TransDtlIDSql = @"SELECT RECORD_ID FROM Activity_trans";
            List<activity_trans_dtl> lstTransDtlID = DBUtl.GetList<activity_trans_dtl>(TransDtlIDSql);
            return lstTransDtlID;
        }

        private string GenerateUUID()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[22];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString.ToString();
        }



    }
}