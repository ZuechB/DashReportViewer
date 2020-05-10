using DashReportViewer.Stripe.Models;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using CoreBackpack.cMath;

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
            StripeList<Charge> charges = await service.ListAsync(
              options
            );

            return charges.Data.Select(c => new StripeTransaction()
            {
                Id = c.Id,
                Paid = c.Paid,
                ApplicationFeeAmount = MoneyExtender.ConvertToDollars(c.ApplicationFeeAmount != null ? c.ApplicationFeeAmount.Value : 0),
                Status = c.Status,
                Description = c.Description,
                PaymentType = c.Object,
                AmountRefunded = MoneyExtender.ConvertToDollars(c.AmountRefunded),
                Amount = MoneyExtender.ConvertToDollars(c.Amount),
                Created = c.Created,
                FailureCode = c.FailureCode,
                FailureMessage = c.FailureMessage,
                TotalAmount = MoneyExtender.ConvertToDollars(c.Amount) - MoneyExtender.ConvertToDollars(c.AmountRefunded)

            }).ToList();
        }
    }
}