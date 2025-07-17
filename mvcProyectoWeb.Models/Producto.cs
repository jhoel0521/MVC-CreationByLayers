using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvcProyectoWeb.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un precio")]
        [Range(0.01, 1000000, ErrorMessage = "Precio inválido")]
        public decimal Precio { get; set; }

        [Display(Name = "Stock disponible")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock inválido")]
        public int Stock { get; set; }

        [Display(Name = "URL Imagen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }

        [Display(Name = "Almacén")]
        [Required(ErrorMessage = "Debe seleccionar un almacén")]
        public int AlmacenId { get; set; }

        [ForeignKey("AlmacenId")]
        public Almacen? Almacen { get; set; }
    }
}
