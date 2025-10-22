using FWRI.KeyCardReport.Library.Utilities;

namespace FWRI.KeyCardReport.Library.Helpers
{
    /// <summary>
    /// Basic model to help with paging on a grid
    /// </summary>
    public class PagingHelper
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDir { get; set; }


        public PagingHelper()
        {
            Length = ConfigurationUtility.DefaultPageSize;
            Start = 0;
            SortColumn = "0";
            SortColumnDir = "asc";
        }
    }
}
