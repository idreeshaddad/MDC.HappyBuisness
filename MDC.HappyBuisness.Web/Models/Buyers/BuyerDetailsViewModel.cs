using MDC.HappyBuisness.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace MDC.HappyBuisness.Web.Models.Buyers
{
    public class BuyerDetailsViewModel
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DOB { get; set; }
        public double Discount { get; set; }

        [Display(Name = "Discount %")]
        public string DiscountInPercentage { get; set; }


        //public List<Deal> Deals { get; set; }
    }
}
