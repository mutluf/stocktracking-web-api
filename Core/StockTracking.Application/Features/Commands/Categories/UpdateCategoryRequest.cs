using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands.Categories
{
    public class UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category = _mapper.Map<Category>(request);
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAysnc();

            return new()
            {
                Message = "Category güncellendi"
            };
        }
    }
    public class UpdateCategoryResponse
    {
        public string? Message { get; set; }
    }
}
