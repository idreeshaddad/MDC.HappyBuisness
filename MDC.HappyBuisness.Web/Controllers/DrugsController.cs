using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using MDC.HappyBuisness.Web.Models.Drugs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MDC.HappyBuisness.Web.Controllers
{
    public class DrugsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DrugsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Action

        public async Task<IActionResult> Index()
        {
            var drugsWOW = await _context
                                .Drugs
                                .Select(d => new DrugLightViewModel() { Id = d.Id, Price = d.Price, StreetName = d.StreetName })
                                .ToListAsync();


            var drugs = await _context
                            .Drugs
                            .Include(d => d.Classification)
                            .OrderBy(d => d.Price)
                            .ToListAsync();

            var drugVMS = _mapper.Map<List<Drug>, List<DrugListViewModel>>(drugs);

            return View(drugVMS);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drug = await _context.Drugs
                                    .Include(d => d.Classification)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (drug == null)
            {
                return NotFound();
            }

            var drugVM = _mapper.Map<Drug, DrugDetailsViewModel>(drug);

            return View(drugVM);
        }

        public IActionResult Create()
        {
            var drugVM = new DrugViewModel();
            drugVM.Classifications = new SelectList(_context.Classifications, "Id", "Name");

            return View(drugVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrugViewModel drugVM)
        {
            if (ModelState.IsValid)
            {
                var drug = _mapper.Map<DrugViewModel, Drug>(drugVM);

                _context.Add(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            drugVM.Classifications = new SelectList(_context.Classifications, "Id", "Name", drugVM.ClassificationId);
            return View(drugVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Drugs == null)
            {
                return NotFound();
            }

            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null)
            {
                return NotFound();
            }

            var drugVM = _mapper.Map<Drug, DrugViewModel>(drug);
            drugVM.Classifications = new SelectList(_context.Classifications, "Id", "Name", drugVM.ClassificationId);
            return View(drugVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DrugViewModel drugVM)
        {
            if (id != drugVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var drug = _mapper.Map<DrugViewModel, Drug>(drugVM);
                
                try
                {
                    _context.Update(drug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugExists(drug.Id))
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

            drugVM.Classifications = new SelectList(_context.Classifications, "Id", "Name", drugVM.ClassificationId);
            return View(drugVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Drugs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Drugs'  is null.");
            }
            var drug = await _context.Drugs.FindAsync(id);
            if (drug != null)
            {
                _context.Drugs.Remove(drug);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool DrugExists(int id)
        {
            return (_context.Drugs?.Any(e => e.Id == id)).GetValueOrDefault();
        } 

        #endregion
    }
}
