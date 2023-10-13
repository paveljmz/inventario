using Inventario.Datos;
using Inventario.Modelos;
using Inventario.Repositorio.IRepositorio;

namespace Inventario.Repositorio
{
    public class ProductosRepositorio : Repositorio<Productos>, IProductosRepositorio
    {
        private readonly ApplicationDbContext _db;
         
        public ProductosRepositorio(ApplicationDbContext db) :base(db) 
        {
            _db = db;
        }

        public async Task<Productos> Actualizar(Productos entidad)
        {
            entidad.FechaCreacion = DateTime.Now;
            _db.Productos.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
