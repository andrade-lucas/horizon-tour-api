namespace Horizon.Shared.Outputs;

public class PaginationResult<T> where T : class
{
    public int TotalRows { get; set; }
    public int RowsOnPage { get; set; }
    public ICollection<T> Rows { get; set; }
}
