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
public class PersonController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PersonController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_PersonDto>>> Get()
    {
        var People = await _unitOfWork.People.GetAllAsync();
        return _mapper.Map<List<G_PersonDto>>(People);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_PersonDto>>> Get([FromQuery] Params PersonParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.People.GetAllAsync(PersonParams.PageIndex,PersonParams.PageSize,PersonParams.Search);
        var listaProv = _mapper.Map<List<G_PersonDto>>(registros);
        return new Pager<G_PersonDto>(listaProv,totalRegistros,PersonParams.PageIndex,PersonParams.PageSize,PersonParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Person Person)
    {
         if (Person == null)
        {
            return BadRequest();
        }
        _unitOfWork.People.Add(Person);
        await _unitOfWork.SaveAsync();
       
        return "Person Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Person Person)
    {
        if (Person == null|| id != Person.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.People.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Person, proveedExiste);
        _unitOfWork.People.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Person Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Person = await _unitOfWork.People.GetByIdAsync(id);
        if (Person == null)
        {
            return NotFound();
        }
        _unitOfWork.People.Remove(Person);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Person {Person.Id} se eliminó con éxito." });
    }
}












