using MDC.HappyBuisness.Web.Models.Classifications;
using System.ComponentModel.DataAnnotations;

namespace MDC.HappyBuisness.Web.Models.Drugs
{
    public class DrugDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        public double Price { get; set; }
        public string ImageName { get; set; }

        public ClassificationViewModel Classification { get; set; }
    }
}
