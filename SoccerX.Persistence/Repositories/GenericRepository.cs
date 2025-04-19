using Microsoft.EntityFrameworkCore;
using SoccerX.Persistence.Context;
using System.Linq.Expressions;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Common.Shared.Model;

namespace SoccerX.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Field
        protected readonly SoccerXDbContext Context;
        private readonly DbSet<T> _dbSet;
        #endregion

        #region Constructor
        public GenericRepository(SoccerXDbContext context)
        {
            Context = context;
            _dbSet = Context.Set<T>();
        }

        #endregion

        #region Public Method
        public async Task<T?> GetByIdAsync(Guid id, Expression<Func<T, T>>? selector = null)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Expression<Func<T, T>>? selector = null, string[]? includeProperties = null, bool tracking = false)
        {
            var query = tracking ? _dbSet : _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (selector != null)
                query = query.Select(selector);

            if (includeProperties != null)
                query = includeProperties.Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Expression<Func<T, T>>? selector = null, string[]? includeProperties = null, bool tracking = false, CancellationToken cancellationToken = default)
        {
            var query = tracking ? _dbSet : _dbSet.AsNoTracking();

            // Filtreleme
            query = query.Where(predicate);

            // Include işlemleri
            if (includeProperties is { Length: > 0 })
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            // Sıralama
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            //Sadece Cekilecek Fieldlar
            if (selector != null)
                query = query.Select(selector);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>>? selector = null, bool tracking = false)
        {
            var query = tracking ? _dbSet : _dbSet.AsNoTracking();
            query = query.Where(predicate);
            if (selector != null)
                query = query.Select(selector);

            return await query.AnyAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, T>>? selector = null, bool tracking = false)
        {
            var query = tracking ? _dbSet : _dbSet.AsNoTracking();
            if(predicate != null)
                query = query.Where(predicate);
            if(selector != null)
                query = query.Select(selector);

            return await query.CountAsync();
        }

        // Offset-based paging metodu
        public async Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, T>>? selector, int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool tracking = false)
        {
            var query = tracking ? _dbSet : _dbSet.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);
            if(selector != null)
                query = query.Select(selector);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
        }

        // Cursor-based paging metodu
        public async Task<CursorPagedResult<T, TCursor>> GetPagedByCursorAsync<TCursor>(Expression<Func<T, bool>>? predicate, Expression<Func<T, T>>? selector, TCursor? lastCursor, int pageSize, Expression<Func<T, TCursor>> cursorSelector, bool tracking = false) where TCursor : IComparable
        {
            var query = tracking ? _dbSet : _dbSet.AsNoTracking();

            // Filtre uygulanıyorsa ekleyelim
            if (predicate != null)
                query = query.Where(predicate);

            // Eğer son cursor değeri verilmişse, bu değerden büyük kayıtları getir.
            if (lastCursor != null)
            {
                // Oluşturduğumuz yardımcı metodla: x => cursorSelector(x) > lastCursor
                query = query.Where(BuildGreaterThanExpression(cursorSelector, lastCursor));
            }

            if (selector != null)
                query = query.Select(selector);

            // Sorguyu cursor alanına göre sıralıyoruz (artan sırayla)
            query = query.OrderBy(cursorSelector);

            // İstenen sayıda veriyi alıyoruz
            var items = await query.Take(pageSize).ToListAsync();

            // Son kaydın cursor değerini belirleyelim
            var newCursor = items.Any() ? cursorSelector.Compile()(items.Last()) : default;

            return new CursorPagedResult<T, TCursor>(items, newCursor);
        }

        // Yardımcı metot: Seçilen cursor alanının lastCursor değerinden büyük olup olmadığını kontrol eden Expression oluşturur.
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
        private Expression<Func<T, bool>> BuildGreaterThanExpression<T, TCursor>(Expression<Func<T, TCursor>> selector, TCursor lastCursor) where TCursor : IComparable
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        {
            var parameter = selector.Parameters[0];
            var property = selector.Body;
            var constant = Expression.Constant(lastCursor, typeof(TCursor));
            var greaterThan = Expression.GreaterThan(property, constant);
            return Expression.Lambda<Func<T, bool>>(greaterThan, parameter);
        }

        public async Task<CursorPagedResult<T, CompositeCursorGuid>> GetPagedByCompositeCursorAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, T>>? selector, CompositeCursorGuid? lastCursor, int pageSize, string createDateFieldName = "Createdate", string idFieldName = "Id", bool tracking = false)
        {
            var query = tracking ? _dbSet : _dbSet.AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (selector != null)
                query = query.Select(selector);

            // Eğer lastCursor sağlanmışsa, bu cursor'dan sonraki kayıtları getir:
            if (lastCursor != null)
            {
                query = query.Where(u =>
                    EF.Property<DateTime>(u, createDateFieldName) > lastCursor.CreateDate ||
                    (EF.Property<DateTime>(u, createDateFieldName) == lastCursor.CreateDate &&
                     EF.Property<Guid>(u, idFieldName) > lastCursor.Id));
            }

            // Sıralama: önce CreateDate, sonra Id
            query = query.OrderBy(u => EF.Property<DateTime>(u, createDateFieldName))
                .ThenBy(u => EF.Property<Guid>(u, idFieldName));

            var items = await query.Take(pageSize).ToListAsync();

            var newCursor = items.Any()
                ? new CompositeCursorGuid
                {
                    CreateDate = (DateTime)typeof(T).GetProperty(createDateFieldName)!.GetValue(items.Last())!,
                    Id = (Guid)typeof(T).GetProperty(idFieldName)!.GetValue(items.Last())!
                }
                : null;

            return new CursorPagedResult<T, CompositeCursorGuid>(items, newCursor);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Private Method
        #endregion
    }
}
