namespace Shared.Pagination;

public class PagedResult<TEntity>
{
    public IEnumerable<TEntity> Items { get; set; }
    public int TotalPages { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public long TotalItemsCount { get; set; }

    public PagedResult(List<TEntity> items, long totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}