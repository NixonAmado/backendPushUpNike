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
public class CategoryController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_CategoryDto>>> Get()
    {
        var Categories = await _unitOfWork.Categories.GetAllAsync();
        return _mapper.Map<List<G_CategoryDto>>(Categories);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_CategoryDto>>> Get([FromQuery] Params CategoryParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Categories.GetAllAsync(CategoryParams.PageIndex,CategoryParams.PageSize,CategoryParams.Search);
        var listaProv = _mapper.Map<List<G_CategoryDto>>(registros);
        return new Pager<G_CategoryDto>(listaProv,totalRegistros,CategoryParams.PageIndex,CategoryParams.PageSize,CategoryParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Category Category)
    {
         if (Category == null)
        {
            return BadRequest();
        }
        _unitOfWork.Categories.Add(Category);
        await _unitOfWork.SaveAsync();
       
        return "Category Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Category Category)
    {
        if (Category == null|| id != Category.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Categories.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Category, proveedExiste);
        _unitOfWork.Categories.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Category Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (Category == null)
        {
            return NotFound();
        }
        _unitOfWork.Categories.Remove(Category);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Category {Category.Id} se eliminó con éxito." });
    }
}












