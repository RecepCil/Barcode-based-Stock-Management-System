using System;

namespace Core.Domain
{
    public class Log : BaseEntity
    {
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }
        public int? TransactionId { get; set; }
        public int LogType { get; set; }
        public bool IsError { get; set; }
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public DateTime CreatedOnUtc { get; set; }
      
        //public virtual User User { get; set; }
    }
}