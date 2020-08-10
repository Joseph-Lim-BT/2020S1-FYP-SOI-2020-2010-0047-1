using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmarterTomorrow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmarterTomorrow.Controllers

{
    [Route("api/Transaction")]
    [ApiController]
    public class TransactionApiController : ControllerBase
    {
        [HttpGet("Customer/{custId}/Order")]
        public IActionResult GetOrdersByCustomer(string custId)
        {
            string sql = "SELECT * FROM activity_trans";
            List<activity_trans> activityList = DBUtl.GetList<activity_trans>(sql);
            var data = activityList.Where(o => o.TYPE == custId).ToList();
            return Ok(data);

        }
    }
}