using DashReportViewer.Stripe.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DashReportViewer.Stripe
{
    public interface IStripeService
    {
        Task<List<StripeTransaction>> GetListOfTransactions();
    }

    public class StripeService : IStripeService
    {
        public StripeService()
        {

        }

        public async Task<List<StripeTransaction>> GetListOfTransactions()
        {
            var transactions = new List<StripeTransaction>();

            var stripeClient = new StripeClient("");

            var options = new ChargeListOptions { Limit = 3 };
            var service = new ChargeService(stripeClient);
            StripeList<Charge> charges = service.List(
              options
            );

            return charges.Data.Select(c => new StripeTransaction
            {
                id = c.Id,
                paid = c.Paid,
                amount = c.Amount,
                created = c.Created,
                failure_code = c.FailureCode,
                failure_message = c.FailureMessage

            }).ToList();
        }

        //public void CopyProperties(object objSource, object objDestination)
        //{
        //    //get the list of all properties in the destination object
        //    var destProps = objDestination.GetType().GetProperties();

        //    //get the list of all properties in the source object
        //    foreach (var sourceProp in objSource.GetType().GetProperties())
        //    {
        //        foreach (var destProperty in destProps)
        //        {
        //            //if we find match between source & destination properties name, set
        //            //the value to the destination property
        //            if (destProperty.Name == sourceProp.Name &&
        //                    destProperty.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
        //            {
        //                destProperty.SetValue(destProps, sourceProp.GetValue(
        //                    sourceProp, new object[] { }), new object[] { });
        //                break;
        //            }
        //        }
        //    }
        //}

    }
}
