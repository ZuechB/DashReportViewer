using DashReportViewer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.ViewComponents
{
    [ViewComponent(Name = "NavigationComponent")]
    public class NavigationViewComponent : ViewComponent
    {
        readonly IReportService reportService;
        public NavigationViewComponent(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reports = await Task.Run(() =>
            {
                return reportService.GetReports();
            });
            
            return View(reports);
        }
    }
}
