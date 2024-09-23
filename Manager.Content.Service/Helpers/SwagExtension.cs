using Manager.Content.Contract.Models;

namespace Manager.Content.Service.Helpers;

public static class SwagExtension
{
    
    public static Swag Convert(this Access.Numbers.Contract.Models.Swag source)
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

    public static Access.Numbers.Contract.Models.Swag Convert(this Swag source)
    {

        var target = new Access.Numbers.Contract.Models.Swag
        {
            Id = source.Id,
            Optimistic = source.Optimistic,
            MostLikely = source.MostLikely,
            Pessimistic = source.Pessimistic,
            Calculated = source.Calculated
        };
        return target;

    }


}