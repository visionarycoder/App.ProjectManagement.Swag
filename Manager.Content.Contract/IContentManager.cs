using Manager.Content.Contract.Models;

namespace Manager.Content.Contract;

public interface IContentManager
{
    Task<Swag> CalculateSwag(int optimistic, int mostLikely, int pessimistic);
}