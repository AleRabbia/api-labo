using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApi.Context;
using MiApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarCompra([FromBody] Compra compra)
        {
            if (compra == null || compra.Detalles == null || !compra.Detalles.Any())
            {
                return BadRequest("Datos de la compra inválidos");
            }

            // Limpiar los IDs de los detalles de compra
            foreach (var detalle in compra.Detalles)
            {
                detalle.Id = 0;
                var producto = await _context.Productos.FindAsync(detalle.ProductoId);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {detalle.ProductoId} no encontrado");
                }

                if (producto.stock < detalle.Cantidad)
                {
                    return BadRequest($"No hay suficiente stock del producto {producto.name}");
                }

                producto.stock -= detalle.Cantidad;
                _context.Productos.Update(producto);
            }

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
            return Ok(compra);
        }

        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetComprasByUser(string email)
        {
            var compras = await _context.Compras
                                        .Include(c => c.Detalles)
                                        .Where(c => c.EmailUsuario == email)
                                        .ToListAsync();

            if (compras == null || !compras.Any())
            {
                return NotFound("No se encontraron compras para este usuario");
            }

            return Ok(compras);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCompras()
        {
            var compras = await _context.Compras
                                        .Include(c => c.Detalles)
                                        .ToListAsync();

            if (compras == null || !compras.Any())
            {
                return NotFound("No se encontraron compras");
            }

            return Ok(compras);
        }
    }
}
