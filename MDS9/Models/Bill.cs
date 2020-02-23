using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MDS9.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        [Required]
        [Display(Name="Parent")]
        public int UserId { get; set; }
        [Display(Name="Student")]
        public int? StudentId { get; set; }
        [Required]
        [Display(Name="Date")]
        public string Date { get; set; }
        [Required]
        [Display(Name="Total Bill")]
        public decimal TotalBill { get; set; }

        public virtual UserProfile ups { get; set; }
        public virtual Student s7 { get; set; }
    }
}