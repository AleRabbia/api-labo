using Microsoft.AspNetCore.Mvc;
using MiApi.Context;
using MiApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosPorCategoria(string categoria)
        {
            var productos = await _context.Productos
                                          .Where(p => p.category == categoria)
                                          .ToListAsync();
            if (productos == null || !productos.Any())
            {
                return NotFound();
            }

            return productos;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto([FromBody] Producto nuevoProducto)
        {
            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto productoActualizado)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.name = productoActualizado.name;
            producto.description = productoActualizado.description;
            producto.image = productoActualizado.image;
            producto.price = productoActualizado.price;
            producto.stock = productoActualizado.stock;
            producto.category = productoActualizado.category;

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
