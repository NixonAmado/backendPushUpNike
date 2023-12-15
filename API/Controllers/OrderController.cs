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
public class OrderController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_OrderDto>>> Get()
    {
        var Orders = await _unitOfWork.Orders.GetAllAsync();
        return _mapper.Map<List<G_OrderDto>>(Orders);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_OrderDto>>> Get([FromQuery] Params OrderParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Orders.GetAllAsync(OrderParams.PageIndex,OrderParams.PageSize,OrderParams.Search);
        var listaProv = _mapper.Map<List<G_OrderDto>>(registros);
        return new Pager<G_OrderDto>(listaProv,totalRegistros,OrderParams.PageIndex,OrderParams.PageSize,OrderParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Order Order)
    {
         if (Order == null)
        {
            return BadRequest();
        }
        _unitOfWork.Orders.Add(Order);
        await _unitOfWork.SaveAsync();
       
        return "Order Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Order Order)
    {
        if (Order == null|| id != Order.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Orders.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Order, proveedExiste);
        _unitOfWork.Orders.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Order Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Order = await _unitOfWork.Orders.GetByIdAsync(id);
        if (Order == null)
        {
            return NotFound();
        }
        _unitOfWork.Orders.Remove(Order);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Order {Order.Id} se eliminó con éxito." });
    }
}












