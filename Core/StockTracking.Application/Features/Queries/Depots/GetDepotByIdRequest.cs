using MediatR;
using Microsoft.EntityFrameworkCore;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.Application.Features.Queries.Depots
{
    public class GetDepotByIdRequest : IRequest<GetDepotByIdResponse>
    {
        public string Id { get; set; }
    }
    public class GetDepotByIdHandler : IRequestHandler<GetDepotByIdRequest, GetDepotByIdResponse>
    {
        private readonly IDepotRepository _depotRepository;

        public GetDepotByIdHandler(IDepotRepository depotRepository)
        {
            _depotRepository = depotRepository;
        }

        public async Task<GetDepotByIdResponse> Handle(GetDepotByIdRequest request, CancellationToken cancellationToken)
        {
           var datas = _depotRepository.Table.Include(p => p.Products).Include(s => s.StockMovements).Select(data=> new GetDepotByIdResponse()
            {
                StockMovements = data.StockMovements,
                Address = data.Address,
                DepotName = data.DepotName,
                Id = data.Id,
                PhoneNumber = data.PhoneNumber,
                Products = data.Products,
                
            }).FirstOrDefault();
            return datas;
        }
    }
    public class GetDepotByIdResponse
    {
        public int Id{ get; set; }
        public string DepotName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<StockMovement> StockMovements { get; set; }

    }
}
