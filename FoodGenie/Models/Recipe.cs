using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FoodGenie.Models
{
    public class Recipe
    {
       [Required]
       public string Name { get; set; }

       [Required]
       public int Id { get; set; }

       [Required]
       public string Description { get; set; }

       [Required]
       public double Price { get; set; }

       public double? CalorieCount { get; set; }
    }
}