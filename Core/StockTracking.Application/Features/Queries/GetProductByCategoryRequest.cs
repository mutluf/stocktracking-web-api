using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries
{
    public class GetProductByCategoryRequest:IRequest<GetProductByCategoryResponse>
    {
        public string CategoryName { get; set; }
    }

    public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategoryRequest, GetProductByCategoryResponse>
    {
        IProductRepository _productRepository;

        public GetProductByCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products =  _productRepository.GetWhere(p => p.Category.CategoryName == request.CategoryName);

            return new()
            {
                Products = products,
            };
        }
    }

    public class GetProductByCategoryResponse
    {
        public IQueryable<Product> Products { get; set; }   
    }
}
