using DashReportViewer.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.ViewComponents
{
    [ViewComponent(Name = "BarChartComponent")]
    public class BarChartComponent : ViewComponent
    {
        readonly AppSettings appSettings;
        public BarChartComponent(IOptions<AppSettings> appSettings)
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
