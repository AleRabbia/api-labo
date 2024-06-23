using MiApi.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly MercadoPagoService _mercadoPagoService;

        public PaymentsController(MercadoPagoService mercadoPagoService)
        {
            _mercadoPagoService = mercadoPagoService;
        }

        [HttpPost("create-preference")]
        public async Task<IActionResult> CreatePreference([FromBody] CreatePreferenceRequestModel model)
        {
            var preference = await _mercadoPagoService.CreatePreference(
                model.Title,
                model.Quantity,
                model.CurrencyId,
                model.UnitPrice
            );

            return Ok(preference);
        }
    }

    public class CreatePreferenceRequestModel
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public string CurrencyId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
