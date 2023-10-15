using MediatR;
using StockTracking.Application.Abstractions.Services;
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
        private readonly ICacheService _cacheService;


        public GetAllCategoriesHandler(ICategoryRepository categoryRepository, ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _cacheService = cacheService;
        }

        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {

            List<Category> categories = await _cacheService.GetOrAddAsync("categories", async () =>
            {

                categories = _categoryRepository.GetAll().ToList();
                return categories;
            });

            //return categories;
            //    IQueryable<Category> categories = _categoryRepository.GetAll();
            return new()
            {
                Categories = categories,
            };
            //}
        }
       
    }

    public class GetAllCategoriesResponse
    {
        public List<Category> Categories { get; set; }
    }
}
