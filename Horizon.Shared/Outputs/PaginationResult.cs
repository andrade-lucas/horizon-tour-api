namespace Horizon.Shared.Outputs;

public class PaginationResult<T> where T : class
{
    public int TotalRows { get; private set; }
    public int RowsOnPage { get; private set; }
    public ICollection<T> Rows { get; private set; }

    public PaginationResult(int totalRows, int rowsOnPage, ICollection<T> rows)
    {
        TotalRows = totalRows;
        RowsOnPage = rowsOnPage;
        Rows = rows;
    }
}
