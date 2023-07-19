﻿using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Service.Dtos.BrandDtos
{
    public class BrandEditDto
    {
        public string Name { get; set; }
    }

    public class BrandEditDtoValidatior : AbstractValidator<BrandEditDto>
    {
        public BrandEditDtoValidatior()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        }
    }
}
