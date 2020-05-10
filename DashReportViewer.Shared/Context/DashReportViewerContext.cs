using DashReportViewer.AzureDevOps.Models;
using DashReportViewer.Models;
using DashReportViewer.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DashReportViewer.Context
{
    public class DashReportViewerContext : IdentityDbContext<ApplicationUser, Role, long>
    {
        public DashReportViewerContext(DbContextOptions<DashReportViewerContext> options) : base(options) { }

        public DbSet<AzureDevOp> AzureDevOps { get; set; }
        public DbSet<CompanyReport> Reports { get; set; }
    }
}