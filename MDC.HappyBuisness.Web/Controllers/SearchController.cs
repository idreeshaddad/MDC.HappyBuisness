using AutoMapper;
using MDC.HappyBuisness.Web.Data;
using MDC.HappyBuisness.Web.Models.Drugs;
using MDC.HappyBuisness.Web.Models.Pharmacists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MDC.HappyBuisness.Web.Controllers
{
    public class SearchController : Controller
    {
        #region Data and Contructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 

        #endregion

        #region Actions

        public async Task<IActionResult> Search(string searchType, string keyword)
        {
            if (searchType == "drug")
            {
                var drugVMs = await SearchByDrugName(keyword);
                return View("SearchDrug", drugVMs);
            }
            else if (searchType == "pharmacist")
            {
                var pharmacistVMs =  await SearchByPharmacistName(keyword);
                return View("SearchPharmacist", pharmacistVMs);
            }
            else
            {
                return NoContent();
            }
        } 

        #endregion

        #region Private Methods

        private async Task<List<DrugListViewModel>> SearchByDrugName(string keyword)
        {
            var drugs = await _context
                                .Drugs
                                .Where(d => d.StreetName.Contains(keyword) || d.Name.Contains(keyword))
                                .ToListAsync();

            var drugVMs = _mapper.Map<List<DrugListViewModel>>(drugs);

            return drugVMs;
        }

        private async Task<List<PharmacistListViewModel>> SearchByPharmacistName(string keyword)
        {
            var pharmacists = await _context
                                        .Pharmacists
                                        .Where(b => b.FirstName.Contains(keyword) || b.LastName.Contains(keyword))
                                        .ToListAsync();

            var pharmacistVMs = _mapper.Map<List<PharmacistListViewModel>>(pharmacists);

            return pharmacistVMs;
        } 

        #endregion
    }
}
