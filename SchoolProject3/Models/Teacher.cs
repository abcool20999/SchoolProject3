
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SchoolProject3.Models;
using System.Web;

namespace SchoolProject3.Models
{
    public class Teacher
    {
        //what defines a Teacher

        //Teacher id
        public int teacherId { get; set; }

        //Teacher first_name
        [Required(ErrorMessage = "First Name is required.")]
        [DisplayName("First Name")]
        public string teacherfname { get; set; }

        //Teacher last_name
        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        public string teacherlname { get; set; }

        //hiredate
        [Required(ErrorMessage = "Hired Date is required.")]
        public DateTime hiredate { get; set; }

        //teacher employeenumber
        [Required(ErrorMessage = "Employee Number is required.")]
        [DisplayName("Employee Number")]
        public string employeenumber { get; set; }
        //teacher salary
        [Required(ErrorMessage = "Salary is required.")]
        public string salary { get; set; }
        //teachers classes
        public string classname { get; set; }
    }
}

