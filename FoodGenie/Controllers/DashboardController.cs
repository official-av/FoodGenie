using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodGenie.Models;
using Microsoft.AspNet.Identity;

namespace FoodGenie.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        private List<ICartItem> GetRecipeList()
        {
            return _context.Users.Single((u => u.Id == User.Identity.GetUserId())).Cart.RecipeList;
        }

        private void AddRecipe(ICartItem rec)
        {
            var recList = GetRecipeList();
            if (!recList.Exists(c => c.RecName.Id == rec.RecName.Id)) //just update the count when recipe already in cart
            {
                rec.Count = 1;
                recList.Add(rec);
            }
            /*else
            {
                recList.Add(rec);
                rec.Count += recipe.Count;
                recList.RemoveAll(r => r.RecName.Id == recipe.RecName.Id);
                recList.Add(rec);
            }*/

            _context.SaveChanges();
        }

        private void RemoveRecipe(ICartItem rec)
        {
            var recList = GetRecipeList();
            if (!recList.Exists(c => c.RecName.Id == rec.RecName.Id)) //just update the count when recipe already in cart
            {
                recList.RemoveAll(r => r.RecName.Id == rec.RecName.Id);
            }
            /*
            recipe.Count -= rec.Count;
                recList.RemoveAll(r => r.RecName.Id == rec.RecName.Id);
            }*/
        }

        private void CartIncrement(ICartItem rec)
        {
            GetRecipeList().SingleOrDefault(c => c.RecName.Id == rec.RecName.Id).Count++;
        }

        private void CartDecrement(ICartItem rec)
        {
            if (rec.Count == 0)
            {
                RemoveRecipe(rec);
            }
            else
            {
                GetRecipeList().SingleOrDefault(c => c.RecName.Id == rec.RecName.Id).Count--;
            }
        }

        
        public ActionResult Index()
        {
            return View(GetRecipeList());
        }
    }
}