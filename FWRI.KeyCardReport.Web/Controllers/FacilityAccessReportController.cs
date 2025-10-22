using FWRI.KeyCardReport.Data;
using FWRI.KeyCardReport.Library.Interfaces;
using FWRI.KeyCardReport.Web.Models.FacilityAccessReport;
using FWRI.KeyCardReport.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FWRI.KeyCardReport.Web.Controllers
{
    /// <summary>
    /// The report controller using injected services
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class FacilityAccessReportController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAppSettings _appSettings;
        private IFacilityAccessReportService _facilityAccessReportService;
        private readonly AppDbContext _context;

        public FacilityAccessReportController(ILogger<HomeController> logger, IAppSettings appSettings, AppDbContext context, IFacilityAccessReportService facilityAccessReportService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _context = context;
            _facilityAccessReportService = facilityAccessReportService;
        }

        /// <summary>
        /// Determines whether the request is ajax or not
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is ajax request]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAjaxRequest()
        {
            return Url.ActionContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>
        /// We're not returning any table data on page load.
        /// Instead, the DataTables plugin will request the current filtered,paged and sorted
        /// data through an ajax request.
        /// Returning JSON data to be used with DataTables.
        /// </summary>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index([FromQuery] FacilityAccessSearchFilterModel searchFilter)
        {
            var reportModel = new FacilityAccessReportModel();
            reportModel.SearchFilter = searchFilter ?? new FacilityAccessSearchFilterModel();


            if (IsAjaxRequest())
            {
                reportModel = _facilityAccessReportService.SearchFacilityAccess(_context, searchFilter ?? new FacilityAccessSearchFilterModel());

                var data = new
                {
                    data = reportModel.KeyCardEntries,
                    start = reportModel.SearchFilter.Start,
                    length = reportModel.SearchFilter.Length,
                    recordsTotal = int.Parse(reportModel.TotalRecords.ToString()),
                    iTotalDisplayRecords = int.Parse(reportModel.TotalRecords.ToString()) //shows the total for pagination and total count in table
                };
                return Json(data);                
            }

            // Render the whole page with search filters populated from reportModel.SearchFilter
            return View("~/Views/FacilityAccessReport/Index.cshtml", reportModel);
        }
    }
}
