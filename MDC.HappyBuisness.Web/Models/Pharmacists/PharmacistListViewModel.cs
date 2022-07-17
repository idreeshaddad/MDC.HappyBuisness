﻿using System.ComponentModel.DataAnnotations;

namespace MDC.HappyBuisness.Web.Models.Pharmacists
{
    public class PharmacistListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
