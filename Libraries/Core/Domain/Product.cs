﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Sku { get; set; } // Stock Keeping Unit
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string ImageUrl { get; set; }
        public int? MerchantId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool ShowOnHomePage { get; set; }
        //public int ShowingPriority { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}