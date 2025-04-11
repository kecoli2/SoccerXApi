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
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
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

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }

        // Offset-based paging metodu
        public async Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>>? predicate, int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
        }

        // Cursor-based paging metodu
        public async Task<CursorPagedResult<T, TCursor>> GetPagedByCursorAsync<TCursor>(Expression<Func<T, bool>>? predicate, TCursor? lastCursor, int pageSize, Expression<Func<T, TCursor>> cursorSelector) where TCursor : IComparable
        {
            IQueryable<T> query = _dbSet;

            // Filtre uygulanıyorsa ekleyelim
            if (predicate != null)
                query = query.Where(predicate);

            // Eğer son cursor değeri verilmişse, bu değerden büyük kayıtları getir.
            if (lastCursor != null)
            {
                // Oluşturduğumuz yardımcı metodla: x => cursorSelector(x) > lastCursor
                query = query.Where(BuildGreaterThanExpression(cursorSelector, lastCursor));
            }

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

        public async Task<CursorPagedResult<T, CompositeCursorGuid>> GetPagedByCompositeCursorAsync(Expression<Func<T, bool>>? predicate, CompositeCursorGuid? lastCursor, int pageSize, string createDateFieldName = "Createdate", string idFieldName = "Id")
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

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

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        #endregion

        #region Private Method
        #endregion
    }
}
