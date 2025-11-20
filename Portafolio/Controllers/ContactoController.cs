using Microsoft.AspNetCore.Mvc;
using Portafolio.Models;
using Portafolio.Servicios;

namespace Portafolio.Controllers
{
    public class ContactoController : Controller
    {
        private readonly IRepositorioContactos repositorioContactos;

        public ContactoController(IRepositorioContactos repositorioContactos)
        {
            this.repositorioContactos = repositorioContactos;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ContactoViewModel contactoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactoViewModel);
            }

            await repositorioContactos.Crear(contactoViewModel);
            return RedirectToAction("Index", "Home");
        }
    }
}
