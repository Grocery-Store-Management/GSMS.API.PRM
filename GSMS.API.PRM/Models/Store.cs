using System;
using System.Collections.Generic;

#nullable disable

namespace GSMS.API.PRM.Models
{
    public partial class Store
    {
        public Store()
        {
            Employees = new HashSet<Employee>();
            ImportOrders = new HashSet<ImportOrder>();
            Receipts = new HashSet<Receipt>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ImportOrder> ImportOrders { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
