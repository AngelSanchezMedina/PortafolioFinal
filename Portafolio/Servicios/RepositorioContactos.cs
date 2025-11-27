using Dapper;
using Microsoft.Data.SqlClient;
using Portafolio.Models;

public interface IRepositorioContactos
{
    Task Crear(ContactoViewModel contactoViewModel);
    Task<IEnumerable<ContactoViewModel>> ObtenerListaContactos();
    Task Borrar (int id);
}

public class RepositorioContactos : IRepositorioContactos
{
    private readonly string connectionString;

    public RepositorioContactos(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task Crear(ContactoViewModel contactoViewModel)
    {
        using var connection = new SqlConnection(connectionString);
        var id = await connection.QuerySingleAsync<int>(
            @"INSERT INTO Contacto (nombre, email, mensaje) 
              VALUES (@nombre, @email, @mensaje);
              SELECT SCOPE_IDENTITY();", contactoViewModel);
    }

    public async Task<IEnumerable<ContactoViewModel>> ObtenerListaContactos()
    {
        using var connection = new SqlConnection(connectionString);
        return await connection.QueryAsync<ContactoViewModel>(
            @"SELECT idContacto, nombre, email, mensaje 
              FROM Contacto");
    }

    public async Task Borrar(int id)
    {
        using var connection = new SqlConnection(connectionString);
        var query = "DELETE FROM Contacto WHERE idContacto = @id";
        await connection.ExecuteAsync(query, new { id });
    }
}