using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodGenie.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Recipe Recipe { get; set; }
        public int Count { get; set; }
    }
}