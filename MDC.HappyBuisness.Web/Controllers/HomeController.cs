using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Data;
using MDC.HappyBuisness.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MDC.HappyBuisness.Web.Models.Drugs;
using MDC.HappyBuisness.Web.Models.Home;

namespace MDC.HappyBuisness.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Data and Constructor

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var homePageVM = new HomePageViewModel();

            homePageVM.TopRatedDrugs = await GetTopRated3Drugs();
            homePageVM.NumberOfBuyers = await GetNumberOfBuyers();

            
            return View(homePageVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } 

        #endregion

        #region Private Methods

        private async Task<List<DrugListViewModel>> GetTopRated3Drugs()
        {
            var top3ratedDrugs = await _context
                                        .Drugs
                                        .OrderByDescending(d => d.Rating)
                                        .Take(3)
                                        .ToListAsync();

            var top3ratedDrugsVms = _mapper.Map<List<Drug>, List<DrugListViewModel>>(top3ratedDrugs);
            return top3ratedDrugsVms;
        }

        private async Task<int> GetNumberOfBuyers()
        {
            return await _context.Buyers.CountAsync();                  
        }

        #endregion
    }
}