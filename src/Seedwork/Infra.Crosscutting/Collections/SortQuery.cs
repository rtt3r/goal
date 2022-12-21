namespace Goal.Seedwork.Infra.Crosscutting.Collections
{
    public class SortQuery
    {
        public SortQuery(string fieldName, SortDirection direction)
        {
            FieldName = fieldName;
            Direction = direction;
        }

        public string FieldName { get; private set; }
        public SortDirection Direction { get; private set; }
    }
}
