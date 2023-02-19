using BillingInformation.Data;
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
    }
}
