namespace Core.Domain
{
    public class TransactionItem : BaseEntity
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }        
        public virtual Transaction Transaction { get; set; }
    }
}