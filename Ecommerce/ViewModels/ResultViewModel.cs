using Ecommerce.ViewModels;
using System.Text.Json.Serialization;

namespace Ecommerce.ViewModels
{
    public class ResultViewModel
    {
        [JsonPropertyName("Customers")]
        public List<CustomerViewModel> CustomerViewModels { get; set; }

        [JsonPropertyName("Orders")]
        public List<OrderViewModel> OrderViewModels { get; set; }

        [JsonPropertyName("Payments")]
        public List<PaymentViewModel> PaymentViewModels { get; set; }

        [JsonPropertyName("Companies")]
        public List<CompanyViewModel> CompanyViewModels { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }
}