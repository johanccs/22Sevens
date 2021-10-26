﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwentyTwoSeven.Data.Dto
{
    [Table("Customer")]
    public class CustomerDto
    {
        public CustomerDto()
        {
            Accounts = new List<AccountDto>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustId { get; set; }
        public IList<AccountDto> Accounts { get; set; }
    }
}
