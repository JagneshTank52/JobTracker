using System.Linq.Expressions;

namespace TestAssignment.Repository.Interface;

public interface IGenericRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync( bool isOrderByDesc = false, Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null,Func<IQueryable<T>, IQueryable<T>>? include = null);

    public Task<IEnumerable<TResult>> GetAllDtoAsync<TResult>(bool isOrderByDesc = false,Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null,  Func<IQueryable<T>, IQueryable<T>>? include = null);

    public Task<(IEnumerable<TResult> records, int TotalRecords)> GetPagedRecordsDtoAsync<TResult>(
    int pageSize,
    int pageIndex,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IQueryable<T>>? include = null
    );
    public Task<(IEnumerable<T> records, int TotalRecords)> GetPagedRecordsAsync(
    int pageSize,
    int pageIndex,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IQueryable<T>>? include = null);

    public Task<T?> GetByIdAsync(int id);

    public Task<TResult?> GetByIdDtoAsync<TResult>(int id);

    public Task AddAsync(T entity);

    public Task AddRangeAsync(IEnumerable<T> entities);

    public void Update(T entity);

    public void UpdateRange(IEnumerable<T> entities);
}
