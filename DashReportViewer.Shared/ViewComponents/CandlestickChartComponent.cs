using DashReportViewer.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.ViewComponents
{
    [ViewComponent(Name = "CandlestickChartComponent")]
    public class CandlestickChartComponent : ViewComponent
    {
        readonly AppSettings appSettings;
        public CandlestickChartComponent(IOptions<AppSettings> appSettings)
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
