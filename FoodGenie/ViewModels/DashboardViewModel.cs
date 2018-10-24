using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodGenie.Models;

namespace FoodGenie.ViewModels
{
    public class DashboardViewModel
    {
        public List<Recipe> RecList { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}