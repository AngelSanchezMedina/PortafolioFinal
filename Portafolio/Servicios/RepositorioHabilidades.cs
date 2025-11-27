using Dapper;
using Microsoft.Data.SqlClient;
using Portafolio.Models;

public interface IRepositorioHabilidades
{
    Task<HabilidadesViewModel> Obtener();
    Task Actualizar(HabilidadesViewModel modelo);
}

public class RepositorioHabilidades : IRepositorioHabilidades
{
    private readonly string connectionString;

    public RepositorioHabilidades(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<HabilidadesViewModel> Obtener()
    {
        using var connection = new SqlConnection(connectionString);

        var sql = @"SELECT TOP 1 
                    BackTitulo, BackDescripcion, BackStack, BackExperiencia AS BackExperienciaString,
                    FrontTitulo, FrontDescripcion, FrontStack, FrontExperiencia AS FrontExperienciaString
                FROM Habilidades";

        var data = await connection.QueryFirstOrDefaultAsync<HabilidadesViewModel>(sql);

        if (data == null)
            return new HabilidadesViewModel();

        // Convertir strings a listas
        data.BackExperiencia = string.IsNullOrWhiteSpace(data.BackExperienciaString)
            ? new List<string>()
            : data.BackExperienciaString.Split(',').Select(x => x.Trim()).ToList();

        data.FrontExperiencia = string.IsNullOrWhiteSpace(data.FrontExperienciaString)
            ? new List<string>()
            : data.FrontExperienciaString.Split(',').Select(x => x.Trim()).ToList();

        return data;
    }



    public async Task Actualizar(HabilidadesViewModel modelo)
    {
        using var connection = new SqlConnection(connectionString);

        var sql = @"UPDATE Habilidades SET 
                    BackTitulo = @BackTitulo,
                    BackDescripcion = @BackDescripcion,
                    BackStack = @BackStack,
                    BackExperiencia = @BackExperienciaString,
                    FrontTitulo = @FrontTitulo,
                    FrontDescripcion = @FrontDescripcion,
                    FrontStack = @FrontStack,
                    FrontExperiencia = @FrontExperienciaString
                WHERE Id = 1";

        await connection.ExecuteAsync(sql, new
        {
            modelo.BackTitulo,
            modelo.BackDescripcion,
            modelo.BackStack,
            BackExperienciaString = string.Join(",", modelo.BackExperiencia ?? new()),
            modelo.FrontTitulo,
            modelo.FrontDescripcion,
            modelo.FrontStack,
            FrontExperienciaString = string.Join(",", modelo.FrontExperiencia ?? new())
        });
    }

}

