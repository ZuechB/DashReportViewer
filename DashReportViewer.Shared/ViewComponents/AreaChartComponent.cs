using DashReportViewer.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.ViewComponents
{
    [ViewComponent(Name = "AreaChartComponent")]
    public class AreaChartComponent : ViewComponent
    {
        readonly AppSettings appSettings;
        public AreaChartComponent(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var companyName = await Task.Run(() =>
            {
                return appSettings.CompanyName;
            });

            return View();
        }
    }
}
