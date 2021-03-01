using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class SaleViewModel
    {
        public Sale Sale { get; set; }
        public IEnumerable<SelectListItem> Sellers { get; set; }
    }
}
