using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodGenie.Models;
using Microsoft.AspNet.Identity;

namespace FoodGenie.Controllers
{
    public class RecipeController : Controller
    {
        private ApplicationDbContext _context;

        public RecipeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(Recipe r)
        {
            _context.Recipes.Add(r);
            _context.SaveChanges();
            return RedirectToAction("Index","Recipe");
        }
    }
}