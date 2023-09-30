using AutoMapper;
using Inventario.Modelos;
using Inventario.Modelos.Vistas;

namespace Inventario
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Usuario, VistaUsuario>();
            CreateMap<VistaUsuario, Usuario>();

            CreateMap<Usuario, CreateUsuario>().ReverseMap();
            CreateMap<Usuario, UpdateUsuario>().ReverseMap();
        }
    }
}
