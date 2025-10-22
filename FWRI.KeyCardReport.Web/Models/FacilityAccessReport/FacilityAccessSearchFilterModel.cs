using FWRI.KeyCardReport.Library.Helpers;

namespace FWRI.KeyCardReport.Web.Models.FacilityAccessReport
{
    /// <summary>
    /// Model for the search filters
    /// </summary>
    /// <seealso cref="FWRI.KeyCardReport.Library.Helpers.PagingHelper" />
    public class FacilityAccessSearchFilterModel : PagingHelper
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkEmail { get; set; }
        public string WorkTitle { get; set; }          
        public string KeyCardId { get; set; }
        public DateTime? EntryDateTimeBegin { get; set; }
        public DateTime? EntryDateTimeEnd { get; set; }
        public string ScannerId { get; set; }

        public FacilityAccessSearchFilterModel()
        {
            SortColumn = "EntryDateTime";
            SortColumnDir = "desc";

            FirstName = string.Empty;
            LastName = string.Empty;
            WorkEmail = string.Empty;
            WorkTitle = string.Empty;
            KeyCardId = string.Empty;
            EntryDateTimeBegin = null;
            EntryDateTimeEnd = null;
            ScannerId = string.Empty;
        }
    }
}
