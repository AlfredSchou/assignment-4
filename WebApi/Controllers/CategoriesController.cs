using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;
     
    public CategoriesController(
        IDataService dataService,
        LinkGenerator linkGenerator) 
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _dataService
            .GetCategories()
            .Select(CreateCategoryModel);
        return Ok(categories);
    }

    [HttpGet("{id}", Name = nameof(GetCategory))]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }
        var model = CreateCategoryModel(category);

        return Ok(model);
    }

   

    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryModel model)
    {
        var category = _dataService.CreateCategory(model.Name, model.Description);
        var categoryModel = CreateCategoryModel(category);
        return CreatedAtRoute(nameof(GetCategory),new { id = categoryModel.Url },categoryModel);
        
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var result = _dataService.DeleteCategory(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, UpdateCategoryModel model)
    {
        var category = _dataService.GetCategory(id);

        if(category == null)
        {
            return NotFound();
        }
        category.Name = model.Name;
        category.Description = model.Description;

        bool success = _dataService.UpdateCategory(id, model.Name, model.Description);


        if (!success)
        {
            return BadRequest("failed to update category."); 
        }


        return Ok();
    }



    private CategoryModel? CreateCategoryModel(Category? category)
    {
        if(category == null)
        {
            return null;
        }

        var model = category.Adapt<CategoryModel>();
        model.Url = GetUrl(category.Id);

        return model;
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { id });
    }
}
