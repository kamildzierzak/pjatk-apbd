namespace Exercise10.API.DTO;

public class PaginatedResult<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IList<T> Items { get; set; }
}
