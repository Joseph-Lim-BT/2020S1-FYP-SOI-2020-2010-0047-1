using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;
using SmarterTomorrow.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace SmarterTomorrow.Controllers
{
    public class TransactionController : Controller
    {
        [Authorize(Roles = "0,1")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "0,1")]
        public IActionResult Return(activity_trans a)
        {

            string type = a.TYPE;
            string sql = "SELECT * FROM activity_trans WHERE TYPE='" + type + "'";
            List<activity_trans> activityList = DBUtl.GetList<activity_trans>(sql);
            return View(activityList);
        }

        [HttpGet("Customer")]
        public IActionResult GetAll(activity_trans a)
        {

            string sql = "SELECT * FROM activity_trans";
            List<activity_trans> activityList = DBUtl.GetList<activity_trans>(sql);
            return View(activityList);
        }
    }
}