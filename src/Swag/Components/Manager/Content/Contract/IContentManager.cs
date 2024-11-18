namespace Swag.Components.Manager.Content.Contract;

public interface IContentManager
{
    Task<Swag> CalculateSwag(int optimistic, int mostLikely, int pessimistic);
}