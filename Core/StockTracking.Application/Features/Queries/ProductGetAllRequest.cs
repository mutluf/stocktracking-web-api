using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.Application.Features.Queries
{

    public class ProductGetAllRequest : IRequest<ProductGetAllResponse>
    {
    }

    public class ProductGetAllHandler : IRequestHandler<ProductGetAllRequest, ProductGetAllResponse>
    {
        private readonly IProductRepository _productRepository;

        public ProductGetAllHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductGetAllResponse> Handle(ProductGetAllRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products = _productRepository.GetAll();
            return new()
            {
                Products = products
            };
        }
    }

    public class ProductGetAllResponse
    {
        public IQueryable<Product>? Products { get; set; }
    }
}
