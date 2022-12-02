using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PlaceViewer.BusinessLogic.Interfaces.Storages;
using PlaceViewer.DatabaseStorage.Infrastructure;
using PlaceViewer.Domain.Models;

namespace PlaceViewer.DatabaseStorage.Storages;

public class PlaceStorage : IPlaceStorage
{
    private readonly PlaceViewerDbContext _context;

    public PlaceStorage(PlaceViewerDbContext context)
    {
        ArgumentNullException.ThrowIfNull
        (
            context,
            nameof(context)
        );

        _context = context;
    }

    public Task<Place> Get(Expression<Func<Place, bool>> predicate)
    {
        return _context.Set<Place>().AsNoTracking().FirstOrDefaultAsync(predicate)!;
    }

    public async Task Add(Place place)
    {
        await _context.Set<Place>().AddAsync(place);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Place place)
    {
        _context.Set<Place>().Update(place);

        await _context.SaveChangesAsync();
    }

    public Task<List<Place>> TakeOrdered<TKey>
    (
        Expression<Func<Place, TKey>> orderKey,
        bool isDesc,
        int count
    )
    {
        var query = _context.Set<Place>().AsNoTracking();
        query = isDesc ? query.OrderByDescending(orderKey) : query.OrderBy(orderKey);

        return query.Take(count).ToListAsync();
    }
}