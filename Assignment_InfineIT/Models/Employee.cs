using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_InfineIT.Models
{
    public class Employee
    {
        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Middle_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(200)]
        public string Full_Name { get; set; }

        public DateTime DOB { get; set; }

        public int Salary { get; set; }

        public byte Dept_Code { get; set; }

        public int Country_Code { get; set; }

        public int State_Code { get; set; }

        public int City_Code { get; set; }
    }
}