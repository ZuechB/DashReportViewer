using DashReportViewer.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.Startup
{
    public class StaticFiles
    {
        public static StaticFileOptions UseDashReportViewer()
        {
            var DashReportViewerSharedAssembly = typeof(ReportController).GetTypeInfo().Assembly;
            var DashReportViewerSharedEmbeddedFileProvider = new EmbeddedFileProvider(
                DashReportViewerSharedAssembly,
                "DashReportViewer.Shared.wwwroot"
            );

            return new StaticFileOptions
            {
                FileProvider = DashReportViewerSharedEmbeddedFileProvider,
                RequestPath = new PathString("/external")
            };
        }
    }
}
