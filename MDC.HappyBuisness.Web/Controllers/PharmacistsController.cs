using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using AutoMapper;
using MDC.HappyBuisness.Web.Models.Pharmacists;

namespace MDC.HappyBuisness.Web.Controllers
{
    public class PharmacistsController : Controller
    {
        #region Data & Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PharmacistsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var pharmacists = await _context.Pharmacists.ToListAsync();

            var pharmacistVMs = _mapper.Map<List<PharmacistListViewModel>>(pharmacists);

            return View(pharmacistVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pharmacists == null)
            {
                return NotFound();
            }

            var pharmacist = await _context
                                        .Pharmacists
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (pharmacist == null)
            {
                return NotFound();
            }

            var pharmacistVM = _mapper.Map<PharmacistDetailsViewModel>(pharmacist);

            return View(pharmacist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PharmacistViewModel pharmacistVM)
        {
            if (ModelState.IsValid)
            {
                var pharmacist = _mapper.Map<Pharmacist>(pharmacistVM);

                _context.Add(pharmacist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacistVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pharmacists == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacists.FindAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            var pharmacistVM = _mapper.Map<PharmacistViewModel>(pharmacist);

            return View(pharmacistVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pharmacist pharmacist)
        {
            if (id != pharmacist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacistExists(pharmacist.Id))
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
            return View(pharmacist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pharmacists == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pharmacists'  is null.");
            }
            var pharmacist = await _context.Pharmacists.FindAsync(id);
            if (pharmacist != null)
            {
                _context.Pharmacists.Remove(pharmacist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool PharmacistExists(int id)
        {
            return (_context.Pharmacists?.Any(e => e.Id == id)).GetValueOrDefault();
        } 

        #endregion
    }
}
