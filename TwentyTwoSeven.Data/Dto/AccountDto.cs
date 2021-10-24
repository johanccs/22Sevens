using System.ComponentModel.DataAnnotations.Schema;

namespace TwentyTwoSeven.Data.Dto
{
    [Table("Account")]
    public class AccountDto
    {

        public int Id { get; set; }

        public string AccNumber { get; set; }

        public int AccType { get; set; }

        public int StatusId { get; set; }

        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
    }
}
