using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mvcProyectoWeb.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoWeb.Models;

namespace mvcProyectoWeb.AccesoDatos.Data.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Producto producto)
        {
            var objDesdeDb = _db.Producto.FirstOrDefault(p => p.Id == producto.Id);
            if (objDesdeDb != null)
            {
                objDesdeDb.Nombre = producto.Nombre;
                objDesdeDb.Descripcion = producto.Descripcion;
                objDesdeDb.Precio = producto.Precio;
                objDesdeDb.Stock = producto.Stock;
                objDesdeDb.UrlImagen = producto.UrlImagen;
                objDesdeDb.AlmacenId = producto.AlmacenId;

                _db.SaveChanges();
            }
        }

    }

}
