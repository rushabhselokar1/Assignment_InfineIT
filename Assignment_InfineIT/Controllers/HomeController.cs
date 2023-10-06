using Assignment_InfineIT.DataContext;
using Assignment_InfineIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_InfineIT.Controllers
{
    public class HomeController : Controller
    {
        Data db = new Data();
        public ActionResult Index()
        {
            List<Employee> employee = db.GetEmployeesWithFullNameAndCode();
            return View(employee);
        }

       

    }
}