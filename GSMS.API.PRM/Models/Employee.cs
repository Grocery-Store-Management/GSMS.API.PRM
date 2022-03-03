using System;
using System.Collections.Generic;

#nullable disable

namespace GSMS.API.PRM.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Receipts = new HashSet<Receipt>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string StoreId { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
