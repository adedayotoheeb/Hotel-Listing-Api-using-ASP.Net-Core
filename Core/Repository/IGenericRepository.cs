using HotelListing.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.Core.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync( Expression<Func<T, bool>> expression = null, Func<IQueryable<T> , IOrderedQueryable<T>> orderBy = null, List<string> includes = null);
        Task<IPagedList<T>> GetAllPagedAsync(RequestParams requestParams, List<string> includes = null  );
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, List<string> includes = null);
        Task InsertAsync(T entity);    
        Task InsertRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        Task <bool> Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
    }
}
