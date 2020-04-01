using DashReportViewer.Stripe.Models;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NetCoreBackPack.Currency;
using Microsoft.Extensions.Options;

namespace DashReportViewer.Stripe
{
    public interface IStripeService
    {
        Task<List<StripeTransaction>> GetListOfTransactions();
    }

    public class StripeService : IStripeService
    {
        readonly StripeSettings appSettings;
        public StripeService(IOptions<StripeSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task<List<StripeTransaction>> GetListOfTransactions()
        {
            var transactions = new List<StripeTransaction>();

            var stripeClient = new StripeClient(appSettings.ApiSecret);

            var options = new ChargeListOptions { Limit = 100 };
            var service = new ChargeService(stripeClient);
            StripeList<Charge> charges = service.List(
              options
            );

            return charges.Data.Select(c => new StripeTransaction()
            {
                Id = c.Id,
                Paid = c.Paid,
                ApplicationFeeAmount = Dollars.CentsToDollar(c.ApplicationFeeAmount != null ? c.ApplicationFeeAmount.Value : 0),
                Status = c.Status,
                Description = c.Description,
                PaymentType = c.Object,
                AmountRefunded = Dollars.CentsToDollar(c.AmountRefunded),
                Amount = Dollars.CentsToDollar(c.Amount),
                Created = c.Created,
                FailureCode = c.FailureCode,
                FailureMessage = c.FailureMessage,
                TotalAmount = Dollars.CentsToDollar(c.Amount) - Dollars.CentsToDollar(c.AmountRefunded)

            }).ToList();
        }
    }
}