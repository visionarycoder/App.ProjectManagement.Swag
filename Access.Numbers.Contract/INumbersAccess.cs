using Access.Numbers.Contract.Models;

namespace Access.Numbers.Contract;

public interface INumbersAccess 
{
    Task<IEnumerable<Swag>> GetSwags();
    Task<(Swag Swag, bool EntryFound)> GetSwag(int optimistic, int mostLikely, int pessimistic);
    Task<(Swag Swag, bool successful)> AddSwag(Swag swag);
    Task<(Swag? swag, bool successful)> UpdateSwag(Swag swag);
    Task<bool> DeleteSwag(int id);
}