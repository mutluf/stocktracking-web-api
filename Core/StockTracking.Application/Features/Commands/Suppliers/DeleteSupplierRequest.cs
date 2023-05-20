using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands.Suppliers
{
    public class DeleteSupplierRequest : IRequest<DeleteSupplierResponse>
    {
        public string Id { get; set; }       
    }
    public class DeleteSupplierHandler : IRequestHandler<DeleteSupplierRequest, DeleteSupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;

        public DeleteSupplierHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;           
        }

        public async Task<DeleteSupplierResponse> Handle(DeleteSupplierRequest request, CancellationToken cancellationToken)
        {
            Supplier supplier = await _supplierRepository.GetByIdAysnc(request.Id);
            _supplierRepository.Delete(supplier);

            await _supplierRepository.SaveAysnc();
            return new()
            {
                Message = "Silme başarılı!"
            };
        }
    }
    public class DeleteSupplierResponse
    {
        public string? Message { get; set; }
    }
}


