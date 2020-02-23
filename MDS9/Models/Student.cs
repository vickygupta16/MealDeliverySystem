using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MDS9.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [Display(Name="Parent Name")]
        public int UserId { get; set; }
        [Required]
        [Display(Name="First Name")]
        public string Fname { get; set; }
        [Required]
        [Display(Name="Last Name")]
        public string Lname { get; set; }
        //[Required]
        [Display(Name="Class")]
        public string Class { get; set; }
        //[Required]
        [Display(Name="Division")]
        public string Division { get; set; }
        [Required]
        [Display(Name="Roll Number")]
        public int RollNo { get; set; }
        [Required]
        [Display(Name="Age")]
        public int Age { get; set; }
        public virtual UserProfile ups { get; set; }
        public virtual ICollection<OrderDetails> o2 { get; set; }
        public virtual ICollection<Bill> b2 { get; set; }
    }
}