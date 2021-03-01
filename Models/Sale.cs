using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Transaction value")]
        public double TransactionValue { get; set; }

        [Required]
        [Display(Name ="Date of Sale")]
        public DateTime DateOfSale { get; set; }

        [Required]
        public int SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public Seller Seller { get; set; }



        [NotMapped]
        public double Commission { get { return TransactionValue * (double)0.1; } }

    }
}
