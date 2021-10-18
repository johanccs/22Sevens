using System;
using System.Collections.Generic;
using System.Text;

namespace TwentyTwoSeven.Contracts
{
    public interface IAccountService<TEntity>
    {
        TEntity Add(TEntity entity);

        TEntity Update();


    }
}
