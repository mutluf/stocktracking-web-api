using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.Application.Features.Commands.Depots
{
    public class CreateDepotRequest : IRequest<CreateDepotResponse>
    {
        public int Id { get; set; }
        public string DepotName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class CreateDepotHandler : IRequestHandler<CreateDepotRequest, CreateDepotResponse>
    {
        private readonly IDepotRepository _depotRepository;
        private readonly IMapper _mapper;

        public CreateDepotHandler(IDepotRepository depotRepository, IMapper mapper)
        {
            _depotRepository = depotRepository;
            _mapper = mapper;
        }

        public async Task<CreateDepotResponse> Handle(CreateDepotRequest request, CancellationToken cancellationToken)
        {
            Depot depot = _mapper.Map<Depot>(request);
            await _depotRepository.AddAysnc(depot);
            await _depotRepository.SaveAysnc();
            return new()
            {
                Message = "Kayıt başarılı!"
            };
            
        }
    }
    public class CreateDepotResponse
    {
        public string Message { get; set; }
    }
}
