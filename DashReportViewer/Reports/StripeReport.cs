using Apolloio;
using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Stripe;

namespace DashReportViewer.Reports
{
    [ReportName("Stripe Report", "9011B0F5-11EE-424E-80F1-1D02C3FC89C3")]
    public class StripeReport : ReportEntity, IReport
    {
        readonly IStripeService stripeService;
        public StripeReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService)
        {
            stripeService = GetService<IStripeService>();
        }

        protected override async Task<IEnumerable<object>> Main()
        {
            var widgets = new List<Widget>();

            var transactions = await stripeService.GetListOfTransactions();


            widgets.Add(new Widget("Total Amount")
            {
                Content = new TextContent()
                {
                    Text = transactions.Sum(t => t.TotalAmount).ToString("C"),
                    FontSize = "30px",
                    HorizontalAlign = TextHorizontalAlign.Center,
                    VerticalAlign = TextVerticalAlign.Middle,
                    WidgetHeight = "200px"
                },
                Column = 3
            });


            widgets.Add(new Widget("Payment History")
            {
                Content = new TableContent()
                {
                    Content = transactions
                },
                Column = 12
            });

            return widgets;
        }
    }
}
