using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Book.Model.Request;


namespace TiendaServicios.Api.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetBooksRequestModel value)
        {
            var result = await _mediator.Send(value);
            return Ok(result);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(Guid Id)
        {
            var result = await _mediator.Send(new GetSingleBookRequestModel { BookId = Id });
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddBookRequestModel value)
        {
            var result = await _mediator.Send(value);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
