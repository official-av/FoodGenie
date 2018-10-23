using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodGenie.Models
{
    public class Cart
    {
        public List<ICartItem> RecipeList { get; set; }
        public int Id { get; set; }
    }

    public interface ICartItem
    {
        Recipe RecName { get; set; }
        int Count { get; set; }
    }
}