using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using revisedPoe.Data;
using revisedPoe.Models;

namespace revisedPoe.Controllers
{
    public class StockItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockItemsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Add(AddStockItemViewModel addStockItemViewModel)
        {
            var stockItem = new StockItem()
            {
                Name = addStockItemViewModel.Name,
                Description = addStockItemViewModel.Description,

                Farmer = User.Identity.Name,
                Quantity = addStockItemViewModel.Quantity,
                StockType = addStockItemViewModel.StockType,
                entryDate = addStockItemViewModel.entryDate,

            };

                _context.Add(stockItem);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        

        }
         
            
          

        // GET: StockItems
        public async Task<IActionResult> Index()
        {
              return _context.StockItems != null ? 
                          View(await _context.StockItems.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StockItems'  is null.");
        }


        //Adapted from:Youtube
        //Author:freeCodeCamp.org
        //Author Profile:https://www.youtube.com/@freecodecamp
        //Date:6 May 2020
        //Link:https://www.youtube.com/watch?v=BfEjDD8mWYg
        
        //GET: shows search form

                [Authorize(Roles ="Employee")]
        public async Task<IActionResult> SearchFarmer()
        {
            return View();


        }

        //Adapted from:Youtube
        //Author:freeCodeCamp.org
        //Author Profile:https://www.youtube.com/@freecodecamp
        //Date:6 May 2020
        //Link:https://www.youtube.com/watch?v=BfEjDD8mWYg

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ShowSearchResults(string SearchUsername, DateTime StartDate, DateTime EndDate,string StockType)
        {
            if (StartDate == default && EndDate == default)
            {
                if (StockType == null)
                {
                    return _context.StockItems != null ?
                        View(SearchUsername ,await _context.StockItems.Where(n => n.Farmer == SearchUsername).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.StockItems' is null.");
                }
                else {

                    return _context.StockItems != null ?
                            View(await _context.StockItems.Where(n => n.Farmer == SearchUsername && n.StockType == StockType).ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.StockItems' is null.");



                }
            }
            else
            {
                DateRange range = new DateRange(StartDate, EndDate);

                return _context.StockItems != null ?
                    View(await _context.StockItems.Where(n => n.Farmer == SearchUsername && n.entryDate >=StartDate && n.entryDate <= EndDate).ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.StockItems' is null.");
            }
        }

        // GET: StockItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.StockItems == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // GET: StockItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Quantity,StockType,entryDate")] StockItem stockItem)
        {
            
            {
                stockItem.ID = Guid.NewGuid();
                //Adapted from: Stackoverflow
                //Author:james
                //Author Profile:https://stackoverflow.com/users/3064389/james
                //Date:26 September 2016
                //Link:https://stackoverflow.com/questions/39693946/how-to-get-currently-logged-users-email-address-to-the-view-in-asp-net-c

                stockItem.Farmer = User.Identity.Name;
                _context.Add(stockItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockItem);
        }
        // GET: StockItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.StockItems == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems.FindAsync(id);
            if (stockItem == null)
            {
                return NotFound();
            }
            return View(stockItem);
        }

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Description,Farmer,Quantity,StockType,entryDate")] StockItem stockItem)
        {
            if (id != stockItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockItemExists(stockItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stockItem);
        }

        // GET: StockItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.StockItems == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // POST: StockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.StockItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StockItems'  is null.");
            }
            var stockItem = await _context.StockItems.FindAsync(id);
            if (stockItem != null)
            {
                _context.StockItems.Remove(stockItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockItemExists(Guid id)
        {
          return (_context.StockItems?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
