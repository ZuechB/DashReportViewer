﻿using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.ReportComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.ViewComponents
{
    [ViewComponent(Name = "TextComponent")]
    public class TextComponent : ViewComponent
    {
        readonly DashReportAppSettings appSettings;
        public TextComponent(IOptions<DashReportAppSettings> appSettings)
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
