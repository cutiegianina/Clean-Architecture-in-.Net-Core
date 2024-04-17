using Application.Dtos;
using Application.Products.Commands;
using Application.Products.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> Get()
    {
        var response = await _mediator.Send(new GetProductQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(response);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Product>> Create([FromBody] ProductDto request)
    {
        if (request is null)
            return BadRequest("Request is null!");

        var response = await _mediator.Send(new CreateProductCommand(request));
        
        return Ok(response);
    }

}