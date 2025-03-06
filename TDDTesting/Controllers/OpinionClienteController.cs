using Microsoft.AspNetCore.Mvc;
using TDDTesting.Data;
using TDDTesting.Models;
using System.Linq;

namespace TDDTesting.Controllers
{
    public class OpinionClienteController : Controller
    {
        OpinionClienteDataAccessLayer objOpinionClienteDAL = new OpinionClienteDataAccessLayer();

        public IActionResult Index()
        {
            // Obtener todas las opiniones
            List<OpinionCliente> opiniones = objOpinionClienteDAL.GetOpiniones().ToList();
            return View(opiniones);
        }

        // Mostrar formulario para crear una nueva opinión
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Crear una nueva opinión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] OpinionCliente objOpinionCliente)
        {
            if (ModelState.IsValid)
            {
                // Llamar al método para agregar la opinión
                objOpinionClienteDAL.AddOpinionCliente(objOpinionCliente);
                return RedirectToAction("Index");
            }
            return View(objOpinionCliente);
        }

        // Mostrar formulario para editar una opinión
        [HttpGet]
        public IActionResult Edit(int OpinionID)
        {
            // Buscar la opinión por su ID
            OpinionCliente opinionCliente = objOpinionClienteDAL.GetOpiniones().FirstOrDefault(o => o.OpinionID == OpinionID);
            if (opinionCliente == null)
            {
                return NotFound();
            }
            return View(opinionCliente);
        }

        // Editar una opinión existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int OpinionID, [Bind] OpinionCliente objOpinionCliente)
        {
            // Verificar que el ID coincida
            if (OpinionID != objOpinionCliente.OpinionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Llamar al método para actualizar la opinión
                objOpinionClienteDAL.UpdateOpinionCliente(objOpinionCliente);
                return RedirectToAction("Index");
            }
            return View(objOpinionCliente);
        }

        // Mostrar el formulario de eliminación de una opinión
        [HttpGet]
        public IActionResult Delete(int OpinionID)
        {
            // Buscar la opinión por su ID
            OpinionCliente opinionCliente = objOpinionClienteDAL.GetOpiniones().FirstOrDefault(o => o.OpinionID == OpinionID);
            if (opinionCliente == null)
            {
                return NotFound();
            }
            return View(opinionCliente);
        }

        // Confirmar eliminación de la opinión
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int OpinionID)
        {
            // Llamar al método para eliminar la opinión
            objOpinionClienteDAL.DeleteOpinionCliente(OpinionID);
            return RedirectToAction("Index");
        }
    }
}
