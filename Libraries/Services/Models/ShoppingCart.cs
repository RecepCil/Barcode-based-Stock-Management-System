using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class ShoppingCart
    {
        public IEnumerable<ShoppingCartItem> itemList { get; set; }
    }

    public class ShoppingCartItem
    {
        public string Sku { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Quantity must be numeric")]
        public int Quantity { get; set; }
    }
}