using PlaceViewer.Domain.Models;

namespace PlaceViewer.BusinessLogic.Interfaces.Services;

public interface IPlaceService
{
    Task<Place> SearchByName(string name);

    Task IncreaseViews(string name);

    Task AddPlace
    (
        string name,
        Stream image
    );

    Task<IReadOnlyCollection<Place>> GetMostViewed(int count);
}