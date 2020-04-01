using System;

namespace DashReportViewer.Stripe.Models
{
    public class StripeTransaction
    {
        public string Id { get; set; }
        public bool Paid { get; set; }
        public decimal ApplicationFeeAmount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public decimal AmountRefunded { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public string FailureCode { get; set; }
        public string FailureMessage { get; set; }
        public decimal TotalAmount { get; set; }
    }
}