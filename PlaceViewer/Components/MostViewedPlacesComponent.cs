using Microsoft.AspNetCore.Mvc;
using PlaceViewer.BusinessLogic.Interfaces.Services;

namespace PlaceViewer.Components;

public class MostViewedPlacesComponent : ViewComponent
{
    private readonly IPlaceService _service;

    public MostViewedPlacesComponent(IPlaceService service)
    {
        ArgumentNullException.ThrowIfNull(service);

        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync(int count = 5)
    {
        var result = await _service.GetMostViewed(count);

        return View
        (
            "MostViewedPlaces",
            result
        );
    }
}