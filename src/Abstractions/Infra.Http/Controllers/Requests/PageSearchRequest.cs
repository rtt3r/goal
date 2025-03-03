namespace Goal.Infra.Http.Controllers.Requests;

public sealed class PageSearchRequest
{
    /// <summary>
    /// The required page index (starts at zero)
    /// </summary>
    public int PageIndex { get; set; } = 0;

    /// <summary>
    /// The number of page items
    /// </summary>
    public int PageSize { get; set; } = int.MaxValue;
}
