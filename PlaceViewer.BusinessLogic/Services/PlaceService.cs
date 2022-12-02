using PlaceViewer.BusinessLogic.Interfaces.Services;
using PlaceViewer.BusinessLogic.Interfaces.Storages;
using PlaceViewer.Domain.Models;

namespace PlaceViewer.BusinessLogic.Services;

public class PlaceService : IPlaceService
{
    private readonly IFileStorage _fileStorage;
    private readonly IPlaceStorage _placeStorage;

    public PlaceService
    (
        IPlaceStorage placeStorage,
        IFileStorage fileStorage
    )
    {
        ArgumentNullException.ThrowIfNull
        (
            placeStorage,
            nameof(placeStorage)
        );
        ArgumentNullException.ThrowIfNull
        (
            fileStorage,
            nameof(fileStorage)
        );

        _placeStorage = placeStorage;
        _fileStorage = fileStorage;
    }

    public Task<Place> SearchByName(string name)
    {
        name = FormatName(name);

        return _placeStorage.Get(p => p.Name == name);
    }

    public async Task IncreaseViews(string name)
    {
        var place = await SearchByName(name);
        if (place is null) return;

        place.ViewCount++;

        await _placeStorage.Update(place);
    }

    public async Task AddPlace
    (
        string name,
        Stream image
    )
    {
        var place = await SearchByName(name);
        if (place is not null) return;

        var imageUrl = await _fileStorage.SaveFile(image);
        place = new Place
        {
            ImageUrl = imageUrl,
            Name = FormatName(name)
        };

        await _placeStorage.Add(place);
    }

    public async Task<IReadOnlyCollection<Place>> GetMostViewed(int count)
    {
        return await _placeStorage.TakeOrdered
        (
            p => p.ViewCount,
            true,
            count
        );
    }

    private static string FormatName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        return name.ToLower().Trim().Replace
        (
            "  ",
            " "
        );
    }
}