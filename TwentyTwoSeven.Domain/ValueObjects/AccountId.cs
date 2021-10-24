using System;

namespace TwentyTwoSeven.Domain.ValueObjects
{
    public class AccountId
    {
        public int Id { get; }

        public AccountId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Please provide a valid Id");
            }
            else
            {
                Id = id;
            }
        }
    }
}
