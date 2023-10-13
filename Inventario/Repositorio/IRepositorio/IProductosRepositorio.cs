using Inventario.Modelos;

namespace Inventario.Repositorio.IRepositorio
{
    public interface IProductosRepositorio : IRepositorio<Productos>
    {
        Task<Productos> Actualizar(Productos entidad);
    }
}
