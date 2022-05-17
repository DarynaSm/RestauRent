using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingASP.Data;
using TrainingASP.Models;
using TrainingASP.Services;

namespace TrainingASP.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAll());
        }
        // GET: Restaurant/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }
        // Post: Restaurant/ShowSearchResults
        /* public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
          {
              return View("Index", await _context.Restaurant.Where( j => j.Name.Contains(SearchPhrase)).ToListAsync());
          }*/

        // GET: Restaurant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _service.GetRestaurantDtoById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        //[Authorize]
        // GET: Restaurant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,TableAmount,OpeningTime,ClosingTime")] CreateRestaurantRequest restaurant)
        {
            if (ModelState.IsValid)
            {
                _service.AddRestaurant(restaurant);
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurant/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RestaurantDto restaurant = _service.GetRestaurantDtoById(id);

            if (restaurant == null)
            {
                return NotFound();
            }
            EditRestaurantRequest editedRes = new EditRestaurantRequest();

            editedRes.Name = restaurant.Name;
            editedRes.Address = restaurant.Address;
            editedRes.PhoneNumber = restaurant.PhoneNumber;
            editedRes.TableAmount = restaurant.Tables?.Count ?? 0;
            editedRes.OpeningTime = restaurant.OpeningTime;
            editedRes.ClosingTime = restaurant.ClosingTime;

            return View(editedRes);
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      //  [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID,Address,OpeningTime,ClosingTime,PhoneNumber,Tables,TableAmount")] EditRestaurantRequest restaurant)
        {
            if (id != restaurant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(restaurant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurant/Delete/5
        // [Authorize]
        /* public async Task<IActionResult> Delete(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var restaurant = await _context.Restaurant
                 .FirstOrDefaultAsync(m => m.ID == id);
             if (restaurant == null)
             {
                 return NotFound();
             }

             return View(restaurant);
         }

         // POST: Restaurant/Delete/5
         [Authorize]
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             var restaurant = await _context.Restaurant.FindAsync(id);
             _context.Restaurant.Remove(restaurant);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool RestaurantExists(int id)
         {
             return _context.Restaurant.Any(e => e.ID == id);
         }
        */
    }
}
