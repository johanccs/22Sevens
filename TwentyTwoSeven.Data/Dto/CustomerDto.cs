using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwentyTwoSeven.Data.Dto
{
    [Table("Customer")]
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string CustId { get; set; }
        public IList<AccountDto> Accounts { get; set; }
    }
}
