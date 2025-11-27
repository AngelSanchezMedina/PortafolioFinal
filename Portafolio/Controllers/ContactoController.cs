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

        public async Task<IActionResult> ListaContactos()
        {
            var contactos = await repositorioContactos.ObtenerListaContactos();

            if (contactos == null)
            {
                contactos = new List<ContactoViewModel>();
            }

            return View(contactos);
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var contactoViewModels = await repositorioContactos.ObtenerListaContactos();

            if (contactoViewModels is null)
            {
                return RedirectToAction("Index");
            }

            return View(contactoViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            await repositorioContactos.Borrar(id);
            return RedirectToAction("ListaContactos");
        }

        [HttpPost]
        public async Task<IActionResult> BorrarContacto(int id)
        {
            await repositorioContactos.Borrar(id);
            return RedirectToAction("Index");
        }

    }
}
