using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwentyTwoSeven.Data.Dto
{
    [Table("Account")]
    public class AccountDto
    {
        [Key]
        public int Id { get; set; }

        public string AccNumber { get; set; }

        public int AccType { get; set; }

        public int StatusId { get; set; }

        public decimal Balance { get; set; }

        public string CustomerId { get; set; }

        public virtual CustomerDto Customer { get; set; }
    }
}
