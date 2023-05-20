using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries.Suppliers
{
    public class GetAllSuppliersRequest : IRequest<GetAllSuppliersResponse>
    {
    }
    public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersRequest, GetAllSuppliersResponse>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetAllSuppliersHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<GetAllSuppliersResponse> Handle(GetAllSuppliersRequest request, CancellationToken cancellationToken)
        {
            var datas = _supplierRepository.GetAll();
            return new()
            {
                Suppliers = datas,
            };
        }
    }
    public class GetAllSuppliersResponse
    {
        public IQueryable<Supplier> Suppliers { get; set; }
    }
}
