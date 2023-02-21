using BillingInformation.Data;
using BillingInformation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;


namespace BillingInformation.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext dataContext;

        public HomeController()
        {
            this.dataContext = new DataContext();
        }

        // GET: Item name and unit price for the dropdown 
        [System.Web.Http.HttpGet]
        public ActionResult GetItems()
        {
            ViewBag.productData = JsonConvert.SerializeObject(dataContext.GetProducts());
            return View();
        }


        //public ActionResult AddNewBill()
        //{
        //    return View();
        //}


        // POST: Creating new bill
        [System.Web.Http.HttpPost]
        public ActionResult AddNewBill([FromBody] BillModel billModel)
        {
            bool result = dataContext.AddBill(billModel);

            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
