using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
   public class FrontPageViewModel
    {
        [Display(Name = "Total Number of Sellers")]
        public int TotalSellers { get; set; }

        [Display(Name = "Sum of all sales")]
        public double SumOfAllSales { get; set; }
    }
}
