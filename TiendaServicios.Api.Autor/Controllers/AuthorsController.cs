using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Model.Dto;
using TiendaServicios.Api.Autor.Model.Request;
using TiendaServicios.Api.Autor.Model.Response;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> AuthorList([FromQuery] GetAuthorsRequestModel model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{AuthorId}")]
        public async Task<ActionResult> SingleAuthor([FromRoute] string AuthorId)
        {
            var result= await _mediator.Send(new GetSingleAuthorRequestModel { BookAuthorId = new Guid(AuthorId) });
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddAuthorRequestModel model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

    }
}
