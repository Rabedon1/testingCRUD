/*using Microsoft.AspNetCore.Mvc;
using TDDTesting.Data;
using TDDTesting.Models;

namespace TDDTesting.Controllers
{
    public class ProductoController : Controller
    {
        ProductoDataAccessLayer objProductoDAL = new ProductoDataAccessLayer();

        public IActionResult Index()
        {
            List<Producto> productos = objProductoDAL.GetProductos().ToList();
            return View(productos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Producto objProducto)
        {
            if (ModelState.IsValid)
            {
                objProductoDAL.AddProducto(objProducto);
                return RedirectToAction("Index");
            }
            return View(objProducto);
        }

        [HttpGet]
        public IActionResult Edit(int codigo)
        {
            Producto producto = objProductoDAL.GetProductos().FirstOrDefault(p => p.Codigo == codigo);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int codigo, [Bind] Producto objProducto)
        {
            if (codigo != objProducto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                objProductoDAL.UpdateProducto(objProducto);
                return RedirectToAction("Index");
            }
            return View(objProducto);
        }

        [HttpGet]
        public IActionResult Delete(int codigo)
        {
            Producto producto = objProductoDAL.GetProductos().FirstOrDefault(p => p.Codigo == codigo);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int codigo)
        {
            objProductoDAL.DeleteProducto(codigo);
            return RedirectToAction("Index");
        }
    }
}
*/