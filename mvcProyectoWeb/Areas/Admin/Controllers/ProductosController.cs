// Controllers/ProductosController.cs
using Microsoft.AspNetCore.Mvc;
using mvcProyectoWeb.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mvcProyectoWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ProductosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Almacenes = _contenedorTrabajo.Almacen.GetAll()
                .Select(a => new SelectListItem { Text = a.NombreAlmacen, Value = a.Id.ToString() });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Producto.Add(producto);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Almacenes = _contenedorTrabajo.Almacen.GetAll()
                .Select(a => new SelectListItem { Text = a.NombreAlmacen, Value = a.Id.ToString() });
            return View(producto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var producto = _contenedorTrabajo.Producto.Get(id);
            if (producto == null)
                return NotFound();

            ViewBag.Almacenes = _contenedorTrabajo.Almacen.GetAll()
                .Select(a => new SelectListItem { Text = a.NombreAlmacen, Value = a.Id.ToString() });

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Producto.Update(producto);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Almacenes = _contenedorTrabajo.Almacen.GetAll()
                .Select(a => new SelectListItem { Text = a.NombreAlmacen, Value = a.Id.ToString() });
            return View(producto);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Producto.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _contenedorTrabajo.Producto.Get(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error borrando el producto" });
            }
            _contenedorTrabajo.Producto.Remove(obj);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Producto eliminado correctamente" });
        }
        #endregion
    }
}
