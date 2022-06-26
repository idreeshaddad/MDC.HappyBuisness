using System.ComponentModel.DataAnnotations.Schema;

namespace MDC.HappyBuisness.Entities
{
    public class Pharmacist
    {
        public Pharmacist()
        {
            Deals = new List<Deal>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }

        public List<Deal> Deals { get; set; }

        [NotMapped]
        public string Fullname
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public int Age
        {
            get
            {
                if(DOB.HasValue)
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
