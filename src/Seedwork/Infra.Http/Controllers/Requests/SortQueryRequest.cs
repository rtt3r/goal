using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Http.Controllers.Requests
{
    public class SortQueryRequest
    {
        /// <summary>
        /// The field name to sort
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// The sorting orientation
        /// </summary>
        public SortDirection Direction { get; set; }
    }
}
