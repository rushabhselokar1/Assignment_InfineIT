using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Assignment_InfineIT.DataContext;
using Assignment_InfineIT.Models;
using MySql.Data.MySqlClient;

public class EmployeeController : Controller
{
        Data db = new Data();
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(db.GetDepartmentsFromDatabase(), "Code", "Name");
            ViewBag.City = new SelectList(db.GetCityFromDatabase(), "Code", "Name");
            ViewBag.State = new SelectList(db.GetStateFromDatabase(), "Code", "Name");
            ViewBag.Country = new SelectList(db.GetCountryFromDatabase(), "Code", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            // Define the prefix and initialize the postfix
            string prefix = employee.Code;
            int postfix = 1;

            // Generate the initial code
            string generatedCode = $"{prefix} {postfix:D3}";

            // Check if the generated code already exists in the database
            while (db.EmployeeCodeExists(generatedCode))
            {
                // If it exists, increment the postfix and regenerate the code
                postfix++;
                generatedCode = $"{prefix} {postfix:D3}";
            }

            // Set the generated code as the employee's code
            employee.Code = generatedCode;

            // Now, you can add the employee to the database with the unique code
            db.CreateEmployee(employee);

            return RedirectToAction("Create");
        }

        public ActionResult Edit(string code)
            {
                ViewBag.Departments = new SelectList(db.GetDepartmentsFromDatabase(), "Code", "Name");
                ViewBag.City = new SelectList(db.GetCityFromDatabase(), "Code", "Name");
                ViewBag.State = new SelectList(db.GetStateFromDatabase(), "Code", "Name");
                ViewBag.Country = new SelectList(db.GetCountryFromDatabase(), "Code", "Name");
                Employee employee = db.GetEmployeeByCode(code);

                DateTime dateOfBirth = employee.DOB; 
                DateTime currentDate = DateTime.Now;
                TimeSpan ageTimeSpan = currentDate - dateOfBirth;

                int ageInYears = ageTimeSpan.Days / 365; // Approximate years
                int ageInMonths = (int)((ageTimeSpan.TotalDays - (ageInYears * 365)) / 30); // Approximate months
                int ageInDays = (int)(ageTimeSpan.TotalDays % 30); // Approximate days

                ViewBag.AgeYears = ageInYears;
                ViewBag.AgeMonths = ageInMonths;
                ViewBag.AgeDays = ageInDays;

                string department = db.GetDepartmentNameWithId(employee.Dept_Code);
                string country = db.GetCountryNameWithId(employee.Country_Code);
                string city = db.GetCityNameWithId(employee.City_Code);
                string state = db.GetStateNameWithId(employee.State_Code);

                ViewBag.DepartmentName = department;
                ViewBag.CountryName = country;
                ViewBag.CityName = city;
                ViewBag.StateName = state;

      

                return View(employee);
            }

        public ActionResult SalaryDetails()
        {
            //List<CityStatisticsViewModel> cityStatistics = 
            //List<DepartmentStatisticsViewModel> departmentStatistics = db.GetDepartmentStatistics();

            //ViewBag.CityStatistics = cityStatistics;
            //ViewBag.DepartmentStatistics = departmentStatistics;

            return View();
        }

        public ActionResult GetCityStatistics()
        {
            List<CityStatisticsViewModel> cityStatistics = db.GetCityStatistics();// Fetch city statistics data
             return Json(cityStatistics, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepartmentStatistics()
        {
            List<DepartmentStatisticsViewModel> departmentStatistics = db.GetDepartmentStatistics();// Fetch department statistics data
            return Json(departmentStatistics, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string code)
        {
       
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Index"); 
            }

            db.DeleteEmployeeByCode(code);

            return RedirectToAction("Index", "Home");

        }


}
