using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands
{
    public class DeleteCategoryRequest:IRequest<DeleteCategoryResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, DeleteCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetByIdAysnc(request.Id.ToString());
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveAysnc();

            return new()
            {
                Message = "silindi"
            };
        }
    }

    public class DeleteCategoryResponse
    {
        public string Message { get; set; }
    }
}
