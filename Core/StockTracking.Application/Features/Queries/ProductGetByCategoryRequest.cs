﻿using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries
{
    public class ProductGetByCategoryRequest:IRequest<ProductGetByCategoryResponse>
    {
        public string CategoryName { get; set; }
    }

    public class ProductGetByCategoryHandler : IRequestHandler<ProductGetByCategoryRequest, ProductGetByCategoryResponse>
    {
        IProductRepository _productRepository;

        public ProductGetByCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductGetByCategoryResponse> Handle(ProductGetByCategoryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products =  _productRepository.GetWhere(p => p.Category.CategoryName == request.CategoryName);

            return new()
            {
                Products = products,
            };
        }
    }

    public class ProductGetByCategoryResponse
    {
        public IQueryable<Product> Products { get; set; }   
    }
}
