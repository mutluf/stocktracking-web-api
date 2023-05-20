using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands.Suppliers
{
    public class CreateSupplierRequest : IRequest<CreateSupplierResponse>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
    public class CreateSupplierHandler : IRequestHandler<CreateSupplierRequest, CreateSupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public CreateSupplierHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<CreateSupplierResponse> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            Supplier supplier = _mapper.Map<Supplier>(request);
            await _supplierRepository.AddAysnc(supplier);

            await _supplierRepository.SaveAysnc();
            return new()
            {
                Message = "Kayıt başarılı!"
            };
            
        }

    }
    public class CreateSupplierResponse
    {
        public string? Message { get; set; }
    }
}
