using System.Web.Mvc;
using Newtonsoft.Json;
using Stripe;

namespace Stripe_ASP.NET_MVC5.Controllers
{
    //[Route("create-payment-intent")]
    public class CheckoutController : Controller
    {
        [Route("create-payment-intent")]
        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = "usd",
            });
            return Json(new {clientSecret = paymentIntent.ClientSecret});
        }

        private int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 1400;
        }

        public class Item
        {
            [JsonProperty("id")] public string Id { get; set; }
        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")] public Item[] Items { get; set; }
        }
    }
}