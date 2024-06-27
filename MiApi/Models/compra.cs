using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiApi.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        public string EmailUsuario { get; set; }
        public string MontoTotal { get; set; }
        public string NumeroOrden { get; set; }
        public List<DetalleCompra> Detalles { get; set; }
    }

    public class DetalleCompra
    {
        [Key]
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
