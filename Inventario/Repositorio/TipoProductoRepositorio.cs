using Inventario.Datos;
using Inventario.Modelos;
using Inventario.Repositorio.IRepositorio;

namespace Inventario.Repositorio
{
    public class TipoProductoRepositorio : Repositorio<TipoProducto>, ITipoProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
         
        public TipoProductoRepositorio(ApplicationDbContext db) :base(db) 
        {
            _db = db;
        }

        public async Task<TipoProducto> Actualizar(TipoProducto entidad)
        {
            
            _db.TipoProducto.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
