using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries.StockMovementTypes
{
    public class GetAllStockMovementTypesRequest : IRequest<GetAllStockMovementTypesResponse>
    {
    }
    public class GetAllStockMovementTypesHandler : IRequestHandler<GetAllStockMovementTypesRequest, GetAllStockMovementTypesResponse>
    {
        private readonly IStockMovementTypeRepository _stockMovementTypeRepository;

        public GetAllStockMovementTypesHandler(IStockMovementTypeRepository stockMovementTypeRepository)
        {
            _stockMovementTypeRepository = stockMovementTypeRepository;
        }

        public async Task<GetAllStockMovementTypesResponse> Handle(GetAllStockMovementTypesRequest request, CancellationToken cancellationToken)
        {
            IQueryable<StockMovementType> stockMovementType = _stockMovementTypeRepository.GetAll();
            return new()
            {
                StockMovementType = stockMovementType,
            };
        }
    }
    public class GetAllStockMovementTypesResponse
    {
        public IQueryable<StockMovementType> StockMovementType { get; set; }
    }
}
