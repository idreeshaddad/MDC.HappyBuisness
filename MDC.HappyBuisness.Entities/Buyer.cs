using MDC.HappyBuisness.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDC.HappyBuisness.Entities
{
    public class Buyer
    {
        public Buyer()
        {
            Deals = new List<Deal>();
        }

        public int Id { get; set; }
        public string CodeName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DOB { get; set; }
        public double Discount { get; set; }

        public List<Deal> Deals { get; set; }


        [NotMapped]
        public int Age
        {
            get
            {
                if (DOB.HasValue)
                {
                    return DateTime.Now.Year - DOB.Value.Year;
                }
                else
                {
                    return -1;
                }
            }
        }

    }
}
