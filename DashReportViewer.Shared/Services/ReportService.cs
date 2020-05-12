using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DashReportViewer.Shared.Models;
using Microsoft.Extensions.Options;
using DashReportViewer.Context;
using Microsoft.EntityFrameworkCore;

namespace DashReportViewer.Shared.Services
{
    public interface IReportService
    {
        Task UpdateCode(Guid id, string code);
        Task<IReport> RunReport(AppDomain domain, Guid id, Dictionary<string, object> paramsList, dynamic Id = null);
        IList<Report> GetReports(AppDomain domain, long? UserId = null);
        T GetService<T>();
        Task<CompanyReport> GetCode(Guid id);
    }

    public class ReportService : IReportService
    {
        readonly DashReportAppSettings appSettings;
        readonly IServiceProvider serviceProvider;
        readonly DashReportViewerContext dashReportViewerContext;
        public ReportService(IServiceProvider serviceProvider, IOptions<DashReportAppSettings> appSettings, DashReportViewerContext dashReportViewerContext)
        {
            this.serviceProvider = serviceProvider;
            this.appSettings = appSettings.Value;
            this.dashReportViewerContext = dashReportViewerContext;
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public async Task<IReport> RunReport(AppDomain domain, Guid id, Dictionary<string, object> paramsList, dynamic Id = null)
        {
            var report = GetReport(domain, id);

            var instance = (IReport)Activator.CreateInstance(report.ReportType, paramsList, this);

            await instance.Run();
            if (Id != null)
            {
                instance.RowClick(Id);
            }

            return instance;
        }

        public async Task UpdateCode(Guid id, string code)
        {
            var report = await dashReportViewerContext.Reports.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (report != null)
            {
                report.Code = code;

                await dashReportViewerContext.SaveChangesAsync();
            }
        }

        public async Task<CompanyReport> GetCode(Guid id)
        {
            return await dashReportViewerContext.Reports.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public Report GetReport(AppDomain domain, Guid id)
        {
            return GetReports(domain).Where(r => r.Id == id).FirstOrDefault();
        }

        public IList<Report> GetReports(AppDomain domain, long? UserId = null)
        {
            var reports = new List<Report>();
            var reportTypes = GetReportTypesInNamespace(domain.GetAssemblies().Where(a => a.FullName.Contains(appSettings.ProjectName)));

            foreach (var report in reportTypes)
            {
                var reportAttribute = report.GetCustomAttribute<ReportNameAttribute>();
                var Id = Guid.Parse(reportAttribute.ReportId);
                string icon = reportAttribute.Icon;

                bool isFavorite = false;
                //if (UserId != null)
                //{
                //    var favoriteReport = companyContext.UserFavoriteReports.Where(f => f.UserId == UserId && f.ReportId == Id).FirstOrDefault();
                //    if (favoriteReport != null)
                //    {
                //        isFavorite = true;
                //    }
                //}

                reports.Add(new Report()
                {
                    Id = Id,
                    Name = reportAttribute.ReportName,
                    ReportType = report,
                    Description = reportAttribute.Description,
                    IsFavorite = isFavorite,
                    Folder = reportAttribute.Folder,
                    Icon = icon
                });
            }
            return reports.OrderBy(r => r.Name).ToList();
        }

        private Type[] GetReportTypesInNamespace(IEnumerable<Assembly> assemblies)
        {
            var reports = assemblies.SelectMany(s => s.GetTypes())
                             .Where(c => typeof(IReport).IsAssignableFrom(c) && c.IsClass && c.Namespace == appSettings.ProjectName + ".Reports")
                             .ToArray();

            return reports;
        }
    }
}
