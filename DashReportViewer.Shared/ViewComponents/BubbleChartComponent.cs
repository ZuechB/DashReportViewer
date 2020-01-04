using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.ReportComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.ViewComponents
{
    [ViewComponent(Name = "BubbleChartComponent")]
    public class BubbleChartComponent : ViewComponent
    {
        readonly AppSettings appSettings;
        public BubbleChartComponent(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(BaseReportReportComponent baseReport)
        {
            var companyName = await Task.Run(() =>
            {
                return appSettings.CompanyName;
            });

            return View(baseReport);
        }
    }
}
