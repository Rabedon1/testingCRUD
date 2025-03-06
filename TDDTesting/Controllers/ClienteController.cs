using Microsoft.AspNetCore.Mvc;
using TDDTesting.Data;
using TDDTesting.Models;

namespace TDDTesting.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDataAccessLayer objClienteDAL = new ClienteDataAccessLayer();

        
        public IActionResult Index()
        {
            List<Cliente> clientes = objClienteDAL.GetClientes().ToList();
            return View(clientes);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                objClienteDAL.AddCliente(objCliente); 
                return RedirectToAction("Index");  
            }
            return View(objCliente);
        }

       
        [HttpGet]
        public IActionResult Edit(int codigo)
        {
            Cliente cliente = objClienteDAL.GetClientes().FirstOrDefault(c => c.Codigo == codigo);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int codigo, [Bind] Cliente objCliente)
        {
            if (codigo != objCliente.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                objClienteDAL.UpdateCliente(objCliente); 
                return RedirectToAction("Index");  
            }
            return View(objCliente);
        }


        
        [HttpGet]
        public IActionResult Delete(int codigo)
        {
            Cliente cliente = objClienteDAL.GetClientes().FirstOrDefault(c => c.Codigo == codigo);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int codigo)
        {
            objClienteDAL.DeleteCliente(codigo); 
            return RedirectToAction("Index");  
        }
    }
}
