using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MDC.HappyBuisness.Web.Models.Drugs
{
    public class DrugViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        public double Price { get; set; }
        public int ClassificationId { get; set; }
        public int Rating { get; set; }

        [ValidateNever]
        public SelectList Classifications { get; set; }
    }
}
