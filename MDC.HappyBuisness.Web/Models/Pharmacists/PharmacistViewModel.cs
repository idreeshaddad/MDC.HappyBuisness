using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MDC.HappyBuisness.Web.Models.Pharmacists
{
    public class PharmacistViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }

        [ValidateNever]
        public string FullName { get; set; }
    }
}
