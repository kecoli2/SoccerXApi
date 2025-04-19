using SoccerX.Common.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, Expression<Func<T, T>>? selector = null);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Expression<Func<T, T>>? selector = null, string[]? includeProperties = null, bool tracking = false);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Expression<Func<T, T>>? selector = null, string[]? includeProperties = null, bool tracking = false, CancellationToken cancellationToken = default);
    Task AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(List<T> entities);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>>? selector = null, bool tracking = false);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, T>>? selector = null, bool tracking = false);

    /// <summary>
    /// Offset-based paging metodu
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, T>>? selector, int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool tracking = false);

    /// <summary>
    /// Cursor-based paging metodu
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="predicate"></param>
    /// <param name="lastCursor"></param>
    /// <param name="pageSize"></param>
    /// <param name="cursorSelector"></param>
    /// <returns></returns>
    Task<CursorPagedResult<T, TCursor>> GetPagedByCursorAsync<TCursor>(Expression<Func<T, bool>>? predicate, Expression<Func<T, T>>? selector, TCursor? lastCursor, int pageSize, Expression<Func<T, TCursor>> cursorSelector, bool tracking = false) where TCursor : IComparable;

    /// <summary>
    /// Cursor-based paging metodu CreateDate ve UUID
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="lastCursor"></param>
    /// <param name="pageSize"></param>
    /// <param name="createDateFieldName"></param>
    /// <param name="idFieldName"></param>
    /// <returns></returns>
    public Task<CursorPagedResult<T, CompositeCursorGuid>> GetPagedByCompositeCursorAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, T>>? selector, CompositeCursorGuid? lastCursor, int pageSize, string createDateFieldName = "Createdate", string idFieldName = "Id", bool tracking = false);

    /// <summary>
    /// Db Insert,Update,Delete
    /// </summary>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}