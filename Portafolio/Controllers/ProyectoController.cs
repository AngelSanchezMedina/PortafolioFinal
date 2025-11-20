using Microsoft.AspNetCore.Mvc;
using Portafolio.Models;
using Portafolio.Servicios;

namespace Portafolio.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly IRepositorioProyectos repositorioProyectos;

        public ProyectoController(IRepositorioProyectos repositorioProyectos)
        {
            this.repositorioProyectos = repositorioProyectos;
        }

        public async Task<IActionResult> Index()
        {
            var proyecto = await repositorioProyectos.Obtener();
            return View(proyecto);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Proyecto proyecto)
        {
            if(!ModelState.IsValid)
                return View(proyecto);

            await repositorioProyectos.Crear(proyecto);
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Editar(int id)
        {
            var proyecto = await repositorioProyectos.ObtenerPorId(id);
            if (proyecto == null)
                return RedirectToAction("Index");

            return View(proyecto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Proyecto proyecto)
        {
            if(!ModelState.IsValid)
                return View(proyecto);  

            await repositorioProyectos.Actualizar(proyecto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var producto = await repositorioProyectos.ObtenerPorId(id);
            if (producto is null)
                return RedirectToAction("Index");

            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarProyecto(int id)
        {
            await repositorioProyectos.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}
