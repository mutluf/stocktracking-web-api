using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;


namespace StockTracking.Application.Features.Commands.Depots
{
    public class DeleteDepotRequest : IRequest<DeleteDepotResponse>
    {
        public string Id { get; set; }
        
    }
    public class DeleteDepotHandler : IRequestHandler<DeleteDepotRequest, DeleteDepotResponse>
    {
        private readonly IDepotRepository _depotRepository;
       

        public DeleteDepotHandler(IDepotRepository depotRepository)
        {
            _depotRepository = depotRepository;
            
        }

        public async Task<DeleteDepotResponse> Handle(DeleteDepotRequest request, CancellationToken cancellationToken)
        {
            await _depotRepository.GetByIdAysnc(request.Id);
            
            await _depotRepository.SaveAysnc();
            return new()
            {
                Message = "Silme başarılı!"
            };

        }
    }
    public class DeleteDepotResponse
    {
        public string Message { get; set; }
    }
}


