using System;

namespace TwentyTwoSeven.Domain.ValueObjects
{
    public class CustomerId
    {
        public int Id { get; }

        public CustomerId(int id)
        {
            if (id == default)
            {
                throw new Exception("Please provide a valid Id");
            }
            else
            {
                Id = id;
            }
        }
    }
}
