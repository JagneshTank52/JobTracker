namespace TestAssignment.Repository.Helper;

public class PaginatedList<T> : List<T>
{
    public List<T> Items { get; set; }
    public QueryParameters queryParameters {get; private set;}
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }
    public int FromCount => (queryParameters.PageNumber - 1) * queryParameters.PageSize;
    public int ToCount => FromCount + queryParameters.PageSize;
    public bool HasPreviousPage => queryParameters.PageNumber > 1;
    public bool HasNextPage => queryParameters.PageNumber < TotalPages;

    public PaginatedList(IEnumerable<T> items, int totalCount, QueryParameters PageQuery)
    {
        Items = items.ToList();
        queryParameters = PageQuery;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageQuery.PageSize);
    }
}

