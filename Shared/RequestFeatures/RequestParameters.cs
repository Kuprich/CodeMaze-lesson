namespace Shared.RequestFeatures;

public abstract class RequestParameters
{
    const int _maxPageSize = 50;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = (value < _maxPageSize) ? value : _maxPageSize;
        }
    }

    public string? OrderBy { get; set; }
}



