using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RefactorThis.Api.Controllers;

[ApiController]
[Route("api/[controller]")]  
public class ApiController : ControllerBase
{
    private IMediator? _mediator;

#pragma warning disable CS8603 // Possible null reference return.
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
#pragma warning restore CS8603 // Possible null reference return.
}
