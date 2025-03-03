namespace Goal.Infra.Crosscutting.Collections;

public interface IPageSearch
{
    int PageIndex { get; }
    int PageSize { get; }
}
