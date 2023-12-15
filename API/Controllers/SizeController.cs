using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class SizeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public SizeController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_SizeDto>>> Get()
    {
        var Sizes = await _unitOfWork.Sizes.GetAllAsync();
        return _mapper.Map<List<G_SizeDto>>(Sizes);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_SizeDto>>> Get([FromQuery] Params SizeParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Sizes.GetAllAsync(SizeParams.PageIndex,SizeParams.PageSize,SizeParams.Search);
        var listaProv = _mapper.Map<List<G_SizeDto>>(registros);
        return new Pager<G_SizeDto>(listaProv,totalRegistros,SizeParams.PageIndex,SizeParams.PageSize,SizeParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Size Size)
    {
         if (Size == null)
        {
            return BadRequest();
        }
        _unitOfWork.Sizes.Add(Size);
        await _unitOfWork.SaveAsync();
       
        return "Size Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Size Size)
    {
        if (Size == null|| id != Size.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Sizes.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Size, proveedExiste);
        _unitOfWork.Sizes.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Size Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Size = await _unitOfWork.Sizes.GetByIdAsync(id);
        if (Size == null)
        {
            return NotFound();
        }
        _unitOfWork.Sizes.Remove(Size);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Size {Size.Id} se eliminó con éxito." });
    }
}












