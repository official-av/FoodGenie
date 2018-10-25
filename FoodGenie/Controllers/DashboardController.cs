using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodGenie.Models;
using FoodGenie.ViewModels;
using Microsoft.AspNet.Identity;

namespace FoodGenie.Controllers
{
    public class DashboardController : Controller
    {
        private readonly  ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        private List<CartItem> GetCartItems()
        {
            var user = GetActiveUser();
            if (user?.CartItems == null)
            {
                user.CartItems=new List<CartItem>();
                _context.SaveChanges();
            }

            return user.CartItems;
        }

        private Recipe GetRecipe(int id)
        {
            return _context.Recipes.ToList().SingleOrDefault(c => c.Id == id);
        }

        //add recipes to user cart
        public ActionResult AddRecipe(int recId)
        {
            var rec = GetRecipe(recId);
            var cartItems = GetCartItems();
            if (!cartItems.Exists(c=>c.Recipe.Id==recId))
            {
                var currentItem = new CartItem
                {
                    Count = 1,
                    Recipe = rec
                };
                cartItems.Add(currentItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Dashboard");
        }

        //remove recipes from user cart
        public ActionResult RemoveRecipe(int itemId)
        {
            var recList = GetCartItems();
            if (recList.Exists(c => c.Id == itemId))
            {
                recList.RemoveAll(r => r.Id == itemId);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult CartIncrement(int itemId)
        {
            var recipe = GetCartItems().SingleOrDefault(c => c.Id == itemId);
            recipe.Count++;
            RemoveRecipe(itemId); //remove recipe with old count
            GetCartItems().Add(recipe); //insert recipe with modified count
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult CartDecrement(int itemId)
        {
            var recipe = GetCartItems().SingleOrDefault(c => c.Id == itemId);
            recipe.Count--;
            RemoveRecipe(itemId); //remove recipe with old count
            if (recipe.Count > 0)
            {
                GetCartItems().Add(recipe); //insert recipe with modified count
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }

        
        public ActionResult Index()
        {
            var r = GetCartItems() ?? new List<CartItem>();
            DashboardViewModel val = new DashboardViewModel
            {
                RecList = _context.Recipes.ToList(),
                CartItems = r
            };
            return View(val);
        }

        public ActionResult Success(CheckoutViewModel data)
        {   ResetCart();
            return View(data);
        }

        private void ResetCart()
        {
            GetActiveUser().CartItems.RemoveAll(c => 1 == 1);
            _context.SaveChanges();
        }

        private ApplicationUser GetActiveUser()
        {
            var id = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault((u => u.Id == id));
            return user;
        }
    }
}