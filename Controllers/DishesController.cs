using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class DishesController : Controller
    {
        private CRUDeliciousContext db;

        public DishesController(CRUDeliciousContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult AllDishes()
        {
            List<Dish> allDishes = db.Dishes.OrderByDescending(cd => cd.CreatedAt).ToList();
            return View("AllDishes", allDishes);
        }

        [HttpGet("/dishes/{dishID}")]
        public IActionResult Details(int dishID)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishID);

            if(dish == null)
            {
                return RedirectToAction("AllDishes");
            }

            return View("Details", dish);
        }

        [HttpGet("/dishes/new")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost("/dishes/create")]
        public IActionResult Create(Dish newDish)
        {
            if(ModelState.IsValid == false)
            {
                return View("New");
            }

            db.Dishes.Add(newDish);
            db.SaveChanges();
            return RedirectToAction("AllDishes");
        }

        [HttpPost("/dishes/{dishID}")]
        public IActionResult Delete(int dishID)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishID);

            if (dish != null)
            {
                db.Dishes.Remove(dish);
                db.SaveChanges();
            }

            return RedirectToAction("AllDishes");
        }

        [HttpGet("/dishes/{dishID}/edit")]
        public IActionResult Edit(int dishID)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishID);

            if(dish == null)
            {
                return RedirectToAction("AllDishes");
            }

            return View("Edit", dish);
        }

        [HttpPost("/dishes/{dishID}/update")]
        public IActionResult Update(int dishID, Dish editedDish)
        {
            if(ModelState.IsValid == false)
            {
                editedDish.DishId = dishID;
                return View("Edit", editedDish);
            }

            Dish dbDish = db.Dishes.FirstOrDefault(d => d.DishId == dishID);

            if(dbDish == null)
            {
                return RedirectToAction("AllDishes");
            }

            dbDish.ChefName = editedDish.ChefName;
            dbDish.DishName = editedDish.DishName;
            dbDish.Calories = editedDish.Calories;
            dbDish.Tastiness = editedDish.Tastiness;
            dbDish.Description = editedDish.Description;
            dbDish.UpdatedAt = DateTime.Now;

            db.Dishes.Update(dbDish);
            db.SaveChanges();

            return RedirectToAction("Details", new { dishID = dbDish.DishId });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}