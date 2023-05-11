using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands.Products
{
    public class UpdateProductRequest : IRequest<UpdateProductResponse>
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? BarcodeNumber { get; set; }
        public string? Brand { get; set; }
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
        public int SupplierId { get; set; }
        public int DepotId { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            _productRepository.Update(product);
            await _productRepository.SaveAysnc();

            return new()
            {
                Message = "Başarıyla güncellendi."
            };
        }
    }
    public class UpdateProductResponse
    {
        public string? Message { get; set; }
    }
}
