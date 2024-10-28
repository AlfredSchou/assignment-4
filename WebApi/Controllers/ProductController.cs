using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/product")]

public class ProductController: ControllerBase
{   
    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public ProductController(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("{id}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }
        var model = CreateProductModel(product);

        return Ok(model);
    }


    private ProductModel CreateProductModel(ProductDto? product)
    {
        if (product == null)
        {
            return null;
        }

        var model = product.Adapt<ProductModel>();
        model.CategoryName = product.CategoryName;
        model.Url = GetUrl(product.Id);

        return model;
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { id });
    }

}