using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
        // protected IMediator Mediator => _mediator ??= 
        //     HttpContext.RequestServices.GetService<IMediator>();
    }
}