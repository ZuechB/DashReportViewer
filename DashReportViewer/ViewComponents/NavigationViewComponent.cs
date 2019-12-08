using DashReportViewer.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
                return reportService.GetReports(AppDomain.CurrentDomain);
            });
            
            return View(reports);
        }
    }
}
