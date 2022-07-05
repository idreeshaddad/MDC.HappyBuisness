using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using MDC.HappyBuisness.Web.Models.Classifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MDC.HappyBuisness.Web.Controllers
{
    public class ClassificationsController : Controller
    {
        #region Data and const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClassificationsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var classifications = await _context.Classifications.ToListAsync();

            var classificationVMs = _mapper.Map<List<Classification>, List<ClassificationViewModel>>(classifications);

            return View(classificationVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classifications == null)
            {
                return NotFound();
            }

            var classification = await _context
                                            .Classifications
                                            .FirstOrDefaultAsync(m => m.Id == id);

            if (classification == null)
            {
                return NotFound();
            }

            var classificationVM = _mapper.Map<Classification, ClassificationViewModel>(classification);

            return View(classificationVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassificationViewModel classificationVM)
        {
            if (ModelState.IsValid)
            {
                var classification = _mapper.Map<ClassificationViewModel, Classification>(classificationVM);

                _context.Add(classification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(classificationVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Classifications == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications.FindAsync(id);

            if (classification == null)
            {
                return NotFound();
            }

            var classificationVM = _mapper.Map<Classification, ClassificationViewModel>(classification);

            return View(classificationVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassificationViewModel classificationVM)
        {
            if (id != classificationVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var classification = _mapper.Map<ClassificationViewModel, Classification>(classificationVM);

                    _context.Update(classification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassificationExists(classificationVM.Id))
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
            return View(classificationVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classifications == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classification == null)
            {
                return NotFound();
            }

            var classificationVM = _mapper.Map<Classification, ClassificationViewModel>(classification);


            return View(classificationVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classifications == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Classifications'  is null.");
            }
            var classification = await _context.Classifications.FindAsync(id);
            if (classification != null)
            {
                _context.Classifications.Remove(classification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool ClassificationExists(int id)
        {
            return (_context.Classifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion
    }
}
