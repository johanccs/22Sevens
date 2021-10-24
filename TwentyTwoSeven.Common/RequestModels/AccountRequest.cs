using System;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Common.RequestModels
{
    public static class AccountRequest
    {
        public static class V1
        {
            public class Add
            {
                public string AccNumber { get; set; }
                public int AccType { get; set; }
                public int StatusId { get; set; }
                public decimal Balance { get; set; }
                public int CustomerId { get; set; }

                public static AccountDto Map(AccountRequest.V1.Add entity)
                {
                    if (entity == null)
                        throw new ArgumentNullException("Entity cannot be null");

                    var returnVal = new AccountDto
                    {
                        AccNumber = entity.AccNumber,
                        AccType = entity.AccType,
                        CustomerId = entity.CustomerId,
                        StatusId = entity.StatusId
                    };

                    return returnVal;
                }
            }

            public class Update
            {
                public int Id { get; set; }
                public string AccNumber { get; set; }
                public int AccType { get; set; }
                public int Status { get; set; }
                public decimal Balance { get; set; }
                public int CustomerId { get; set; }

                public static AccountDto Map(AccountRequest.V1.Update entity)
                {
                    if (entity == null)
                        throw new ArgumentNullException("Entity cannot be null");

                    var returnVal = new AccountDto
                    {
                        Id = entity.Id,
                        AccNumber = entity.AccNumber,
                        AccType = entity.AccType,
                        CustomerId = entity.CustomerId,
                        StatusId = entity.Status
                    };

                    return returnVal;
                }
            }

            public class Delete
            {
                public int Id { get; set; }
            }

            public class GetById
            {
                public int Id { get; set; }
            }

            public class Transfer
            {
                public string SourceAccNumber { get; set; }

                public string DestinationAccNumber { get; set; }
            }
        }
    }
}
