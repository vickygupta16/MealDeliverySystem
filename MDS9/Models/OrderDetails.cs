using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MDS9.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        [Required]
        [Display(Name="Parent")]
        public int UserId { get; set; }
        
        [Display(Name="Student")]
        public int? StudentId { get; set; }
        [Required]
        [Display(Name="Meal")]
        public int MealId { get; set; }
        [Required]
        [Display(Name="Date")]
        public string Date { get; set; }
        [Required]
        [Display(Name="Time")]
        public string Time { get; set; }
        [Required]
        [Display(Name="Total Amount")]
        public decimal TotalAmount { get; set; }

        public virtual UserProfile ups { get; set; }
        public virtual Student s5 { get; set; }
        public virtual Meal m4 { get; set; }
    }
}