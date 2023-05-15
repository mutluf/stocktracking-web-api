using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;


namespace StockTracking.Application.Features.Commands.Depots
{
    public class UpdateDepotRequest : IRequest<UpdateDepotResponse>
    {
        public int Id { get; set; }
        public string DepotName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UpdateDepotHandler : IRequestHandler<UpdateDepotRequest, UpdateDepotResponse>
    {
        private readonly IDepotRepository _depotRepository;
        private readonly IMapper _mapper;

        public UpdateDepotHandler(IDepotRepository depotRepository, IMapper mapper)
        {
            _depotRepository = depotRepository;
            _mapper = mapper;
        }

        public async Task<UpdateDepotResponse> Handle(UpdateDepotRequest request, CancellationToken cancellationToken)
        {
            Depot depot = _mapper.Map<Depot>(request);
            _depotRepository.Update(depot);
            await _depotRepository.SaveAysnc();
            return new()
            {
                Message = "Güncelleme başarılı!"
            };

        }

    }
    public class UpdateDepotResponse
    {
        public string Message { get; set; }
    }
}


