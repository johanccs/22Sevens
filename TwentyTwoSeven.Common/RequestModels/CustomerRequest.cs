using System.Collections.Generic;

namespace TwentyTwoSeven.Common.RequestModels
{
    public static class CustomerRequest
    {
        public static class V1
        {
            public class Add
            {
                public string Name { get; set; }
                public int Status { get; set; }
                public IList<int> Accounts { get; set; }
            }
            public class Update
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public int Status { get; set; }
                public IList<int> Accounts { get; set; }
            }

            public class Delete
            {
                public int Id { get; set; }
            }

            public class GetById
            {
                public int Id { get; set; }
            }
        }
    }
}
