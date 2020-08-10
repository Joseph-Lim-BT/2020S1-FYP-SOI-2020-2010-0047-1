using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmarterTomorrow.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Http;
using System.Data;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FYP.Controllers
{
    public class LoanController : Controller
    {


        [Authorize]
        public IActionResult Index()
        {
            //add code to delete all empty voucher ids
            string update = "UPDATE usr SET CURRENT_PG=null WHERE NRIC='" + User.Identity.Name + "'";
            DBUtl.ExecSQL(update);
            List<Loan_in_dtl> totalEq = DBUtl.GetList<Loan_in_dtl>("SELECT * FROM loan_in_dtl");
            int rowCount = totalEq.Count();
            HttpContext.Session.SetInt32("no", rowCount);
            return View();

        }

        public IActionResult BufferPage()
        {
            string voucher = GenerateVoucher();
            string person = GetPerson();
            string insert = @"INSERT INTO loan_in(NRIC,DTE_TIME_CR,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY,VOUCHER_ID,EXPECTED_RETURN_DTE) 
VALUES('{0}','{1:yyyy-MM-dd hh:mm:ss}','{2:yyyy-MM-dd hh:mm:ss}','{3}','{4}','{5}','{6:yyyy-MM-dd hh:mm:ss}')";
            DBUtl.ExecSQL(insert, User.Identity.Name, DateTime.Now, DateTime.Now, person, person, voucher,DateTime.Now);
            string update = "UPDATE usr SET CURRENT_PG='loan' WHERE NRIC='" + User.Identity.Name + "'";
            DBUtl.ExecSQL(update);
            
            return RedirectToAction("EquipmentAvailable");

        }

        public IActionResult EquipmentAvailable()
        {

            List<Equipment> equipmentAvailable = new List<Equipment>();
            List<assignment> equipmentAssigned = DBUtl.GetList<assignment>(
@"SELECT * FROM assignment WHERE NRIC='" + User.Identity.Name + "'");
            foreach (assignment en in equipmentAssigned)
            {
                foreach (Equipment eq in GetListEquipment())
                {
                    if (eq.EQUIPMENT_ID.Equals(en.EQUIPMENT_ID) && eq.QUANTITY > 0)
                    {
                        equipmentAvailable.Add(eq);
                    }

                }
            }
            //dec
            List<Loan_in> loanList = DBUtl.GetList<Loan_in>("SELECT * FROM loan_in");
            var model = loanList.OrderByDescending(s => s.DTE_TIME_CR).First();
            List<Loan_in_dtl> latestAddition = DBUtl.GetList<Loan_in_dtl>("SELECT * FROM loan_in_dtl WHERE VOUCHER_ID='" + model.VOUCHER_ID + "'");




            ViewData["scanned"] = latestAddition;
            return View(equipmentAvailable);
        }



        public IActionResult LoanEquipment(string id)
        {
            List<Loan_in> loanList = DBUtl.GetList<Loan_in>("SELECT * FROM loan_in");
            var model = loanList.OrderByDescending(s => s.DTE_TIME_CR).First();
            DateTime? dateNull = null;
            //string uuid = GenerateUUID();

            string insert = @"INSERT INTO loan_in_dtl(VOUCHER_ID,QUANTITY,ISSUE_DTE,RETURN_DTE,EQUIPMENT_ID) 
                    VALUES('{0}',{1},'{2:yyyy-MM-dd hh:mm:ss}','{3:yyyy-MM-dd hh:mm:ss}','{4}')";

            int res = DBUtl.ExecSQL(insert, model.VOUCHER_ID, 1, DateTime.Now, dateNull, id);
            if (res == 1)
            {
                string update = @"UPDATE equipment SET QUANTITY=QUANTITY-1, STATUS=3 WHERE EQUIPMENT_ID='" + id + "'";
                DBUtl.ExecSQL(update);
                assignment assignedEq = DBUtl.GetList<assignment>("SELECT * FROM assignment WHERE EQUIPMENT_ID='" + id + "'").First();
                activity expectedReturn = DBUtl.GetList<activity>("SELECT * FROM activity WHERE ACTIVITY_ID='" + assignedEq.ACTIVITY_ID + "'").First();
                string updateLoan = @"UPDATE loan_in SET EXPECTED_RETURN_DTE='{0:yyyy-MM-dd hh:mm:ss}' WHERE VOUCHER_ID ='" + model.VOUCHER_ID + "'";
                DateTime endDate = Convert.ToDateTime(expectedReturn.ACTIVITY_END);
                DBUtl.ExecSQL(updateLoan, endDate);
                TempData["Message"] = "Equipment:" + id + " has been borrowed successfully!";
                TempData["MsgType"] = "success";

            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("EquipmentAvailable");
        }

        public IActionResult ReturnEquipments()
        {

            string update = "UPDATE usr SET CURRENT_PG='return' WHERE NRIC='" + User.Identity.Name + "'";
            DBUtl.ExecSQL(update);
            List<Loan_in> loan = DBUtl.GetList<Loan_in>(@"SELECT * FROM loan_in WHERE NRIC='" + User.Identity.Name + "'");
            List<Loan_in_dtl> loan_dtl = DBUtl.GetList<Loan_in_dtl>(@"SELECT * FROM loan_in_dtl");
            List<Loan_in_dtl> returnList = new List<Loan_in_dtl>();
            //default datetime in sql
            DateTime value = new DateTime(1900, 1, 1);
            foreach (Loan_in ln in loan)
            {
                foreach (Loan_in_dtl dtl in loan_dtl)
                {
                    if (ln.VOUCHER_ID.Equals(dtl.VOUCHER_ID) && DateTime.Compare(dtl.RETURN_DTE, value) == 0)
                    {
                        returnList.Add(dtl);
                    }
                }
            }
            return View(returnList);
        }
        public IActionResult Return(string id)
        {
            string update = @"UPDATE loan_in_dtl SET RETURN_DTE='{0:yyyy-MM-dd hh:mm}' WHERE RECORD_ID='" + id + "'";
            DateTime currentDateTime = DateTime.Now;
            string person = GetPerson();
            string uuid = GenerateUUID();
            int res = DBUtl.ExecSQL(update, DateTime.Now);
            if (res == 1)
            {

                List<Loan_in_dtl> currentLoanDetail = DBUtl.GetList<Loan_in_dtl>("SELECT * FROM loan_in_dtl WHERE RECORD_ID ='" + id + "'");
                string updateEq = @"UPDATE equipment SET QUANTITY=QUANTITY+1, STATUS=2 WHERE EQUIPMENT_ID='" + currentLoanDetail[0].EQUIPMENT_ID + "'";
                DBUtl.ExecSQL(updateEq);
                string insert1 = @"INSERT INTO Activity_trans(TRANS_ID,ACTIVITY_ID,NRIC,TYPE,MACHINE_NAME,DTE_TIME_CR,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY)
VALUES('{0}',1,'{1}','RE','Machine 1','{2:yyyy-MM-dd hh:mm:ss}','{3:yyyy-MM-dd hh:mm:ss}','{4}','{5}')";
                DBUtl.ExecSQL(insert1, uuid, User.Identity.Name, currentDateTime, currentDateTime, person, person);
                TempData["Message"] = "Item scanned sucessfully Completed!";
                TempData["MsgType"] = "success";

            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

            List<Loan_in> loanList = DBUtl.GetList<Loan_in>("SELECT * FROM loan_in");
            var model = loanList.OrderByDescending(s => s.DTE_TIME_CR).First();
            List<Loan_in_dtl> latestAddition = DBUtl.GetList<Loan_in_dtl>("SELECT * FROM loan_in_dtl WHERE VOUCHER_ID='" + model.VOUCHER_ID + "'");

            List<Loan_in_dtl> totalEqz = DBUtl.GetList<Loan_in_dtl>("SELECT * FROM loan_in_dtl");
            int rowCountz = totalEqz.Count();
            TempData["count"] = HttpContext.Session.GetInt32("no");
            if (HttpContext.Session.GetInt32("no") != rowCountz)
            {
                HttpContext.Session.SetInt32("no", rowCountz);
                TempData["Message"] = latestAddition[0].EQUIPMENT_ID + " has been borrowed successfully!";
                TempData.Keep("Message");
                TempData["MsgType"] = "success";

            }
            return RedirectToAction("ReturnEquipments");
        }

        public IActionResult LoanVoucher()
        {
            var remaining = 3;
            if (HttpContext.Session.GetInt32("tries") < 3)
            {
                remaining = HttpContext.Session.GetInt32("tries").GetValueOrDefault();
            }
            else
            {
                HttpContext.Session.SetInt32("tries", 3);
            }

            List<Loan_in> loanList = DBUtl.GetList<Loan_in>("SELECT * FROM loan_in");
            var models = loanList.OrderByDescending(s => s.DTE_TIME_CR).First();
            if (remaining == 0)
            {

                List<Loan_in_dtl> loanDtl = DBUtl.GetList<Loan_in_dtl>(
@"SELECT * FROM loan_in_dtl WHERE VOUCHER_ID='" + models.VOUCHER_ID + "'");
                foreach (Loan_in_dtl dtl in loanDtl)
                {
                    var eqId = dtl.EQUIPMENT_ID;
                    List<Equipment> equipment = DBUtl.GetList<Equipment>(
    @"SELECT * FROM equipment WHERE EQUIPMENT_ID='" + eqId + "'");
                    var qty = equipment[0].QUANTITY + 1;

                    string updateEq = @"UPDATE equipment SET QUANTITY={0} WHERE EQUIPMENT_ID='{1}'";
                    DBUtl.ExecSQL(updateEq, qty, eqId);
                }
                string deleteDetail = @"DELETE FROM loan_in_dtl  WHERE VOUCHER_ID='" + models.VOUCHER_ID + "'";
                DBUtl.ExecSQL(deleteDetail);
                string deleteLoan = @"DELETE FROM loan_in  WHERE VOUCHER_ID='" + models.VOUCHER_ID + "'";
                DBUtl.ExecSQL(deleteLoan);
                TempData["Message"] = "Loans have been cancelled due to failed authentication";
                TempData["MsgType"] = "danger";
                return View("Index");
            }
            else
            {


                List<Loan_in_dtl> loanDtl = DBUtl.GetList<Loan_in_dtl>(
@"SELECT * FROM loan_in_dtl WHERE VOUCHER_ID='" + models.VOUCHER_ID + "'");
                ViewData["return"] = models.EXPECTED_RETURN_DTE;
                ViewData["voucher"] = models.VOUCHER_ID;
                return View(loanDtl);
            }


        }
        public IActionResult Complete(string fingerprint)
        {

            string usrSql = @"SELECT * FROM usr WHERE NRIC = '{0}'AND FINGERPRINT = HASHBYTES('SHA1', '{1}')";
            DataTable ds = DBUtl.GetTable(usrSql, User.Identity.Name, fingerprint);


            if (ds.Rows.Count != 1)
            {
                TempData["Message"] = "Fingerprint dosent match";
                TempData["MsgType"] = "danger";
                HttpContext.Session.SetInt32("tries", HttpContext.Session.GetInt32("tries").GetValueOrDefault() - 1);
                return RedirectToAction("LoanVoucher");
            }
            else
            {
                TempData["Message"] = "Equipments have been successfully borrowed.Please return items on time!";
                TempData["MsgType"] = "success";
                //success messages 
                return View("Index");
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



        [Authorize(Roles = "1,2,3,4")]
        public IActionResult VerifyDate(DateTime performDT)
        {
            if (performDT > DateTime.Today)
            {
                return Json($"Cannot choose a date earlier than the present date");
            }
            return Json(true);
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

        public IActionResult ListDetails()
        {
            List<Loan_in_dtl> loan_dtl = DBUtl.GetList<Loan_in_dtl>(@"SELECT * FROM loan_in_dtl");
            return View(loan_dtl);
        }

        private List<Loan_in> GetListLoan()
        {
            string loanSql = @"SELECT * FROM loan_in;";
            List<Loan_in> lstLoan = DBUtl.GetList<Loan_in>(loanSql);
            return lstLoan;
        }
        private List<Equipment> GetListEquipment()
        {
            string equipmentSql = @"SELECT * FROM equipment;";
            List<Equipment> lstEquipment = DBUtl.GetList<Equipment>(equipmentSql);
            return lstEquipment;
        }

        private string GenerateVoucher()
        {
            //"1"
            string start = "0000";
            string voucher = String.Format("{0:ddMMyyyy}", DateTime.Today) + start;
            //1
            int intCounter = 0000;
            foreach (Loan_in ln in GetListLoan())
            {
                if (voucher.Equals(ln.VOUCHER_ID))
                {
                    intCounter++;
                    string str = String.Format("{0}", intCounter);
                    if (str.Length == 1)
                    {
                        str = "000" + str;
                    }
                    else if (str.Length == 2)
                    {
                        str = "00" + str;
                    }
                    else if (str.Length == 3)
                    {
                        str = "0" + str;
                    }
                    voucher = String.Format("{0:ddMMyyyy}", DateTime.Today) + str;

                }
            }

            return voucher;
        }

    }

}
