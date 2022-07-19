using MDC.HappyBuisness.Utils.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MDC.HappyBuisness.Web.Models.Deals
{
    public class DealViewModel
    {
        public DealViewModel()
        {
            DrugIds = new List<int>();
        }

        public int Id { get; set; }
        public PaymentType PaymentType { get; set; }
        public int BuyerId { get; set; }
        public int PharmacistId { get; set; }
        public List<int> DrugIds { get; set; }

        [ValidateNever]
        public Guid TransactionCode { get; set; }


        //----------------------------------------------------------------------------
        // Things needed in the HTTPGET but not in the HTTPPOST

        [ValidateNever]
        public SelectList Buyers { get; set; }

        [ValidateNever]
        public SelectList Pharmacists { get; set; }

        [ValidateNever]
        public MultiSelectList DrugsMultiSelect { get; set; }
    }
}
