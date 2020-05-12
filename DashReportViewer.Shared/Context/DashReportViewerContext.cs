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
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Note>(entity =>
            {
                entity.HasKey(p => new { p.ReportId, p.CardId });
            });

            base.OnModelCreating(builder);
        }
    }
}