using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.ShoppingCart.Model.Request;


namespace TiendaServicios.Api.ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly IMapper _mapper;
        public ShoppingCartsController(IMediator mediator)
        {
            _mediator = mediator;
            //_mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(Guid Id)
        {
            var result = await _mediator.Send(new GetSingleSessionRequestModel { SessionCartId = Id });
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddCartSessionRequestModel value)
        {
            var result = await _mediator.Send(value);
            return Ok(result);
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
