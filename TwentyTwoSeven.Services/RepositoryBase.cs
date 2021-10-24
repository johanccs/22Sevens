using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.DataContext;

namespace TwentyTwoSeven.Services
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        #region Properties

        protected RepoContext RepoContext { get; set; }

        #endregion

        #region Constructor

        public RepositoryBase(RepoContext repoContext)
        {
            RepoContext = repoContext;
        }

        #endregion

        #region Methods

        public async Task CreateAsync(T entity)
        {
            await RepoContext.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(RepoContext.Set<T>().Update(entity));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.FromResult(RepoContext.Set<T>().Remove(entity));
        }

        public async Task<IQueryable<T>> FindAllAsync()
        {
            return await Task.FromResult(RepoContext.Set<T>().AsNoTracking());
        }

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(RepoContext.Set<T>().Where(expression).AsNoTracking());
        }
     
        #endregion
    }
}
