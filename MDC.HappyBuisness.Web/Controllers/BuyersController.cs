using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using AutoMapper;
using MDC.HappyBuisness.Web.Models.Buyers;

namespace MDC.HappyBuisness.Web.Controllers
{
    public class BuyersController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BuyersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var buyers = await _context.Buyers.ToListAsync();

            var buyerVMs = _mapper.Map<List<BuyerListViewModel>>(buyers);

            return View(buyerVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Buyers == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buyer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Buyers == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyers.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }
            return View(buyer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Buyer buyer)
        {
            if (id != buyer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerExists(buyer.Id))
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
            return View(buyer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Buyers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Buyers'  is null.");
            }
            var buyer = await _context.Buyers.FindAsync(id);
            if (buyer != null)
            {
                _context.Buyers.Remove(buyer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Method

        private bool BuyerExists(int id)
        {
            return (_context.Buyers?.Any(e => e.Id == id)).GetValueOrDefault();
        } 

        #endregion
    }
}
