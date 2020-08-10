using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmarterTomorrow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace SmarterTomorrow.Controllers
{
    public class StocktakeController : Controller
    {
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult StocktakeList()
        {
            update();
            List<stocktaking> list = DBUtl.GetList<stocktaking>(@"SELECT * FROM stocktaking");
            return View(list);
        }
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult Archive()
        {
            update();
            List<stocktaking> list = DBUtl.GetList<stocktaking>(@"SELECT * FROM stocktaking");
            return View(list);
        }
        [Authorize(Roles = "0,1,2,3")]
        public int CurrentStocktake()
        {
            List<User> user = DBUtl.GetList<User>("SELECT * FROM usr WHERE nric = '" + User.Identity.Name + " ' ");
            List<stocktaking> stocktakingPerson = DBUtl.GetList<stocktaking>(@"SELECT * FROM Stocktaking WHERE NRIC='" + user[0].NRIC + "' ORDER BY DTE_TIME_CR DESC");
            int id = stocktakingPerson[0].STOCKTAKE_ID;
            return id;
        }
        [Authorize(Roles = "0,1,2,3")]
        private void update()
        {
            List<stocktaking> sList = DBUtl.GetList<stocktaking>("SELECT * from stocktaking");
            DateTime date1 = DateTime.Now;

            foreach (stocktaking s in sList)
            {
                DateTime date2 = s.DTE_TIME_CR;
                String difference = (date1 - date2).TotalDays.ToString();
                double a = Double.Parse(difference);
                if (a > 30)
                {
                    String update = "UPDATE stocktaking SET archive={0} WHERE stocktaking_id='{1}'";
                    DBUtl.ExecSQL(update, 1, s.STOCKTAKE_ID);
                }
            }
        }
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult AddStocktake()
        {
            return View();
        }
        [Authorize(Roles = "0,1,2,3")]
        [HttpPost]
        public IActionResult AddStocktake(IFormFile postedFile, String location)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        return View();
                    }


                    var equipment = new List<Equipment>();
                    var accessory = new List<accessories>();
                    using (var sreader = new StreamReader(postedFile.OpenReadStream()))
                    {
                        //First line is header. If header is not passed in csv then we can neglect the below line.
                        string[] headers = sreader.ReadLine().Split(',');

                        //Loop through the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            equipment.Add(new Equipment
                            {          
                                EQUIPMENT_ID = rows[0] + "," + rows[1].Trim().Replace("\\", "").Replace("\"", ""),
                                EQUIPMENT_NAME = rows[2],
                                ELEMENT_MATERIAL_NO = rows[3],
                                SERIAL_NO = rows[4],
                                EQUIPMENT_TYPE_ID = Int32.Parse(rows[5]),
                                STORAGE_LOCATION = rows[6],
                                STORAGE_BIN = rows[7],
                                BOX_LOT_NO = rows[8],
                                BUTT_NO = rows[9],
                                QUANTITY = Int32.Parse(rows[10]),
                                STATUS = Int32.Parse(rows[11])
                            }); ;

                            accessory.Add(new accessories
                            {
                                ACCESSORY_ID = (rows[13] + "," + rows[14]).Trim().Replace("\\","").Replace("\"",""),
                                ACCESSORY_NAME = rows[15],
                                ELEMENT_MATERIAL_NO = rows[16],
                                SERIAL_NO = rows[17],
                                STORAGE_LOCATION = rows[18],
                                STORAGE_BIN = rows[19],
                                QUANTITY = Int32.Parse(rows[20]),
                                BOX_LOT_NO = rows[21]
                            }); ;



                        }

                    }



                    List<User> user = DBUtl.GetList<User>("SELECT * FROM usr WHERE nric = '" + User.Identity.Name + " ' ");
                    List<Equipment> currequip = DBUtl.GetList<Equipment>("SELECT * FROM equipment WHERE STORAGE_LOCATION ='" + location + "'");
                    List<accessories> curracess = DBUtl.GetList<accessories>("Select * FROM accessories WHERE STORAGE_LOCATION='" + location + "'");
                    ArrayList diffequip = new ArrayList();
                    ArrayList diffaccess = new ArrayList();
                    ArrayList equip = new ArrayList();
                    var archive = 0;
                    int curr_equip_total = currequip.Count;
                    int curr_access_total = 0;

                    foreach (var z in curracess)
                    {
                        curr_access_total += z.QUANTITY;
                    }


                    foreach (var a in equipment)
                    {
                        if (a.STATUS == 1)
                        {
                            equip.Add(a);
                        }
                        foreach (var b in currequip)
                        {
                            if (a.STORAGE_LOCATION.Equals(location))
                            {
                                if (a.SERIAL_NO.Equals(b.SERIAL_NO))
                                {
                                    if (a.STATUS != b.STATUS)
                                    {
                                        diffequip.Add(a);
                                    }
                                } else
                                {
                                    diffequip.Add(a);
                                }
                            }

                        }
                    }
                    int stock_access = 0;
                    foreach (var c in accessory)
                    {
                        if (c.STORAGE_LOCATION.Equals(location))
                        {
                            stock_access += c.QUANTITY;
                        }


                        foreach (var d in curracess)
                        {
                            if (c.STORAGE_LOCATION.Equals(location))
                            {
                                if (c.ACCESSORY_ID.Equals(d.ACCESSORY_ID))
                                {
                                    if (c.QUANTITY != d.QUANTITY)
                                    {
                                        diffaccess.Add(c);
                                    }
                                } else
                                {
                                    diffaccess.Add(c);
                                }
                            }
                        }
                    }

                    int stock_equip = equip.Count;

                    int curr_equip_avail = DBUtl.GetList<Equipment>("SELECT * FROM Equipment WHERE STORAGE_LOCATION='" + location + "' AND STATUS=1").Count;

                    int diff_equip = stock_equip - curr_equip_avail;
                    int diff_access = stock_access - curr_access_total;



                    String insert = @"INSERT INTO Stocktaking(NRIC, TOTAL_QUANTITY, TOTAL_QUANTITY_ACC, DTE_TIME_CR, ARCHIVE, E_DIFF, A_DIFF, STOCKTAKE_LOCATION) Values ('{0}', {1}, {2}, '{3:yyyy-MM-dd HH:mm:ss}', {4}, {5}, {6}, '{7}')";

                    int resu = DBUtl.ExecSQL(insert, user[0].NRIC, stock_equip, stock_access, DateTime.Now, archive, diff_equip, diff_access, location);






                    foreach (var e in equipment)
                    {
                        if (e.STORAGE_LOCATION.Equals(location))
                        { 
                            if (diffequip.Contains(e))
                            {
                                String insertequip = @"INSERT INTO stocktake_equipment(EQUIPMENT_ID, EQUIPMENT_NAME, EQUIPMENT_TYPE_ID, ELEMENT_MATERIAL_NO, SERIAL_NO, STOCKTAKE_ID, STORAGE_LOCATION, STORAGE_BIN, QUANTITY, BOX_LOT_NO, BUTT_NO, STATUS, MATCH) Values ('{0}' , '{1}' , {2} , '{3}' , '{4}' , {5} , '{6}', '{7}', {8}, '{9}', '{10}', {11}, '{12}')";

                                DBUtl.ExecSQL(insertequip, e.EQUIPMENT_ID, e.EQUIPMENT_NAME, e.EQUIPMENT_TYPE_ID, e.ELEMENT_MATERIAL_NO, e.SERIAL_NO, CurrentStocktake(), e.STORAGE_LOCATION, e.STORAGE_BIN, e.QUANTITY, e.BOX_LOT_NO, e.BUTT_NO, e.STATUS, false);
                            }
                            else
                            {
                                String insertequip = @"INSERT INTO stocktake_equipment(EQUIPMENT_ID, EQUIPMENT_NAME, EQUIPMENT_TYPE_ID, ELEMENT_MATERIAL_NO, SERIAL_NO, STOCKTAKE_ID, STORAGE_LOCATION, STORAGE_BIN, QUANTITY, BOX_LOT_NO, BUTT_NO, STATUS, MATCH) Values ('{0}' , '{1}' , {2} , '{3}' , '{4}' , {5} , '{6}', '{7}', {8}, '{9}', '{10}', {11}, '{12}')";

                                DBUtl.ExecSQL(insertequip, e.EQUIPMENT_ID, e.EQUIPMENT_NAME, e.EQUIPMENT_TYPE_ID, e.ELEMENT_MATERIAL_NO, e.SERIAL_NO, CurrentStocktake(), e.STORAGE_LOCATION, e.STORAGE_BIN, e.QUANTITY, e.BOX_LOT_NO, e.BUTT_NO, e.STATUS, true);
                            }
                        }



                    }

                    foreach (var f in accessory)
                    {
                        if (f.STORAGE_LOCATION.Equals(location))
                        {
                            if (diffaccess.Contains(f))
                            {
                                String insertacess = @"INSERT INTO stocktake_accessories(STOCKTAKE_ID , ACCESSORY_ID, ACCESSORY_NAME, ELEMENT_MATERIAL_NO, SERIAL_NO, STORAGE_LOCATION, STORAGE_BIN, QUANTITY, MATCH) Values ({0} , '{1}' , '{2}' , '{3}' , '{4}' , '{5}', '{6}', {7}, '{8}')";

                                DBUtl.ExecSQL(insertacess, CurrentStocktake(), f.ACCESSORY_ID, f.ACCESSORY_NAME, f.ELEMENT_MATERIAL_NO, f.SERIAL_NO, f.STORAGE_LOCATION, f.STORAGE_BIN, f.QUANTITY, false);
                            }
                            else
                            {
                                String insertacess = @"INSERT INTO stocktake_accessories(STOCKTAKE_ID , ACCESSORY_ID, ACCESSORY_NAME, ELEMENT_MATERIAL_NO, SERIAL_NO, STORAGE_LOCATION, STORAGE_BIN, QUANTITY, MATCH) Values ({0} , '{1}' , '{2}' , '{3}' , '{4}' , '{5}', '{6}', {7}, '{8}')";


                                DBUtl.ExecSQL(insertacess, CurrentStocktake(), f.ACCESSORY_ID, f.ACCESSORY_NAME, f.ELEMENT_MATERIAL_NO, f.SERIAL_NO, f.STORAGE_LOCATION, f.STORAGE_BIN, f.QUANTITY, true);
                            }
                        }

                    }

                    return RedirectToAction("ViewCurrentStocktake");

                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }

            }
            else
            {
                ViewBag.Message = "Please select the file to upload.";
            }
            return View();
        }
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult ViewEquipment(int id)
        {
            var stockequiplist = DBUtl.GetList<stocktake_equipment>("SELECT * FROM stocktake_equipment WHERE stocktake_id = '" + id + "' AND MATCH='false'");
            return View(stockequiplist);
        }
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult ViewAccessory(int id)
        {
            var stockaccesslist = DBUtl.GetList<stocktake_accessories>("SELECT * FROM stocktake_accessories WHERE stocktake_id = '" + id + "' AND MATCH='false'");
           return View(stockaccesslist);
        }
        [Authorize(Roles = "0,1,2,3")]
        public IActionResult ViewCurrentStocktake()
        {
            int stocktake_id = CurrentStocktake();
            var currstocktake = DBUtl.GetList<stocktaking>("Select * From stocktaking Where stocktake_id = '" + stocktake_id + "'");
            return View(currstocktake);
        }
    

    //Start Here
    private string GetPerson()
        {
            string personSql = @"SELECT *
                                FROM usr WHERE NRIC='" + User.Identity.Name + "'";
            List<User> assigned = DBUtl.GetList<User>(personSql);
            string person = assigned[0].FULL_NAME;
            return person;
        }

    }
}