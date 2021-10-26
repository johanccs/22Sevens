using System;

namespace TwentyTwoSeven.Data.Entities
{
    public class TransferDto
    {
        public int Id { get; set; }
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime DateTransfered { get; set; }
    }
}
