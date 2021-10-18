using System;
using System.Collections.Generic;
using System.Text;

namespace TwentyTwoSeven.Data.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public string AccNumber { get; set; }
        public string AccType { get; set; }
        public int Status { get; set; }
        public int CustomerId { get; set; }
    }
}
