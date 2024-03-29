﻿using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Merchant : BaseEntity
    {
        public string Name { get; set; }
        public string WebSite { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}