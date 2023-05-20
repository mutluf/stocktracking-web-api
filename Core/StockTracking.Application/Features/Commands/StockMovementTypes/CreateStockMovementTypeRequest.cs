using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands.Categories
{
    public class CreateStockMovementTypeRequest : IRequest<CreateStockMovementTypeResponse>
    {
        public string Type { get; set; }
    }
    public class CreateStockMovementTypeHandler : IRequestHandler<CreateStockMovementTypeRequest, CreateStockMovementTypeResponse>
    {
        IMapper _mapper;
        private readonly IStockMovementTypeRepository _stockMovementTypeRepository;

        public CreateStockMovementTypeHandler(IMapper mapper, IStockMovementTypeRepository stockMovementTypeRepository)
        {
            _stockMovementTypeRepository = stockMovementTypeRepository;
            _mapper = mapper;
        }

        public async Task<CreateStockMovementTypeResponse> Handle(CreateStockMovementTypeRequest request, CancellationToken cancellationToken)
        {
            StockMovementType stockMovementTypeRepository = _mapper.Map<StockMovementType>(request);
            await _stockMovementTypeRepository.AddAysnc(stockMovementTypeRepository);
            await _stockMovementTypeRepository.SaveAysnc();

            return new()
            {
                Message = "Stok hareketi başarıyla eklendi."
            };
        }
    }
    public class CreateStockMovementTypeResponse
    {
        public string Message { get; set; }
    }
}
