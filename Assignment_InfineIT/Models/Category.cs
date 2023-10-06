using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_InfineIT.Models
{
    // Department model class
    public class Department
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public byte Sort_No { get; set; }
    }

    // City model class
    public class City
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int State_Code { get; set; }
        public int Country_Code { get; set; }
        public byte Sort_No { get; set; }
    }

    // State model class
    public class State
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Country_Code { get; set; }
        public byte Sort_No { get; set; }
    }

    // Country model class
    public class Country
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public byte Sort_No { get; set; }
    }

    public class CityStatisticsViewModel
    {
        public string Name { get; set; }
        public int TotalSalary { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class DepartmentStatisticsViewModel
    {
        public string Name { get; set; }
        public int TotalSalary { get; set; }
        public int EmployeeCount { get; set; }
    }


}