using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using AutoMapper;
using MDC.HappyBuisness.Web.Models.Deals;
using Microsoft.AspNetCore.Authorization;

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
                            .Include(deal => deal.Drugs)
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
                                .Include(d => d.Drugs)
                                    .ThenInclude(drugs => drugs.Classification)
                                .SingleOrDefaultAsync(m => m.Id == id);

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
            dealVM.DrugsMultiSelect = new MultiSelectList(_context.Drugs, "Id", "StreetName");

            return View(dealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DealViewModel dealVM)
        {
            if (ModelState.IsValid)
            {
                var deal = _mapper.Map<Deal>(dealVM);

                deal.DealTime = DateTime.Now;
                deal.LastModifiedTime = deal.DealTime;
                deal.TransactionCode = Guid.NewGuid();

                await AddDrugsToDealAsync(dealVM, deal);

                await CalculateDealTotalPrice(deal);

                _context.Add(deal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            dealVM.Buyers = new SelectList(_context.Buyers, "Id", "CodeName", dealVM.BuyerId);
            dealVM.Pharmacists = new SelectList(_context.Pharmacists, "Id", "FirstName", dealVM.PharmacistId);
            dealVM.DrugsMultiSelect = new MultiSelectList(_context.Drugs, "Id", "StreetName", dealVM.DrugIds);
            
            return View(dealVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deals == null)
            {
                return NotFound();
            }

            var deal = await _context
                                .Deals
                                .Include(d => d.Buyer)
                                .Include(d => d.Pharmacist)
                                .Include(d => d.Drugs)
                                .SingleOrDefaultAsync(d => d.Id == id);

            if (deal == null)
            {
                return NotFound();
            }

            var dealVM = _mapper.Map<DealViewModel>(deal);

            dealVM.Buyers = new SelectList(_context.Buyers, "Id", "CodeName", dealVM.BuyerId);
            dealVM.Pharmacists = new SelectList(_context.Pharmacists, "Id", "FirstName", dealVM.PharmacistId);

            dealVM.DrugIds = deal.Drugs.Select(d => d.Id).ToList();
            dealVM.DrugsMultiSelect = new MultiSelectList(_context.Drugs, "Id", "StreetName", dealVM.DrugIds);

            return View(dealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DealViewModel dealVM)
        {
            if (id != dealVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var deal = _mapper.Map<Deal>(dealVM);
                try
                {
                    deal.LastModifiedTime = DateTime.Now;

                    _context.Update(deal);
                    await _context.SaveChangesAsync();


                    await UpdateDealDrugsAndSave(dealVM, deal.Id);
                    await CalculateDealTotalPrice(deal);

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

            dealVM.Buyers = new SelectList(_context.Buyers, "Id", "CodeName", dealVM.BuyerId);
            dealVM.Pharmacists = new SelectList(_context.Pharmacists, "Id", "FirstName", dealVM.PharmacistId);
            dealVM.DrugsMultiSelect = new MultiSelectList(_context.Drugs, "Id", "StreetName", dealVM.DrugIds);

            return View(dealVM);
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

        private async Task AddDrugsToDealAsync(DealViewModel dealVM, Deal deal)
        {
            var drugs = await _context.Drugs.Where(drug => dealVM.DrugIds.Contains(drug.Id)).ToListAsync();
            deal.Drugs.AddRange(drugs);
        }

        private async Task UpdateDealDrugsAndSave(DealViewModel dealVM, int dealId)
        {
            // Load the Deal from DB
            var deal = await _context
                            .Deals
                            .Include(d => d.Drugs)
                            .Where(d => d.Id == dealId)
                            .SingleAsync();

            // Clear all deal drugs 
            deal.Drugs.Clear();

            // Load the new drugs from DB
            var drugs = await _context
                                    .Drugs
                                    .Where(drug => dealVM.DrugIds.Contains(drug.Id))
                                    .ToListAsync();

            // Add drugs to deal
            deal.Drugs.AddRange(drugs);

            // Save the deal
            
        }

        private async Task CalculateDealTotalPrice(Deal deal)
        {
            var buyerDiscount = await _context
                                            .Buyers
                                            .Where(b => b.Id == deal.BuyerId)
                                            .Select(b => b.Discount)
                                            .SingleAsync();

            var drugsPriceTotal = deal.Drugs.Sum(d => d.Price);

            var discountAmount = drugsPriceTotal * (buyerDiscount / 100f);

            deal.TotalPrice = drugsPriceTotal - discountAmount;
        }

        #endregion
    }
}
