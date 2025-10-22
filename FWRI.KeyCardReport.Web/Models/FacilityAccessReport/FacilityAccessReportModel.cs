using FWRI.KeyCardReport.Data.Entities;

namespace FWRI.KeyCardReport.Web.Models.FacilityAccessReport
{
    /// <summary>
    /// Model used for display in the reporting view
    /// </summary>
    public class FacilityAccessReportModel
    {       
        public FacilityAccessSearchFilterModel SearchFilter { get; set; }
        public List<KeyCardEntry> KeyCardEntries { get; set; }
        public int TotalRecords { get; set; }

        public FacilityAccessReportModel()
        {
            SearchFilter = new FacilityAccessSearchFilterModel();
            KeyCardEntries = new List<KeyCardEntry>();
            TotalRecords = 0;
        }
    }
}
