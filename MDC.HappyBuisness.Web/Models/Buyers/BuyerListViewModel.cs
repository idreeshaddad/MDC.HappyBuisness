using MDC.HappyBuisness.Utils.Enums;

namespace MDC.HappyBuisness.Web.Models.Buyers
{
    public class BuyerListViewModel
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public double Discount { get; set; }
    }
}
