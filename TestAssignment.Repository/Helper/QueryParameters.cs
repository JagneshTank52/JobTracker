namespace TestAssignment.Repository.Helper;

public class QueryParameters
{
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public string SortOrder { get; set; } = "asc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

}
