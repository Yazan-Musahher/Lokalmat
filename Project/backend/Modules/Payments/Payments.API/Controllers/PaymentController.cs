using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Payment.Commands.CreatePayment;
using Payments.Application.Payment.Commands.UpdatePaymentStatus;
using Payments.Application.Payment.Models;
using Payments.Contracts.Payment.CreatePayment;

namespace Payments.API.Controllers;

[Route("payments")]
public class PaymentsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PaymentsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment(CreatePaymentRequest request)
    {
        var command = _mapper.Map<CreatePaymentCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            paymentDetails =>{
                var response = _mapper.Map<CreatePaymentResponse>(paymentDetails.Item1);
                var paymentResult = _mapper.Map<PaymentResultResponse>(new PaymentResult
                {
                    SessionId = paymentDetails.Item2.SessionId,
                    Url = paymentDetails.Item2.Url
                });
                return Ok(response with { PaymentResultResponse = paymentResult });
            },
            errors => Problem(errors));
    }
    
    [HttpGet("confirm-payment")]
    public async Task<IActionResult> ConfirmPayment([FromQuery] Guid paymentId, [FromQuery] string sessionId)
    {
        var command = new ConfirmPaymentCommand(paymentId, sessionId);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}
