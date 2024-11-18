using Swag.Components.Access.Storage.Orm.Models;

namespace Swag.Components.Access.Storage.Service.Helpers;

public static class SwagExtension
{
    
    public static Swag Convert(this ThreePointEstimate source)
    {
        var target = new Swag
        {
            Id = source.Id,
            Optimistic = source.Optimistic,
            MostLikely = source.MostLikely,
            Pessimistic = source.Pessimistic,
            Calculated = source.Calculated
        };
        return target;
    }

    public static ThreePointEstimate Convert(this Swag source)
    {
        var target = new ThreePointEstimate
        {
            Id = source.Id,
            Optimistic = source.Optimistic,
            MostLikely = source.MostLikely,
            Pessimistic = source.Pessimistic,
            Calculated = source.Calculated
        };
        return target;
    }

    public static IEnumerable<Swag> Convert(this IEnumerable<ThreePointEstimate> source)
    {
        return source.Select(i => i.Convert());
    }

    public static IEnumerable<ThreePointEstimate> Convert(this IEnumerable<Swag> source)
    {
        return source.Select(i => i.Convert());
    }

}