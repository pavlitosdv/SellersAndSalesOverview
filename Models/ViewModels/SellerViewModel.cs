using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
   public class SellerViewModel
    {
        public Seller Seller { get; set; }


        [Display(Name = "Months")]
        public string Month { get; set; }

        [Display(Name = "Sum of all sales / Revenue")]
        public double Total { get; set; }

        [Display(Name="Sum of Commissions")]
        public double Commission { get; set; }


    }
}
