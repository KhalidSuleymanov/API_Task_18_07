using FluentValidation;

namespace ShopApp.Service.Dtos.BrandDtos
{
    public class BrandCreateDto
    {
        public string Name { get; set; }

    }
    public class BrandCreateDtoValidatior : AbstractValidator<BrandCreateDto>
    {
        public BrandCreateDtoValidatior()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        }
    }
}
