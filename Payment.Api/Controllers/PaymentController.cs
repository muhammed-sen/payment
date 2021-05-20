using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.Core.Features.Queries;
using Payment.Core.Features.Commands;
using System.Threading.Tasks;
using System;

namespace Payment.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]

    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetBalances()
        {
            return Ok(await _mediator.Send(new GetBalancesQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(UpdateAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
            /*try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (ArgumentException)
            {

            }
            catch ()
            {

            }*/

            //throw new InvalidOperationException("hhh"); //new UnprocessableEntityObjectResult("dddd");
            // return Ok(await _mediator.Send(command));
        }
    }
}
