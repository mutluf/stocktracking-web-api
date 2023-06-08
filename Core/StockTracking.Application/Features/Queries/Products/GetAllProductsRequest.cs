using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries.Products
{

    public class GetAllProductsRequest : IRequest<GetAllProductsResponse>
    {
    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products = _productRepository.GetAll();
            return new()
            {
                Products = products
            };
        }
    }

    public class GetAllProductsResponse
    {
        public IQueryable<Product>? Products { get; set; }
    }
}
