using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Portafolio.Models;
using Portafolio.Servicios;
using System.Diagnostics;

namespace Portafolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioProyectos repositorioProyectos;
        private readonly IRepositorioHabilidades repositorioHabilidades;

        public HomeController(ILogger<HomeController> logger,
            IRepositorioProyectos repositorioProyectos,
            IRepositorioHabilidades repositorioHabilidades
            )
        {
            _logger = logger;
            this.repositorioProyectos = repositorioProyectos;
            this.repositorioHabilidades = repositorioHabilidades;
        }

        public async Task<IActionResult> Index()
        {
            var proyectos = (await repositorioProyectos.Obtener()).Take(3).ToList();
            var habilidades = await repositorioHabilidades.Obtener();

            var modelo = new HomeIndexViewModel()
            {
                Proyectos = proyectos,
                Habilidades = habilidades
            };

            return View(modelo);
        }

        public async Task<IActionResult> MisProyectos()
        {
            var proyecto = await repositorioProyectos.Obtener();
            return View(proyecto);
        }


        [HttpGet]
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contacto(ContactoViewModel contactoViewModel)
        {
            return RedirectToAction("Gracias");
        }

        public IActionResult Gracias()
        {
            return View();
        }
                    
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
