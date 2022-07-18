using MDC.HappyBuisness.Utils.Enums;

namespace MDC.HappyBuisness.Entities
{
    public class Deal
    {
        public Deal()
        {
            Drugs = new List<Drug>();
        }

        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime DealTime { get; set; }
        public Guid TransactionCode { get; set; }


        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; }

        public int PharmacistId { get; set; }
        public Pharmacist Pharmacist { get; set; }

        public List<Drug> Drugs { get; set; }
    }
}
