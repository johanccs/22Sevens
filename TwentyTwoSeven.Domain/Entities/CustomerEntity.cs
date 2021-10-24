using System;
using TwentyTwoSeven.Domain.ValueObjects;

namespace TwentyTwoSeven.Domain.Entities
{
    public class CustomerEntity : BaseEntity
    {
        #region Fields

        public CustomerId _id;

        public string _name;

        public int _statusId;

        #endregion

        #region Constructor

        public CustomerEntity(CustomerId id)
        {
            _id = id;

            _statusId = 1;            
        }

        #endregion

        #region Methods

        protected override bool Validate()
        {
            throw new NotImplementedException();
        }

        public CustomerId GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be null or empty");

            _name = name;
        }

        public int GetStatusId()
        {
            return _statusId;
        }

        public void SetStatusId(int statusId)
        {
            if (statusId <= 0)
                throw new ArgumentNullException(nameof(statusId), "Status Id must have a positive value");

            _statusId = statusId;
        }

        #endregion
    }
}
