using MediatR;
using Microsoft.EntityFrameworkCore;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries.Categories
{
    public class GetStockMovementTypeByIdRequest : IRequest<GetStockMovementTypeByIdResponse>
    {
        public int Id { get; set; }
    }
    public class GetStockMovementTypeByIdHandler : IRequestHandler<GetStockMovementTypeByIdRequest, GetStockMovementTypeByIdResponse>
    {
        IStockMovementTypeRepository _stockMovementTypeRepository;

        public GetStockMovementTypeByIdHandler(IStockMovementTypeRepository stockMovementTypeRepository)
        {
            _stockMovementTypeRepository = stockMovementTypeRepository;
        }

        public async Task<GetStockMovementTypeByIdResponse> Handle(GetStockMovementTypeByIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<StockMovementType> stockMovementType = _stockMovementTypeRepository.GetWhere(p => p.Id == request.Id);
            return new()
            {
                StockMovementType = stockMovementType,
            };
        }
    }
    public class GetStockMovementTypeByIdResponse
    {
        public IQueryable<StockMovementType> StockMovementType { get; set; }
    }
}
