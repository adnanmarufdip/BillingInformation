using BillingInformation.Data;
using BillingInformation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [HttpGet]
        public ActionResult GetItems()
        {
            ViewBag.productData = JsonConvert.SerializeObject(dataContext.GetProducts());
            return View();
        }


        public ActionResult AddNewBill()
        {
            return View();
        }


        // POST: Creating new bill
        [HttpPost]
        public ActionResult AddNewBill(BillModel billModel)
        {
            dataContext.AddBill(billModel);
            //ViewBag.BillState = "Bill added";
            return View();
        }
    }
}
