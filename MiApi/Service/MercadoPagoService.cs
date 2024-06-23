using MercadoPago.Config;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiApi.Service
{
    public class MercadoPagoService
    {
        public MercadoPagoService()
        {
            MercadoPagoConfig.AccessToken = "TEST-3869705539231226-062014-6bde69b03ab890bf1576774760e50b8f-178072239";
        }

        public async Task<Preference> CreatePreference(string title, int quantity, string currencyId, decimal unitPrice)
        {
            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = title,
                        Quantity = quantity,
                        CurrencyId = currencyId,
                        UnitPrice = unitPrice,
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "http://google.com/",
                    Failure = "http://google.com/",
                    Pending = "http://google.com/"
                },
                AutoReturn = "approved"
            };

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            return preference;
        }
    }
}
