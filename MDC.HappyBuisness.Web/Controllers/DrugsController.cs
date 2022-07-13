using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using MDC.HappyBuisness.Web.Models.Drugs;
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
            var drugs = await _context
                            .Drugs
                            .Include(d => d.Classification)
                            .ToListAsync();

            var drugVMS = _mapper.Map<List<Drug>, List<DrugViewModel>>(drugs);

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

            var drugVM = _mapper.Map<Drug, DrugViewModel>(drug);

            return View(drugVM);
        }

        public IActionResult Create()
        {
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Drug drug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "Name", drug.ClassificationId);
            return View(drug);
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
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "Name", drug.ClassificationId);
            return View(drug);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Drug drug)
        {
            if (id != drug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "Id", "Name", drug.ClassificationId);
            return View(drug);
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
