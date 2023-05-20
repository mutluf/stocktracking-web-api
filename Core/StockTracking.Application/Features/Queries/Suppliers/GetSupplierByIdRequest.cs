using MediatR;
using Microsoft.EntityFrameworkCore;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries.Suppliers
{
    public class GetSupplierByIdRequest : IRequest<GetSupplierByIdResponse>
    {
        public string Id { get; set; }
    }
    public class GetSupplierByIdHandler : IRequestHandler<GetSupplierByIdRequest, GetSupplierByIdResponse>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSupplierByIdHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<GetSupplierByIdResponse> Handle(GetSupplierByIdRequest request, CancellationToken cancellationToken)
        {
            var datas = _supplierRepository.Table.Include(p => p.Products).Select(data => new GetSupplierByIdResponse()
            {
                Name = data.Name,
                Address = data.Address,
                Id = data.Id,
                PhoneNumber = data.PhoneNumber,
                Email=data.Email,
                Products = data.Products,

            }).FirstOrDefault();
            return datas;
        }
    }
    public class GetSupplierByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
