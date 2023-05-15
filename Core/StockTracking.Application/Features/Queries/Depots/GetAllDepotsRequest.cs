

using MediatR;
using Microsoft.EntityFrameworkCore;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries.Depots
{
    public class GetAllDepotsRequest : IRequest<GetAllDepotsResponse>
    {

    }
    public class GetAllDepotsHandler : IRequestHandler<GetAllDepotsRequest, GetAllDepotsResponse>
    {
        private readonly IDepotRepository _depotRepository;

        public GetAllDepotsHandler(IDepotRepository depotRepository)
        {
            _depotRepository = depotRepository;
        }
        public async Task<GetAllDepotsResponse> Handle(GetAllDepotsRequest request, CancellationToken cancellationToken)
        {
            var datas = _depotRepository.Table.Include(p => p.Products).Include(s => s.StockMovements).Select(data => new Depot()
            {
                StockMovements = data.StockMovements,
                Address = data.Address,
                DepotName = data.DepotName,
                Id = data.Id,
                PhoneNumber = data.PhoneNumber,
                Products = data.Products,

            });
            return new()
            {
                Depots = datas,
            };
        }
    }
    public class GetAllDepotsResponse
    {
        public IQueryable<Depot> Depots  { get; set; }
    }
}
