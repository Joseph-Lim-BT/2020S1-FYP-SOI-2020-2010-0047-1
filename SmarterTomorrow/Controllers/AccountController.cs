using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;
using SmarterTomorrow.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace SmarterTomorrow.Controllers
{

    public class AccountController : Controller
    {
        private const string LOGIN_SQL =
         @"SELECT * FROM Usr 
            WHERE NRIC = '{0}' 
              AND PASSWORD = HASHBYTES('SHA1', '{1}')";

        private const string LASTLOGIN_SQL =
           @"UPDATE Usr SET DTE_TIME_LAST_LOGIN=GETDATE() WHERE NRIC='{0}'";

        private const string ROLE_COL = "GROUP_ID";
        private const string NAME_COL = "NRIC";

        private const string REDIRECT_CNTR = "Home";
        private const string REDIRECT_ACTN = "Index";

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (!AuthenticateUser(user.NRIC, user.PASSWORD, out ClaimsPrincipal principal))
            {
                ViewData["Message"] = "Incorrect NRIC or Password";
                ViewData["MsgType"] = "warning";
                return View();
            }
            else
            {
                string update = @"UPDATE usr SET DTE_TIME_LAST_LOGIN='" + DateTime.Now + "' WHERE NRIC='" + user.NRIC + "'";
                DBUtl.ExecSQL(update);
                DBUtl.ExecSQL(LASTLOGIN_SQL, user.NRIC);
                HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   principal);

                // Update the Last Login Timestamp of the User
                DBUtl.ExecSQL(LASTLOGIN_SQL, user.NRIC);

                if (TempData["returnUrl"] != null)
                {
                    string returnUrl = TempData["returnUrl"].ToString();
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                }

                return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
            }
        }

        [Authorize]
        public IActionResult Logoff(string returnUrl = null)
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
        }

        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return View();
        }

        [Authorize(Roles ="0,1")]
        public IActionResult Users()
        {
            ViewData["NAME"] = User.Identity.Name;
            List<User> list = DBUtl.GetList<User>("SELECT * FROM usr ORDER BY UNIT, NRIC, FULL_NAME, GROUP_ID");
            return View(list);
        }

        [Authorize(Roles ="0")]
        public IActionResult Delete(string id)
        {
            string delete = "DELETE FROM usr WHERE NRIC='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "User Record Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("Users");
        }

        [Authorize(Roles = "0")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [Authorize(Roles = "0")]
        [HttpPost]
        public IActionResult Register(User newUsr)
        {
            string uuid = GenerateUUID();
            //need to check whether there is already an account with the same NRIC
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("Register");
            }
            else
            //
            {
                DateTime currentDateTime = DateTime.Now;
                string person = GetPerson();
                string insert =
                   @"INSERT INTO usr(NRIC,FULL_NAME,GROUP_ID,PASSWORD,DTE_TIME_CR,DTE_TIME_LAST_LOGIN,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY,UNIT,RANK) 
VALUES('{0}','{1}','{2}',HASHBYTES('SHA1', '{3}'),'{4:yyyy-MM-dd hh:mm:ss}','{5:yyyy-MM-dd hh:mm:ss}','{6:yyyy-MM-dd hh:mm:ss}','{7}','{8}','{9}','{10}')";
                if (DBUtl.ExecSQL(insert, newUsr.NRIC, newUsr.FULL_NAME, newUsr.GROUP_ID, newUsr.PASSWORD, currentDateTime, currentDateTime, currentDateTime, person, person, newUsr.UNIT, newUsr.RANK) == 1)
                {
                    string insert1 = @"INSERT INTO activity_trans(TRANS_ID,ACTIVITY_ID,NRIC,TYPE,MACHINE_NAME,DTE_TIME_CR,DTE_TIME_LAST_MOD,CREATED_BY,MODIFIED_BY)
VALUES('{0}',1,'{1}','RG','Machine 1','{2:yyyy-MM-dd hh:mm:ss}','{3:yyyy-MM-dd hh:mm:ss}','{4}','{5}')";
                    DBUtl.ExecSQL(insert1, uuid, User.Identity.Name, currentDateTime, currentDateTime, person, person);
                    TempData["Message"] = "User Successfully Registered";
                    TempData["MsgType"] = "success";
                    return RedirectToAction("Users");
                }

                else
                {
                    ViewData["Message"] = "You cannot submit a record that has the same NRIC as an existing record!";
                    ViewData["MsgType"] = "danger";
                    return View("Register");
                }
            }
        }


        [HttpGet]
        [Authorize(Roles = "0")]
        public IActionResult Update(string id)
        {
            // Get the record from the database using the id
            string usrSql = @"SELECT * FROM usr WHERE NRIC= '{0}'";
            List<User> lstUsr = DBUtl.GetList<User>(usrSql, id);

            // If the record is found, pass the model to the View
            if (lstUsr.Count == 1)
            {
                return View(lstUsr[0]);
            }
            else
            // Otherwise redirect to the movie list page
            {
                TempData["Message"] = "User record not found.";
                TempData["MsgType"] = "warning";
                return RedirectToAction("Users");
            }
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public IActionResult Update(User user)
        {

            // Check the state of the model ((Ref Week 9). 
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("Update");
            }
            else
            {
                // Write the SQL statement
                DateTime currentDateTime = DateTime.Now;
                string person = GetPerson();
                string update = @"UPDATE usr SET FULL_NAME='{1}', GROUP_ID='{2}',DTE_TIME_LAST_MOD='{3:yyyy-MM-dd hh:mm:ss}', MODIFIED_BY ='{4}', UNIT ='{5}', RANK='{6}', PASSWORD=HASHBYTES('SHA1', '{7}')  WHERE NRIC='{0}'";

                // Execute the SQL statement in a secure manner
                // Check the result and branch
                if (DBUtl.ExecSQL(update, user.NRIC, user.FULL_NAME, user.GROUP_ID, currentDateTime, person, user.UNIT, user.RANK, user.PASSWORD) == 1)
                {
                    // If successful set a TempData success Message and MsgType
                    TempData["Message"] = "User Updated";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    // If unsuccessful, set a TempData message that equals the DBUtl error message
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
                // Call the action ListMovies to show the result of the update
                return RedirectToAction("Users");
            }
        }

        [AllowAnonymous]
        public IActionResult VerifyNRIC(string nric)
        {
            string select = $"SELECT * FROM usr WHERE NRIC='{nric}'";
            if (DBUtl.GetTable(select).Rows.Count > 0)
            {
                return Json($"[{nric}] already registered");
            }
            return Json(true);
        }

        private bool AuthenticateUser(string nric, string pw, out ClaimsPrincipal principal)
        {
            principal = null;

            DataTable ds = DBUtl.GetTable(LOGIN_SQL, nric, pw);
            if (ds.Rows.Count == 1)
            {
                principal =
                   new ClaimsPrincipal(
                      new ClaimsIdentity(
                         new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, nric),
                        new Claim(ClaimTypes.Name, ds.Rows[0][NAME_COL].ToString()),
                        new Claim(ClaimTypes.Role, ds.Rows[0][ROLE_COL].ToString())
                         }, "Basic"
                      )
                   );
                return true;
            }
            return false;
        }

        private string GetPerson()
        {
            string personSql = @"SELECT *
                                FROM usr WHERE NRIC='" + User.Identity.Name + "'";
            List<User> assigned = DBUtl.GetList<User>(personSql);
            string person = assigned[0].FULL_NAME;
            return person;
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

