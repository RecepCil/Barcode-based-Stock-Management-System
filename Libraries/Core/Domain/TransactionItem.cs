namespace Core.Domain
{
    public class TransactionItem : BaseEntity
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}