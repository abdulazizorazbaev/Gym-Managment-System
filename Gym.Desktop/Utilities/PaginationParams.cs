namespace Gym.Desktop.Utilities;

public class PaginationParams
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int SkipCount { get
        {
            return (PageNumber - 1) * PageSize;
        }
    }

    public PaginationParams(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
    }

    public PaginationParams()
    {

    }
}