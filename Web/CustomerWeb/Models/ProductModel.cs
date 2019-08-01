using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerWeb.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Sku { get; set; } // Stock Keeping Unit
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public int? MerchantId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool ShowOnHomePage { get; set; }
        //public int ShowingPriority { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
    }
}