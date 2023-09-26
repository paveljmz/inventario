
using Microsoft.AspNetCore.Mvc;
using Inventario.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Inventario.Modelos;
using Inventario.Modelos.Vistas;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ApplicationDbContext _db;
        public UsuariosController(ILogger<UsuariosController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            _logger.LogInformation("obtener los usuarios");
            return Ok(_db.Usuarios);
        }

        [HttpGet("id:int", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VistaUsuario> GetUsuario(int id)
        {
            if (id == 0)
            {
                _logger.LogError("error al obtener el usuario");
                return BadRequest();

            }
            //var villa = _db.villas.FirstOrDefault(v => v.Id == id);
            var usuario = _db.Usuarios.FirstOrDefault(v => v.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VistaUsuario> CrearUsuario([FromBody] VistaUsuario vistaUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.Usuarios.FirstOrDefault(v => v.NombreUsuario.ToLower() == vistaUsuario.NombreUsuario.ToLower()) != null) //este if es pára evitar la repeticion de articulos o personas con el mismo nombre o para que algunn dato ya ingresado no se repita
            {
                ModelState.AddModelError("EL NOMBRE YA EXISTE", " UN USUARIO  CON ESE NOMBRE YA EXISTE");
                return BadRequest(ModelState);
            }
            if (vistaUsuario == null)
            {
                return BadRequest(vistaUsuario);
            }
            if (vistaUsuario.IdUsuario> 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Usuario modelo = new()
            {
                IdUsuario = vistaUsuario.IdUsuario,
                NombreUsuario = vistaUsuario.NombreUsuario,
                CorreoUsuario = vistaUsuario.CorreoUsuario,
                TipoUsuario = vistaUsuario.TipoUsuario,
                Area = vistaUsuario.Area,
                PasswordUsuario = vistaUsuario.PasswordUsuario

            };
            _db.Usuarios.Add(modelo);//insert en bd
            _db.SaveChanges();
            return CreatedAtRoute("GetUsuario", new { id = vistaUsuario.IdUsuario }, vistaUsuario);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUsuario(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var usuario = _db.Usuarios.FirstOrDefault(v => v.IdUsuario == id);
            if (usuario == null) { return NotFound(); }
            _db.Usuarios.Remove(usuario);
            _db.SaveChanges();
            return NoContent();

        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUsuario(int id, [FromBody] VistaUsuario vistaUsuario)
        {
            if (vistaUsuario == null || id != vistaUsuario.IdUsuario)
            { return BadRequest(); }
            
            Usuario modelo = new()
            {
                IdUsuario = vistaUsuario.IdUsuario,
                NombreUsuario = vistaUsuario.NombreUsuario,
                CorreoUsuario = vistaUsuario.CorreoUsuario,
                TipoUsuario = vistaUsuario.TipoUsuario,
                Area = vistaUsuario.Area,
                PasswordUsuario = vistaUsuario.PasswordUsuario

            };
            _db.Usuarios.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialUsuario(int id, JsonPatchDocument<VistaUsuario> patchDto)
        {
            if (patchDto == null || id == 0)
            { return BadRequest(); }
            var usuario = _db.Usuarios.AsNoTracking().FirstOrDefault(v => v.IdUsuario == id);
            VistaUsuario vistaUsuario = new()
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                CorreoUsuario = usuario.CorreoUsuario,
                TipoUsuario = usuario.TipoUsuario,
                Area = usuario.Area,
                PasswordUsuario = usuario.PasswordUsuario
            };

            if (usuario == null) return BadRequest();
            patchDto.ApplyTo(vistaUsuario, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Usuario modelo = new()
            {
                IdUsuario = vistaUsuario.IdUsuario,
                NombreUsuario = vistaUsuario.NombreUsuario,
                CorreoUsuario = vistaUsuario.CorreoUsuario,
                TipoUsuario = vistaUsuario.TipoUsuario,
                Area = vistaUsuario.Area,
                PasswordUsuario = vistaUsuario.PasswordUsuario

            };
            _db.Usuarios.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
