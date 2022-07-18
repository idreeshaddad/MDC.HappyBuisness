using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using AutoMapper;
using MDC.HappyBuisness.Web.Models.Deals;

namespace MDC.HappyBuisness.Web.Controllers
{
    public class DealsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DealsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var deals = await _context
                            .Deals
                            .Include(deal => deal.Buyer)
                            .ToListAsync();

            var dealVMs = _mapper.Map<List<DealListViewModel>>(deals);

            return View(dealVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Deals == null)
            {
                return NotFound();
            }

            var deal = await _context
                                .Deals
                                .Include(d => d.Buyer)
                                .Include(d => d.Pharmacist)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (deal == null)
            {
                return NotFound();
            }

            var dealVM = _mapper.Map<DealDetailsViewModel>(deal);

            return View(dealVM);
        }

        public IActionResult Create()
        {
            var dealVM = new DealViewModel();

            dealVM.Buyers = new SelectList(_context.Buyers, "Id", "CodeName");
            dealVM.Pharmacists = new SelectList(_context.Pharmacists, "Id", "FirstName");
            dealVM.Drugs = new MultiSelectList(_context.Drugs, "Id", "StreetName");

            return View(dealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Deal deal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyers, "Id", "CodeName", deal.BuyerId);
            ViewData["PharmacistId"] = new SelectList(_context.Pharmacists, "Id", "FirstName", deal.PharmacistId);
            return View(deal);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deals == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyers, "Id", "CodeName", deal.BuyerId);
            ViewData["PharmacistId"] = new SelectList(_context.Pharmacists, "Id", "FirstName", deal.PharmacistId);
            return View(deal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Deal deal)
        {
            if (id != deal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealExists(deal.Id))
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
            ViewData["BuyerId"] = new SelectList(_context.Buyers, "Id", "CodeName", deal.BuyerId);
            ViewData["PharmacistId"] = new SelectList(_context.Pharmacists, "Id", "FirstName", deal.PharmacistId);
            return View(deal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deals == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Deals'  is null.");
            }
            var deal = await _context.Deals.FindAsync(id);
            if (deal != null)
            {
                _context.Deals.Remove(deal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool DealExists(int id)
        {
            return (_context.Deals?.Any(e => e.Id == id)).GetValueOrDefault();
        } 

        #endregion
    }
}
