﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApp.Api.Dtos.BrandDtos;
using ShopApp.Core.Entities;
using ShopApp.Core.Repositories;

namespace ShopApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly IBrandRepository _brandRepository;

        public BrandsController(ShopDbContext context, IBrandRepository brandRepository)
        {
            _context = context;
            _brandRepository = brandRepository;
        }

        [HttpGet("{all}")]
        public ActionResult<List<BrandGetAllItemDto>> GetAll () 
        {
            var brandDtos = _brandRepository.GetQueryable(x=>x.Products.Count>0).Select(b => new BrandGetAllItemDto { Id = b.Id, Name=b.Name }).ToList();
            return Ok(brandDtos);
        }


        [HttpGet("{id}")]
        public ActionResult<BrandGetDto> Get(int id)
        {
            Brand brand = _brandRepository.Get(b => b.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            BrandGetDto brandDto = new BrandGetDto
            {
                Name = brand.Name,
                ProductsCount = _context.Products.Where(x=>x.BrandId==id).Count(),
            };
            return Ok(brandDto);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(BrandCreateDto brandDto)
        {
            if (_brandRepository.IsExist(x => x.Name == brandDto.Name))
            {
                ModelState.AddModelError("Name", "Name is already taken");
                return BadRequest(ModelState);
            }
            Brand brand = new Brand
            {
                Name = brandDto.Name,
            };
            _brandRepository.Add(brand);
            _brandRepository.Commit();
            return StatusCode(201, new {Id=brand.Id}); 
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, BrandEditDto brandDto)
        {
            Brand brand = _brandRepository.Get(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            if (brand.Name != brandDto.Name && _brandRepository.IsExist(x=>x.Name==brandDto.Name))
            {
                ModelState.AddModelError("Name", "Name is already taken");
                return BadRequest(ModelState);
            }
            brand.Name = brandDto.Name;
            _brandRepository.Commit();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Brand brand = _brandRepository.Get(b => b.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            _brandRepository.Remove(brand);
            _brandRepository.Commit();
            return NoContent();
        }
    }
}