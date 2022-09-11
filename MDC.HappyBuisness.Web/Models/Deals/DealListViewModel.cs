using MDC.HappyBuisness.Utils.Enums;
using MDC.HappyBuisness.Web.Models.Drugs;
using System.ComponentModel.DataAnnotations;

namespace MDC.HappyBuisness.Web.Models.Deals
{
    public class DealListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Buyer")]
        public string BuyerCodeName { get; set; }

        [Display(Name = "Deal Time")]
        public DateTime DealTime { get; set; }

        [Display(Name = "Payment Type")]
        public PaymentType PaymentType { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }

        [Display(Name = "Transaction Code")]
        public Guid TransactionCode { get; set; }

        public List<DrugLightViewModel> Drugs { get; set; }
    }
}
