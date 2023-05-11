using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Queries.Categories
{
    public class GetAllCategoriesRequest : IRequest<GetAllCategoriesResponse>
    {
    }
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Category> categories = _categoryRepository.GetAll();
            return new()
            {
                Categories = categories,
            };
        }
    }
    public class GetAllCategoriesResponse
    {
        public IQueryable<Category> Categories { get; set; }
    }
}
