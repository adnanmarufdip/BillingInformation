using BillingInformation.Data;
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

        // GET: Products
        public ActionResult Index()
        {
            //DataContext dataContext = new DataContext();
            ModelState.Clear();
            return View(dataContext.GetProducts());
        }
    }
}
