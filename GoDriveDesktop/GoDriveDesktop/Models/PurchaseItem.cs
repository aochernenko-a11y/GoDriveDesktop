using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDriveDesktop.Models
{
    public class PurchaseItem
    {
        public int ProductId { get; set; }
        public string ItemType { get; set; } = "";
        public string ItemName { get; set; } = "";
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = "";
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string PromotionName { get; set; } = "";
    }

}
