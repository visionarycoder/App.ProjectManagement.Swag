namespace Swag.Components.Manager.Content.Service.Helpers;

public static class SwagExtension
{
    
    public static Swag Convert(this Access.Storage.Contract.Models.Swag source)
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

    public static Access.Storage.Contract.Models.Swag Convert(this Swag source)
    {

        var target = new Access.Storage.Contract.Models.Swag
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