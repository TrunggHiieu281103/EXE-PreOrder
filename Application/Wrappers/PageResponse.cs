namespace Application.Wrappers;

public class PageResponse<T> : BaseResponse<T>
{
   public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public bool PreviousPage => PageNumber > 1;
    public bool NextPage => PageNumber < TotalPages;

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public PageResponse(T data, int pageNumber, int pageSize, int totalItems)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
        Data = data;
        Message = null;
        Succeeded = true;
        Errors = null;
    }
}