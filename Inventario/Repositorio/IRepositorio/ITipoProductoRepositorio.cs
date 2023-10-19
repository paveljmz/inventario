using Inventario.Modelos;

namespace Inventario.Repositorio.IRepositorio
{
    public interface ITipoProductoRepositorio : IRepositorio<TipoProducto>
    {
        Task<TipoProducto> Actualizar(TipoProducto entidad);
    }
}
