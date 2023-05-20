using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands.Categories
{
    public class DeleteStockMovementTypeRequest : IRequest<DeleteStockMovementTypeResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteStockMovementTypeHandler : IRequestHandler<DeleteStockMovementTypeRequest, DeleteStockMovementTypeResponse>
    {
        private readonly IStockMovementTypeRepository _stockMovementTypeRepository;

        public DeleteStockMovementTypeHandler(IStockMovementTypeRepository stockMovementTypeRepository)
        {
            _stockMovementTypeRepository = stockMovementTypeRepository;
        }
        public async Task<DeleteStockMovementTypeResponse> Handle(DeleteStockMovementTypeRequest request, CancellationToken cancellationToken)
        {
            StockMovementType stockMovementType = await _stockMovementTypeRepository.GetByIdAysnc(request.Id.ToString());
            _stockMovementTypeRepository.Delete(stockMovementType);
            await _stockMovementTypeRepository.SaveAysnc();

            return new()
            {
                Message = "silindi"
            };
        }
    }

    public class DeleteStockMovementTypeResponse
    {
        public string Message { get; set; }
    }
}
