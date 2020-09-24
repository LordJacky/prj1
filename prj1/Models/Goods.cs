using System;
using System.Collections.Generic;

namespace prj1.Models
{
    public partial class Goods
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int ProductQuantity { get; set; }
    }
}
