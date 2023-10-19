
using Microsoft.AspNetCore.Mvc;
using Inventario.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Inventario.Modelos;
using Inventario.Modelos.Vistas;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Inventario.Repositorio.IRepositorio;
using System.Net;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly ILogger<TipoProductoController> _logger;
        //private readonly IProductosRepositorio _productosRepo;
        private readonly ITipoProductoRepositorio _tipoProductoRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public TipoProductoController(ILogger<TipoProductoController> logger,ITipoProductoRepositorio tipoProductoRepo ,IMapper mapper)
        {
            _logger = logger;
        //    _productosRepo = productosRepo; 
            _tipoProductoRepo = tipoProductoRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [Route("Lista")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> ListaTP()
        {
            try
            {
                _logger.LogInformation("obtener los tipos de  productos");
                IEnumerable<TipoProducto> tipoProductoList = await _tipoProductoRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<VistaTipoProducto>>(tipoProductoList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }

        }

        [HttpGet]
        [Route("Lista2/{id:int}", Name = "GetProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ListaTP(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("error al obtener el tipo de  producto " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest();

                }
              
                var tipoProducto = await _tipoProductoRepo.Obtener(v => v.IdTipoProducto == id);

                if (tipoProducto == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<VistaTipoProducto>(tipoProducto);
                _response.IsExitoso = false;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;

            }

        }
        [HttpPost]
        [Route("Guardar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> GuardarTP([FromBody] CreateTipoProducto createTProducto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _tipoProductoRepo.Obtener(v => v.NombreTipoProducto.ToLower() == createTProducto.NombreTipoProducto.ToLower()) != null) //este if es pára evitar la repeticion de articulos o personas con el mismo nombre o para que algunn dato ya ingresado no se repita
                {
                    ModelState.AddModelError("EL NOMBRE YA EXISTE", " UN TIPO DE PRODUCTO  CON ESE NOMBRE YA EXISTE");
                    return BadRequest(ModelState);
                }
                if (createTProducto == null)
                {
                    return BadRequest(createTProducto);
                }
                TipoProducto modelo = _mapper.Map<TipoProducto>(createTProducto);
                await _tipoProductoRepo.Crear(modelo);//insert en bd
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetProductos", new { id = modelo.IdTipoProducto }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;

            }

        }
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarTP(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var tProducto = await _tipoProductoRepo.Obtener(v => v.IdTipoProducto == id);
                if (tProducto == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _tipoProductoRepo.Remover(tProducto);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);

            }

        }
        [HttpPut]
        [Route("Editar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditarTP(int id, [FromBody] UpdateTipoProducto updateTProducto)
        {
            try
            {
                if (updateTProducto == null || id != updateTProducto.IdTipoProducto)
                    
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //if (await _productosRepo.Obtener(v => v.IdProducto == updateProducto.IdTipoProducto) == null) //este if es pára evitar la repeticion de articulos o personas con el mismo nombre o para que algunn dato ya ingresado no se repita
                //{
                //    ModelState.AddModelError("CLAVE FORANEA", " NO EXISTE UN PRODUCTO CON ESE ID");
                //    return BadRequest(ModelState);
                //}
                TipoProducto modelo = _mapper.Map<TipoProducto>(updateTProducto);
                await _tipoProductoRepo.Actualizar(modelo);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

        }

       
    }
}
