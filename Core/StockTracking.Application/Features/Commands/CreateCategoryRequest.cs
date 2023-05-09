using AutoMapper;
using MediatR;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Features.Commands
{
    public class CreateCategoryRequest:IRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        

        public CreateCategoryHandler(IMapper mapper, ICategoryRepository categoryRepository )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category =  _mapper.Map<Category>(request);
            await _categoryRepository.AddAysnc(category);
            await _categoryRepository.SaveAysnc();

            return new()
            {
                Message = "Kategori başarıyla eklendi."
            };
        }
    }

    public class CreateCategoryResponse
    {
        public string Message { get; set; }
    }
}
