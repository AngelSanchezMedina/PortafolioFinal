using Microsoft.AspNetCore.Mvc;
using Portafolio.Models;

public class HabilidadesController : Controller
{
    private readonly IRepositorioHabilidades repositorioHabilidades;

    public HabilidadesController(IRepositorioHabilidades repositorioHabilidades)
    {
        this.repositorioHabilidades = repositorioHabilidades;
    }

    public async Task<IActionResult> Editar()
    {
        var modelo = await repositorioHabilidades.Obtener();
        return View(modelo);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(HabilidadesViewModel model, string BackExperiencia, string FrontExperiencia)
    {
        // Convertir cadenas separadas por comas a listas
        model.BackExperiencia = string.IsNullOrWhiteSpace(BackExperiencia)
            ? new List<string>()
            : BackExperiencia.Split(',').Select(e => e.Trim()).ToList();

        model.FrontExperiencia = string.IsNullOrWhiteSpace(FrontExperiencia)
            ? new List<string>()
            : FrontExperiencia.Split(',').Select(e => e.Trim()).ToList();

        await repositorioHabilidades.Actualizar(model);

        return RedirectToAction("Index", "Home");
    }
}
