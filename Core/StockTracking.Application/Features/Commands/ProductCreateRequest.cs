using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands
{
    public class ProductCreateRequest : IRequest<ProductCreateResponse>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string BarcodeNumber { get; set; }
        public string Brand { get; set; }
        public int Stock { get; set; }
        public int MinimumStock { get; set; }

        public int SupplierId { get; set; }

        public int DepotId { get; set; }


        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }

    public class ProductCreateHandler : IRequestHandler<ProductCreateRequest, ProductCreateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductCreateHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductCreateResponse> Handle(ProductCreateRequest request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            bool result = await _productRepository.AddAysnc(product);
            await _productRepository.SaveAysnc();

            int productId = product.Id;

            return new()
            {
                ProductId = productId,
            };

        }
    }

    public class ProductCreateResponse
    {
        public int ProductId { get; set; }
    }
}
