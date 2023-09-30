
using Microsoft.AspNetCore.Mvc;
using Inventario.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Inventario.Modelos;
using Inventario.Modelos.Vistas;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public  UsuariosController(ILogger<UsuariosController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;   
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            _logger.LogInformation("obtener los usuarios");
            IEnumerable<Usuario> usuarioList = await _db.Usuarios.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VistaUsuario>>(usuarioList));
        }

        [HttpGet("id:int", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VistaUsuario>> GetUsuario(int id)
        {
            if (id == 0)
            {
                _logger.LogError("error al obtener el usuario");
                return BadRequest();

            }
            //var villa = _db.villas.FirstOrDefault(v => v.Id == id);
            var usuario =await  _db.Usuarios.FirstOrDefaultAsync(v => v.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VistaUsuario>(usuario));

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<VistaUsuario>> CrearUsuario([FromBody] CreateUsuario createUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _db.Usuarios.FirstOrDefaultAsync(v => v.NombreUsuario.ToLower() == createUsuario.NombreUsuario.ToLower()) != null) //este if es pára evitar la repeticion de articulos o personas con el mismo nombre o para que algunn dato ya ingresado no se repita
            {
                ModelState.AddModelError("EL NOMBRE YA EXISTE", " UN USUARIO  CON ESE NOMBRE YA EXISTE");
                return BadRequest(ModelState);
            }
            if (createUsuario == null)
            {
                return BadRequest(createUsuario);
            }
            Usuario modelo = _mapper.Map<Usuario>(createUsuario);
            await _db.Usuarios.AddAsync(modelo);//insert en bd
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetUsuario", new { id = modelo.IdUsuario },modelo);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult >DeleteUsuario(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var usuario =await  _db.Usuarios.FirstOrDefaultAsync(v => v.IdUsuario == id);
            if (usuario == null) { return NotFound(); }
            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuario updateUsuario)
        {
            if (updateUsuario == null || id != updateUsuario.IdUsuario)
            { return BadRequest(); }

            Usuario modelo = _mapper.Map<Usuario>(updateUsuario);
            _db.Usuarios.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialUsuario(int id, JsonPatchDocument<UpdateUsuario> patchDto)
        {
            if (patchDto == null || id == 0)
            { 
                return BadRequest(); 
            
           }
            var usuario = await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(v => v.IdUsuario == id);

            UpdateUsuario updateUsuario = _mapper.Map<UpdateUsuario>(usuario);

            if (usuario == null)
            { return BadRequest(); }
            patchDto.ApplyTo(updateUsuario, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Usuario modelo = _mapper.Map<Usuario>(updateUsuario);
            _db.Usuarios.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    
    }
}
