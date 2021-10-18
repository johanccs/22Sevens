using System.Collections.Generic;

namespace TwentyTwoSeven.Data.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public IList<int> Accounts { get; set; }
    }
}
