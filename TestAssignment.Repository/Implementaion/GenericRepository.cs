using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TestAssignment.Entity.Data;
using TestAssignment.Repository.Interface;

namespace TestAssignment.Repository.Implementaion;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TestAssignmentContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly IMapper _mapper;
    public GenericRepository(TestAssignmentContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _mapper = mapper;
    }

    // Get all 
    public async Task<IEnumerable<T>> GetAllAsync(bool isOrderByDesc = false,Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        //Apply filter
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply orderby
        if (orderBy != null)
        {
            if (isOrderByDesc)
            {
                query = query.OrderByDescending(orderBy);
            }
            else
            {
                query = query.OrderBy(orderBy);
            }
        }

        // Apply Include 
        if (include != null)
        {
            query = include(query);
        }

        return await query.ToListAsync();
    }

    //Get all record of only DTO
    public async Task<IEnumerable<TResult>> GetAllDtoAsync<TResult>(bool isOrderByDesc = false,Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        //Apply filter
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply orderby
        if (orderBy != null)
        {
            if (isOrderByDesc)
            {
                query = query.OrderByDescending(orderBy);
            }
            else
            {
                query = query.OrderBy(orderBy);
            }
        }

        // Apply Include 
        if (include != null)
        {
            query = include(query);
        }

        // Select DTO
        IEnumerable<TResult> resultQuery = await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();

        return resultQuery;
    }

    //Get paged record of only DTO
    public async Task<(IEnumerable<T> records, int TotalRecords)> GetPagedRecordsAsync(
    int pageSize,
    int pageIndex,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IQueryable<T>>? include = null
    )
    {
        IQueryable<T> query = _dbSet;

        if (orderBy == null)
        {
            throw new ArgumentNullException(nameof(orderBy), "Ordering function cannot be null.");
        }

        // Apply fillter 
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply Include
        if (include != null)
        {
            query = include(query);
        }

        int totalRecord = query.Count();

        // Manage pagination
        if (totalRecord != 0 && totalRecord % pageSize == 0 && pageIndex > totalRecord / pageSize)
        {
            pageIndex--;
        }

        IEnumerable<T> resultQuery = await orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return (resultQuery, totalRecord);
    }

    //Get paged record 
    public async Task<(IEnumerable<TResult> records, int TotalRecords)> GetPagedRecordsDtoAsync<TResult>(
    int pageSize,
    int pageIndex,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IQueryable<T>>? include = null
    )
    {
        IQueryable<T> query = _dbSet;

        if (orderBy == null)
        {
            throw new ArgumentNullException(nameof(orderBy), "Ordering function cannot be null.");
        }

        // Apply fillter 
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply Include
        if (include != null)
        {
            query = include(query);
        }

        int totalRecord = query.Count();

        // Manage pagination
        if (totalRecord != 0 && totalRecord % pageSize == 0 && pageIndex > totalRecord / pageSize)
        {
            pageIndex--;
        }

        IEnumerable<TResult> resultQuery = await orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize).ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();

        return (resultQuery, totalRecord);
    }

    // Get by id
    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    // Get by id only DTO
    public async Task<TResult?> GetByIdDtoAsync<TResult>(int id)
    {
        try
        {
            T? entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TResult>(entity);
        }
        catch (Exception)
        {
            throw new NotImplementedException();
        }
    }

    // Add Async
    public async Task AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
        }
        catch (Exception)
        {
            throw new NotImplementedException();
        }
    }

    // Add Range Async
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        try
        {
            await _dbSet.AddRangeAsync(entities);
        }
        catch (Exception)
        {
            throw new NotImplementedException();
        }
    }

    // Update Async
    public void Update(T entity)
    {
        try
        {
            _dbSet.Update(entity);
        }
        catch (Exception)
        {
            throw new NotImplementedException();
        }
    }

    // Update Range Async
    public void UpdateRange(IEnumerable<T> entities)
    {
        try
        {
            _dbSet.UpdateRange(entities);
        }
        catch (Exception)
        {
            throw new NotImplementedException();
        }
    }
}
