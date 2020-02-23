using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MDS9.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name="Price")]
        public decimal Price { get; set; }

        public virtual ICollection<OrderDetails> o2 { get; set; }
    }
}