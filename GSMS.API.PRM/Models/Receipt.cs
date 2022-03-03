using System;
using System.Collections.Generic;

#nullable disable

namespace GSMS.API.PRM.Models
{
    public partial class Receipt
    {
        public Receipt()
        {
            ReceiptDetails = new HashSet<ReceiptDetail>();
        }

        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string StoreId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
