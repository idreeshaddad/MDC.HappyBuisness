using MDC.HappyBuisness.Web.Models.Drugs;

namespace MDC.HappyBuisness.Web.Models.Home
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            TopRatedDrugs = new List<DrugListViewModel>();
        }

        public List<DrugListViewModel> TopRatedDrugs { get; set; }

        public int NumberOfBuyers { get; set; }
    }
}
