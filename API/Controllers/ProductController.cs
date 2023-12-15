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
public class ProductController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<G_ProductDto>>> Get()
    {
        var Products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<List<G_ProductDto>>(Products);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<G_ProductDto>>> Get([FromQuery] Params ProductParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Products.GetAllAsync(ProductParams.PageIndex,ProductParams.PageSize,ProductParams.Search);
        var listaProv = _mapper.Map<List<G_ProductDto>>(registros);
        return new Pager<G_ProductDto>(listaProv,totalRegistros,ProductParams.PageIndex,ProductParams.PageSize,ProductParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Product Product)
    {
         if (Product == null)
        {
            return BadRequest();
        }
        _unitOfWork.Products.Add(Product);
        await _unitOfWork.SaveAsync();
       
        return "Product Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Product Product)
    {
        if (Product == null|| id != Product.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Products.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Product, proveedExiste);
        _unitOfWork.Products.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Product Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Product = await _unitOfWork.Products.GetByIdAsync(id);
        if (Product == null)
        {
            return NotFound();
        }
        _unitOfWork.Products.Remove(Product);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Product {Product.Id} se eliminó con éxito." });
    }
}












