using Microsoft.AspNetCore.Mvc;
using PlaceViewer.BusinessLogic.Interfaces.Services;
using PlaceViewer.Domain.Models;
using PlaceViewer.ViewModels;

namespace PlaceViewer.Controllers;

[Route("places")]
public class PlaceController : Controller
{
    private readonly IPlaceService _service;

    public PlaceController(IPlaceService service)
    {
        ArgumentNullException.ThrowIfNull(service);

        _service = service;
    }

    [HttpGet("search")]
    public IActionResult GetSearch()
    {
        return View(nameof(Search));
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromForm] SearchPlaceModel model)
    {
        if (!ModelState.IsValid)
            return View
            (
                nameof(Search),
                model
            );

        var place = await FindPlace(model.Name);

        return View
        (
            "Place",
            place
        );
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> Get([FromRoute] string name)
    {
        var place = await FindPlace(name);

        return View
        (
            "Place",
            place
        );
    }

    [HttpGet("create/{name}")]
    public IActionResult GetCreate(string name)
    {
        return View
        (
            nameof(Create),
            new CreatePlaceModel { Name = name }
        );
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] CreatePlaceModel model)
    {
        if (!ModelState.IsValid || model.Image?.ContentType != "image/jpeg") return View(model);

        await using (var imageStream = model.Image.OpenReadStream())
        {
            await _service.AddPlace
            (
                model.Name,
                imageStream
            );
        }

        return RedirectToAction
        (
            nameof(Get),
            new { name = model.Name }
        );
    }

    private async Task<Place> FindPlace(string name)
    {
        var place = await _service.SearchByName(name);
        if (place is not null)
        {
            await _service.IncreaseViews(name);
            place.ViewCount++;
        }
        else
        {
            place = new Place { Name = name };
        }

        return place;
    }
}