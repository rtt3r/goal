namespace Goal.Seedwork.Infra.Crosscutting.Collections
{
    public interface ISearchQuery
    {
        int PageIndex { get; }
        int PageSize { get; }
        public string SortBy { get; }
        public SortDirection SortDirection { get; }
    }
}