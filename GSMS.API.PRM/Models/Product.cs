using System;
using System.Collections.Generic;

#nullable disable

namespace GSMS.API.PRM.Models
{
    public partial class Product
    {
        public Product()
        {
            ImportOrderDetails = new HashSet<ImportOrderDetail>();
            ReceiptDetails = new HashSet<ReceiptDetail>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public int Status { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiringDate { get; set; }
        public int StoredQuantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ImportOrderDetail> ImportOrderDetails { get; set; }
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
