namespace TwentyTwoSeven.Common.RequestModels
{
    public static class AccountRequest
    {
        public static class V1
        {
            public class Add
            {
                public string AccNumber { get; set; }
                public string AccType { get; set; }
                public int Status { get; set; }
                public int CustomerId { get; set; }
            }

            public class Update
            {
                public int Id { get; set; }
                public string AccNumber { get; set; }
                public string AccType { get; set; }
                public int Status { get; set; }
                public int CustomerId { get; set; }
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
