using MDC.HappyBuisness.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace MDC.HappyBuisness.Web.Models.Buyers
{
    public class BuyerViewModel
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DOB { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }
    }
}
