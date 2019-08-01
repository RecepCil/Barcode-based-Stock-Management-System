using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerWeb.Models
{
    public class TransactionModel
    {
        public IEnumerable<TransactionItemModel> itemList { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Total must be numeric")]
        public decimal Total { get; set; }
    }

    public class TransactionItemModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Quantity must be numeric")]
        public int Quantity { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UnitPrice must be numeric")]
        public decimal UnitPrice { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "AmountToPay must be numeric")]
        public decimal AmountToPay { get; set; }
    }

}