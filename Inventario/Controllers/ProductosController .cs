﻿
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
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly IProductosRepositorio _productosRepo;
        private readonly ITipoProductoRepositorio _tipoProductoRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public ProductosController(ILogger<ProductosController> logger, IProductosRepositorio productosRepo,
                                                  ITipoProductoRepositorio tipoProductoRepo, IMapper mapper)
        {
            _logger = logger;
            _productosRepo = productosRepo;
            _tipoProductoRepo = tipoProductoRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [Route("Lista")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Lista()
        {
            try
            {
                _logger.LogInformation("obtener los productos");
                IEnumerable<Productos> productosList = await _productosRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<VistaProductos>>(productosList);
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
        [Route("Lista2/{id:int}, Name =GetProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Lista(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("error al obtener el producto " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest();

                }
                
                var producto = await _productosRepo.Obtener(v => v.IdProducto == id);

                if (producto == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<VistaProductos>(producto);
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

        public async Task<ActionResult<APIResponse>> Guardar([FromBody] CreateProducto createProducto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _tipoProductoRepo.Obtener(v => v.IdTipoProducto == createProducto.IdTProducto) == null) //este if es pára evitar la repeticion de articulos o personas con el mismo nombre o para que algunn dato ya ingresado no se repita
                {
                    ModelState.AddModelError("CLAVE FORANEA", " NO EXISTE UN TIPO DE PRODUCTO CON ESE ID");
                    return BadRequest(ModelState);
                }
                if (createProducto == null)
                {
                    return BadRequest(createProducto);
                }
                Productos modelo = _mapper.Map<Productos>(createProducto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _productosRepo.Crear(modelo);//insert en bd
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetProductos", new { id = modelo.IdProducto }, _response);
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
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var producto = await _productosRepo.Obtener(v => v.IdProducto == id);
                if (producto == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _productosRepo.Remover(producto);
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
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateProducto updateProducto)
        {
            try
            {
                if (updateProducto == null || id != updateProducto.IdProducto)
                    
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _tipoProductoRepo.Obtener(v => v.IdTipoProducto == updateProducto.IdTProducto) == null) //este if es pára evitar la repeticion de articulos o personas con el mismo nombre o para que algunn dato ya ingresado no se repita
                {
                    ModelState.AddModelError("CLAVE FORANEA", " NO EXISTE UN TIPO DE PRODUCTO CON ESE ID");
                    return BadRequest(ModelState);
                }
                Productos modelo = _mapper.Map<Productos>(updateProducto);
                await _productosRepo.Actualizar(modelo);
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

        [HttpPatch]
        [Route("Editar2/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Editar2(int id, JsonPatchDocument<UpdateProducto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();

            }
            var producto = await _productosRepo.Obtener(v => v.IdProducto == id, tracked: false);

            UpdateProducto updateProducto = _mapper.Map<UpdateProducto>(producto);

            try
            {
                if (producto == null)
                { return BadRequest(); }
                patchDto.ApplyTo(updateProducto, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _tipoProductoRepo.Obtener(v => v.IdTipoProducto == updateProducto.IdTProducto) == null) //este if es pára evitar la repeticion de articulos o personas con el mismo nombre o para que algunn dato ya ingresado no se repita
                {
                    ModelState.AddModelError("CLAVE FORANEA", " NO EXISTE UN TIPO DE PRODUCTO CON ESE ID");
                    return BadRequest(ModelState);
                }
                Productos modelo = _mapper.Map<Productos>(updateProducto);
                await _productosRepo.Actualizar(modelo);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex )
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

        }
    }
}
