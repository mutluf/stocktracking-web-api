using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands.Categories
{
    public class UpdateStockMovementTypeRequest : IRequest<UpdateStockMovementTypeResponse>
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
    public class UpdateStockMovementTypeHandler : IRequestHandler<UpdateStockMovementTypeRequest, UpdateStockMovementTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockMovementTypeRepository _stockMovementTypeRepository;
        public UpdateStockMovementTypeHandler(IMapper mapper, IStockMovementTypeRepository stockMovementTypeRepository)
        {
            _mapper = mapper;
            _stockMovementTypeRepository = stockMovementTypeRepository;
        }
        public async Task<UpdateStockMovementTypeResponse> Handle(UpdateStockMovementTypeRequest request, CancellationToken cancellationToken)
        {
            StockMovementType category = _mapper.Map<StockMovementType>(request);
            _stockMovementTypeRepository.Update(category);
            await _stockMovementTypeRepository.SaveAysnc();

            return new()
            {
                Message = "Stok hareketi güncellendi"
            };
        }
    }
    public class UpdateStockMovementTypeResponse
    {
        public string? Message { get; set; }
    }
}
