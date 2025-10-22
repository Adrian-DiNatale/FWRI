using FWRI.KeyCardReport.Data;
using FWRI.KeyCardReport.Web.Models.FacilityAccessReport;
using Microsoft.EntityFrameworkCore;

namespace FWRI.KeyCardReport.Web.Services
{
    /// <summary>
    /// Service to inject for accessing the report data for KeyCard Entries
    /// </summary>
    public interface IFacilityAccessReportService
    {
        FacilityAccessReportModel SearchFacilityAccess(AppDbContext context, FacilityAccessSearchFilterModel searchFilter);
    }


    public class FacilityAccessReportService : IFacilityAccessReportService
    {
        public FacilityAccessReportModel SearchFacilityAccess(AppDbContext context, FacilityAccessSearchFilterModel searchFilter)
        {
            FacilityAccessReportModel facilityAccessReportModel = new FacilityAccessReportModel();
            facilityAccessReportModel.SearchFilter = searchFilter;

            var keyCardEntries = context.KeyCardEntries
                .Include(kce => kce.EntryCategory)
                .Include(kce => kce.SecurityImage)
                    .ThenInclude(img => img.Category)
                .Include(kce => kce.Employee)
                    .ThenInclude(e => e.ProfilePicture)
                    .ThenInclude(img => img.Category)
                .Where(e => searchFilter.FirstName == null || (e.Employee != null && e.Employee.FirstName.Contains(searchFilter.FirstName, StringComparison.InvariantCultureIgnoreCase)))
                .Where(e => searchFilter.LastName == null || (e.Employee != null && e.Employee.LastName.Contains(searchFilter.LastName, StringComparison.InvariantCultureIgnoreCase)))
                .Where(e => searchFilter.WorkEmail == null || (e.Employee != null && e.Employee.WorkEmail.Contains(searchFilter.WorkEmail, StringComparison.InvariantCultureIgnoreCase)))
                .Where(e => searchFilter.WorkTitle == null || (e.Employee != null && e.Employee.WorkTitle.Contains(searchFilter.WorkTitle, StringComparison.InvariantCultureIgnoreCase)))
                .Where(e => searchFilter.EntryDateTimeBegin == null || e.EntryDateTime >= searchFilter.EntryDateTimeBegin)
                .Where(e => searchFilter.EntryDateTimeEnd == null || e.EntryDateTime <= searchFilter.EntryDateTimeEnd)
                .Where(e => searchFilter.KeyCardId == null || e.KeyCardId.ToString().Contains(searchFilter.KeyCardId))
                .Where(e => searchFilter.ScannerId == null || e.ScannerId.ToString().Contains(searchFilter.ScannerId));
                
            // Doesn't currently work for mapped table columns so resorting to a less desirable ordering logic
            //.OrderByString(searchFilter.SortColumn, searchFilter.SortColumnDir);

            //Get the total count of the records before paging
            facilityAccessReportModel.TotalRecords = keyCardEntries.Count();

            // Order the data
            if (searchFilter.SortColumnDir == "asc")
            {
                switch (searchFilter.SortColumn)
                {
                    case "EntryDateTime":
                        keyCardEntries = keyCardEntries.OrderBy(e => e.EntryDateTime);
                        break;
                    case "EmployeeFullName":
                        keyCardEntries = keyCardEntries.OrderBy(e => e.Employee!.FirstName).ThenBy(e => e.Employee!.LastName);
                        break;
                    case "CategoryName":
                        keyCardEntries = keyCardEntries.OrderBy(e => e.EntryCategory!.categoryName);
                        break;
                    case "KeyCardId":
                        keyCardEntries = keyCardEntries.OrderBy(e => e.KeyCardId);
                        break;
                    case "ScannerId":
                        keyCardEntries = keyCardEntries.OrderBy(e => e.ScannerId);
                        break;
                    default:
                        keyCardEntries = keyCardEntries.OrderBy(e => e.EntryDateTime);
                        break;
                }   
            }
            else
            {
                switch (searchFilter.SortColumn)
                {
                    case "EntryDateTime":
                        keyCardEntries = keyCardEntries.OrderByDescending(e => e.EntryDateTime);
                        break;
                    case "EmployeeFullName":
                        keyCardEntries = keyCardEntries.OrderByDescending(e => e.Employee!.FirstName).ThenByDescending(e => e.Employee!.LastName);
                        break;
                    case "CategoryName":
                        keyCardEntries = keyCardEntries.OrderByDescending(e => e.EntryCategory!.categoryName);
                        break;
                    case "KeyCardId":
                        keyCardEntries = keyCardEntries.OrderByDescending(e => e.KeyCardId);
                        break;
                    case "ScannerId":
                        keyCardEntries = keyCardEntries.OrderByDescending(e => e.ScannerId);
                        break;
                    default:
                        keyCardEntries = keyCardEntries.OrderByDescending(e => e.EntryDateTime);
                        break;
                }
            }
                
                     
            // Only return the paged data
            facilityAccessReportModel.KeyCardEntries = keyCardEntries
                                                    .Skip(searchFilter.Start)
                                                    .Take(searchFilter.Length)
                                                    .ToList();

            return facilityAccessReportModel;

        }
    }
}
