using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;


namespace StockTracking.Application.Features.Commands.Suppliers
{
    public class UpdateSupplierRequest : IRequest<UpdateSupplierResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
    public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierRequest, UpdateSupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public UpdateSupplierHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSupplierResponse> Handle(UpdateSupplierRequest request, CancellationToken cancellationToken)
        {
            Supplier supplier = _mapper.Map<Supplier>(request);
            _supplierRepository.Update(supplier);
            await _supplierRepository.SaveAysnc();
            return new()
            {
                Message = "Güncelleme başarılı!"
            };
        }
    }
    public class UpdateSupplierResponse
    {
        public string? Message { get; set; }
    }
}


