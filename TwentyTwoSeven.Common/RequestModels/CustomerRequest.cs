using System;
using System.Collections.Generic;
using TwentyTwoSeven.Data.Dto;

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
                public int CustId { get; set; }
                public IList<int> Accounts { get; set; }

                public static CustomerDto Map(CustomerRequest.V1.Add entity)
                {
                    if (entity == null)
                        throw new ArgumentNullException("Entity cannot be null");

                    var returnVal = new CustomerDto
                    {
                        Name = entity.Name,
                        StatusId = entity.Status                      
                    };

                    return returnVal;
                }
            }
            
            public class Update
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public int Status { get; set; }
                public int CustId { get; set; }
                public IList<int> Accounts { get; set; }

                public static CustomerDto Map(CustomerRequest.V1.Update entity)
                {
                    if (entity == null)
                        throw new ArgumentNullException("Entity cannot be null");

                    var returnVal = new CustomerDto
                    {
                        Id = entity.Id,
                        Name = entity.Name,
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
        }
    }
}
