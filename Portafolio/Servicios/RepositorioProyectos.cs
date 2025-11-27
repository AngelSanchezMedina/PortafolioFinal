using Dapper;
using Microsoft.Data.SqlClient;
using Portafolio.Models;

namespace Portafolio.Servicios
{
    public interface IRepositorioProyectos
    {
        Task Actualizar(Proyecto proyecto);
        Task Borrar(int id);
        Task Crear(Proyecto proyecto);
        Task<IEnumerable<Proyecto>> Obtener();
        Task<Proyecto> ObtenerPorId(int id);
        Task<List<Proyecto>> ObtenerProyectos();
        Task Ordenar(IEnumerable<Proyecto> proyectosOrdenados);
    }

    public class RepositorioProyectos : IRepositorioProyectos
    {
        private readonly string connectionString;

        public RepositorioProyectos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Proyecto>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"SELECT idProyecto, titulo, descripcion, imagenUrl, link, orden
                          FROM Proyecto
                          ORDER BY Orden";
            return await connection.QueryAsync<Proyecto>(query);
        }

        public async Task<List<Proyecto>> ObtenerProyectos()
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"SELECT idProyecto, titulo, descripcion, imagenUrl, link
                  FROM Proyecto";

            var proyectos = await connection.QueryAsync<Proyecto>(query);
            return proyectos.ToList();
        }

        public async Task<Proyecto> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"SELECT idProyecto, titulo, descripcion, imagenUrl, link, orden
                          FROM Proyecto
                          WHERE idProyecto = @id";
            return await connection.QueryFirstOrDefaultAsync<Proyecto>(query, new { id });
        }

        /*public async Task Crear(Proyecto proyecto)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Proyecto (titulo, descripcion, imagenUrl, link)
                  VALUES (@titulo, @descripcion, @imagenUrl, @link);
                  SELECT SCOPE_IDENTITY();", proyecto);
        }*/

        public async Task Crear(Proyecto proyecto)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                ("Proyecto_Insertar",
                new
                {
                    titulo = proyecto.Titulo,
                    descripcion = proyecto.Descripcion,
                    imagenURL = proyecto.ImagenURL,
                    link = proyecto.Link,
                },
                commandType: System.Data.CommandType.StoredProcedure);
            proyecto.idProyecto = id;
        }//clase lunes

        public async Task Actualizar(Proyecto proyecto)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Proyecto SET
                                            titulo = @titulo,
                                            descripcion = @descripcion,
                                            imagenUrl = @imagenUrl,
                                            link = @link
                                            WHERE idProyecto = @idProyecto", proyecto);
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            var query = "DELETE FROM Proyecto WHERE idProyecto = @id";
            await connection.ExecuteAsync(query, new { id });
        }

        public async Task Ordenar(IEnumerable<Proyecto> proyectosOrdenados)
        {
            var query = "UPDATE Proyecto SET orden = @orden WHERE idProyecto = @idProyecto;";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, proyectosOrdenados);
        }
    }
}