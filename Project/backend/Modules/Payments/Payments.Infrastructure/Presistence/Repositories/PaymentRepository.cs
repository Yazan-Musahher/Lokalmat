using Mapster;

using MongoDB.Driver;
using Payments.Application.Interfaces;
using Payments.Application.Interfaces.Presistence;
using Payments.Domain.PaymentAggregate;
using Payments.Infrastructure.Models;

namespace Payments.IntegrationEvents.Presistence.Repositories;


public sealed class PaymentRepository : IPaymentRepository
{
    private readonly IMongoCollection<PaymentDto> _paymets;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentRepository(IUnitOfWork unitOfWork, IMongoDatabase database)
    {
        _paymets = database.GetCollection<PaymentDto>("PaymentDetailsDocument");
        _unitOfWork = unitOfWork;
    }

    public async Task AddPaymentAsync(PaymentAggregate payment, CancellationToken cancellationToken)
    {
        var paymentDto = payment.Adapt<PaymentDto>();
        Task operation = _paymets.InsertOneAsync(_unitOfWork.Session as IClientSessionHandle, paymentDto);
        await _unitOfWork.AddOperation(operation);
    }

    public async Task<PaymentAggregate?> GetPaymentId(Guid paymentId, CancellationToken cancellationToken)
    {
        var paymentDto = await _paymets.Find(p => p.Id == paymentId).SingleOrDefaultAsync(cancellationToken);
        if (paymentDto == null) return null;
        return paymentDto.Adapt<PaymentAggregate>();
    }

    public async Task UpdatePaymentStatusAsync(PaymentAggregate payment, CancellationToken cancellationToken)
    {
        var paymentDto = payment.Adapt<PaymentDto>();
        Task operation = _paymets.ReplaceOneAsync(p => p.Id == payment.Id.Value, paymentDto);
        await _unitOfWork.AddOperation(operation);
    }
}