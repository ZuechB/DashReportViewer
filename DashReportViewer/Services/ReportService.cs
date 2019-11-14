using DashReportViewer.Attributes;
using DashReportViewer.Models.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DashReportViewer.Services
{
    public interface IReportService
    {
        Task<IReport> RunReport(Guid id, Dictionary<string, object> paramsList, dynamic Id = null);
    }

    public class ReportService : IReportService
    {
        public async Task<IReport> RunReport(Guid id, Dictionary<string, object> paramsList, dynamic Id = null)
        {
            var report = GetReport(id);
            var instance = (IReport)Activator.CreateInstance(report.ReportType, paramsList, this);

            await instance.Run();
            if (Id != null)
            {
                instance.RowClick(Id);
            }

            return instance;
        }

        public Report GetReport(Guid id)
        {
            return GetReports().Where(r => r.Id == id).FirstOrDefault();
        }

        public IList<Report> GetReports(long? UserId = null)
        {
            var reports = new List<Report>();

            var reportTypes = GetReportTypesInNamespace(AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("DashReportViewer")));

            foreach (var report in reportTypes)
            {
                var reportAttribute = report.GetCustomAttribute<ReportNameAttribute>();
                var Id = Guid.Parse(reportAttribute.ReportId);

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
                    ReportType = reportAttribute.ReportType,
                    Description = reportAttribute.Description,
                    IsFavorite = isFavorite
                });
            }
            return reports.OrderBy(r => r.Name).ToList();
        }

        private Type[] GetReportTypesInNamespace(IEnumerable<Assembly> assemblies)
        {
            // First load Orbose Reports.
            var reports = assemblies.SelectMany(s => s.GetTypes())
                             .Where(c => typeof(IReport).IsAssignableFrom(c) && c.IsClass && c.Namespace == "DashReportViewer.Reports")
                             .ToArray();

            return reports;
        }
    }
}
