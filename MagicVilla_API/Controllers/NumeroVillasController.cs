using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repositoy.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillasController : ControllerBase
    {
        private readonly ILogger<NumeroVillasController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly INumeroVillaRepository _numeroVillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillasController(ILogger<NumeroVillasController> logger, IVillaRepository villaRepo, 
                                                                              INumeroVillaRepository numeroVillaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroVillaRepo = numeroVillaRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener numero de Villas");

                IEnumerable<NumeroVilla> numerovillalist = await _numeroVillaRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numerovillalist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            { _response.ISExitoso= false;
              _response.ErrorMessages = new List<string>(){ ex.ToString() };  
            }

            return _response;
        }

        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer villa con id " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ISExitoso= false;
                    return BadRequest(_response);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNo == id);
                if (numeroVilla == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ISExitoso= false;
                    return NotFound(_response);
                }

                _response.Resultado =_mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.StatusCode=HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.ISExitoso= false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
           
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroVillaRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NameExiste", "El numero de Villa ya existe");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Obtener(v=> v.Id==createDto.VillaId)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id  de la  Villa No existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);
                modelo.DateCreate = DateTime.Now;
                modelo.DateUpdate = DateTime.Now;
                await _numeroVillaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {

                _response.ISExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return (_response);
           
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.VillaNo)
            {
                _response.ISExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepo.Obtener(v=>v.Id == updateDto.VillaId)==null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id  de la  Villa ya existe");
                return BadRequest(ModelState);
            }

            NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);
            
            await  _numeroVillaRepo.Actualizar(modelo);
            _response.StatusCode = HttpStatusCode.NoContent;
            
            return Ok(_response);
        }

        //[HttpPatch("id:int")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        //{
        //    if (patchDto == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var villa = await _villaRepo.Obtener(v => v.Id == id, tracked:false);

        //    VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

        //    if (villa == null) return BadRequest();
            
        //    patchDto.ApplyTo(villaDto, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    Villa modelo = _mapper.Map<Villa>(villaDto);


        //    await _villaRepo.Actualizar(modelo);
        //    _response.StatusCode = HttpStatusCode.NoContent;
            
        //    return Ok(_response);
        //}

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.ISExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    return BadRequest(_response);
                }
                var numerovilla = await _numeroVillaRepo.Obtener(v => v.VillaNo == id);
                if (numerovilla == null)
                {
                    _response.ISExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _numeroVillaRepo.Remover(numerovilla);

                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.ISExitoso=false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return BadRequest(_response);
           
        }
    }

    
}
