namespace Swag.Components.Access.Storage.Contract;

public interface INumbersAccess 
{
    Task<IEnumerable<Models.Swag>> GetSwags();
    Task<(Models.Swag Swag, bool EntryFound)> GetSwag(int optimistic, int mostLikely, int pessimistic);
    Task<(Models.Swag Swag, bool successful)> AddSwag(Models.Swag swag);
    Task<(Models.Swag? swag, bool successful)> UpdateSwag(Models.Swag swag);
    Task<bool> DeleteSwag(int id);
}