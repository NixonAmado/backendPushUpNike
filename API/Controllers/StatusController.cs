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
public class StatusController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public StatusController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_StatusDto>>> Get()
    {
        var Statuses = await _unitOfWork.Statuses.GetAllAsync();
        return _mapper.Map<List<G_StatusDto>>(Statuses);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_StatusDto>>> Get([FromQuery] Params StatusParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Statuses.GetAllAsync(StatusParams.PageIndex,StatusParams.PageSize,StatusParams.Search);
        var listaProv = _mapper.Map<List<G_StatusDto>>(registros);
        return new Pager<G_StatusDto>(listaProv,totalRegistros,StatusParams.PageIndex,StatusParams.PageSize,StatusParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Status Status)
    {
         if (Status == null)
        {
            return BadRequest();
        }
        _unitOfWork.Statuses.Add(Status);
        await _unitOfWork.SaveAsync();
       
        return "Status Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Status Status)
    {
        if (Status == null|| id != Status.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Statuses.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Status, proveedExiste);
        _unitOfWork.Statuses.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Status Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Status = await _unitOfWork.Statuses.GetByIdAsync(id);
        if (Status == null)
        {
            return NotFound();
        }
        _unitOfWork.Statuses.Remove(Status);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Status {Status.Id} se eliminó con éxito." });
    }
}












