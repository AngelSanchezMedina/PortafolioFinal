using Portafolio.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Portafolio.Servicios
{
    public interface IRepositorioContactos
    {
        Task Crear(ContactoViewModel contactoViewModel);
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
            VALUES( @nombre, @email, @mensaje);

            SELECT SCOPE_IDENTITY();", contactoViewModel);
        }
    }
}
