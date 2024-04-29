using Application.Dtos;
using Application.Extensions;
using Application.Products.Commands;
using Application.Products.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.WebAPI;

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

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get([FromRoute] Guid id)
    {
        var claims = User.Claims.ToList();
        var response = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(response);
    }

    [Authorize]    
    [HttpPost("create")]
    public async Task<ActionResult<Product>> Create([FromBody] ProductDto request)
    {
        if (request is null)
            return BadRequest("Request is null!");

        var userId = User.GetUserIdClaim();
        request.UserId = Guid.Parse(userId);

        var response = await _mediator.Send(new CreateProductCommand(request));
        
        return Ok(response);
    }

}