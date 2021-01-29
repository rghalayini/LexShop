using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexShop.Core.Models
{
    public class OrderItem : BaseEntity
    {
        //1. Create this class in the Models folder.
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string image { get; set; }
        public int Quantity { get; set; }
    }
}
