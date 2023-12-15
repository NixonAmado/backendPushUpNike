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
public class CartController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CartController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_CartDto>>> Get()
    {
        var Carts = await _unitOfWork.Carts.GetAllAsync();
        return _mapper.Map<List<G_CartDto>>(Carts);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_CartDto>>> Get([FromQuery] Params CartParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Carts.GetAllAsync(CartParams.PageIndex,CartParams.PageSize,CartParams.Search);
        var listaProv = _mapper.Map<List<G_CartDto>>(registros);
        return new Pager<G_CartDto>(listaProv,totalRegistros,CartParams.PageIndex,CartParams.PageSize,CartParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Cart Cart)
    {
         if (Cart == null)
        {
            return BadRequest();
        }
        _unitOfWork.Carts.Add(Cart);
        await _unitOfWork.SaveAsync();
       
        return "Cart Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Cart Cart)
    {
        if (Cart == null|| id != Cart.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Carts.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Cart, proveedExiste);
        _unitOfWork.Carts.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Cart Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Cart = await _unitOfWork.Carts.GetByIdAsync(id);
        if (Cart == null)
        {
            return NotFound();
        }
        _unitOfWork.Carts.Remove(Cart);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Cart {Cart.Id} se eliminó con éxito." });
    }
}












