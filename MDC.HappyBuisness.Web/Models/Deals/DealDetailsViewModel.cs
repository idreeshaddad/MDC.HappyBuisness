using MDC.HappyBuisness.Utils.Enums;
using MDC.HappyBuisness.Web.Models.Drugs;
using System.ComponentModel.DataAnnotations;

namespace MDC.HappyBuisness.Web.Models.Deals
{
    public class DealDetailsViewModel
    {
        public DealDetailsViewModel()
        {
            Drugs = new List<DrugListViewModel>();
        }

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

        [Display(Name = "Pharmacist")]
        public string PharmacistFullName { get; set; }

        public List<DrugListViewModel> Drugs { get; set; }
    }
}
