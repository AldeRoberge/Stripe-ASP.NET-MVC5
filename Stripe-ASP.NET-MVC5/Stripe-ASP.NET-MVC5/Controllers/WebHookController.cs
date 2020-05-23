using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Stripe;

namespace Stripe_ASP.NET_MVC5.Controllers
{

    public class WebHookController : Controller
    {
        // If you are testing your webhook locally with the Stripe CLI you
        // can find the endpoint's secret by running `stripe listen`
        // Otherwise, find your endpoint's secret in your webhook settings in the Developer Dashboard
        const string endpointSecret = "whsec_1dUUc4I1lgtgq4IlXtPkdTUiCwtdt69p";

        [Route("webhook")]
        [HttpPost]
        public async Task<ActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.InputStream).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("PaymentIntent was successful!");
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    Console.WriteLine("PaymentMethod was attached to a Customer!");
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (StripeException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}