using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Transaction : BaseEntity
    {
        public string ActionType { get; set; }  // satış - alış - iade
        public DateTime TransactionDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<TransactionItem> TransactionItems { get; set; }
    }
}