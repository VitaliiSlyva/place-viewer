using System.Linq.Expressions;
using PlaceViewer.Domain.Models;

namespace PlaceViewer.BusinessLogic.Interfaces.Storages;

public interface IPlaceStorage
{
    Task<Place> Get(Expression<Func<Place, bool>> predicate);

    Task Add(Place place);

    Task Update(Place place);

    Task<List<Place>> TakeOrdered<TKey>
    (
        Expression<Func<Place, TKey>> orderKey,
        bool isDesc,
        int count
    );
}