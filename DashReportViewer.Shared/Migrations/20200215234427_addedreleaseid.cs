using Microsoft.EntityFrameworkCore.Migrations;

namespace DashReportViewer.Shared.Migrations
{
    public partial class addedreleaseid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReleaseId",
                table: "AzureDevOps",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseId",
                table: "AzureDevOps");
        }
    }
}
