using DashReportViewer.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DashReportViewer.ViewComponents
{
    [ViewComponent(Name = "CompanyHeaderComponent")]
    public class CompanyHeaderViewComponent : ViewComponent
    {
        readonly DashReportAppSettings appSettings;
        public CompanyHeaderViewComponent(IOptions<DashReportAppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var companyName = await Task.Run(() =>
            {
                return appSettings.CompanyName;
            });

            return View("Default", companyName);
        }
    }
}