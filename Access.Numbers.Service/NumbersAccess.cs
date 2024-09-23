using Access.Numbers.Contract;
using Access.Numbers.Contract.Models;
using Access.Numbers.Service.Helpers;
using Ifx.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Resource.Data.NumbersDB;

namespace Access.Numbers.Service;

public class NumbersAccess(ILogger<NumbersAccess> logger, DbContextOptions<NumbersContext> options) : INumbersAccess
{

    private readonly NumbersContext ctx = DbHelper
        .GetContextAsync(dbContextOptions: options)
        .GetAwaiter()
        .GetResult();

    public async Task<IEnumerable<Swag>> GetSwags()
    {

        try
        {

            var query = ctx.Swags.AsQueryable();
            query = query.OrderBy(i => i.Id);
            var entities = await query.ToListAsync();
            var swags = entities.Convert();
            return swags;

        }
        catch (Exception ex)
        {

            logger.LogError(ex, ex.Message);
            return Array.Empty<Swag>();

        }

    }

    public async Task<(Swag? Swag, bool successful)> GetSwag(int id)
    {

        try
        {
            
            var query = ctx.Swags.AsQueryable();
            var entity = await query.SingleOrDefaultAsync(i => i.Id == id);
            if (entity is null)
            {
                return (null, false);
            }

            var swag = entity.Convert();
            return (swag, true);

        }
        catch (Exception ex)
        {

            logger.LogError(ex, ex.Message);
            return (null, false);

        }

    }

    public async Task<(Swag Swag, bool EntryFound)> GetSwag(int optimistic, int mostLikely, int pessimistic)
    {

        try
        {
        
            var query = ctx.Swags.AsQueryable();
            var entity = await query.SingleOrDefaultAsync(i => i.Optimistic == optimistic && i.MostLikely == mostLikely && i.Pessimistic == pessimistic);
            if (entity is null)
            {
                return (null, false);
            }

            var swag = entity.Convert();
            return (swag, true);

        }
        catch (Exception ex)
        {

            logger.LogError(ex, ex.Message);
            return (null, false);

        }

    }

    public async Task<(Swag Swag, bool successful)> AddSwag(Swag swag)
    {

        try
        {
        
            var alreadyExists = await ctx.Swags.AnyAsync(i => i.Optimistic == swag.Optimistic && i.MostLikely == swag.MostLikely && i.Pessimistic == swag.Pessimistic);
            if (alreadyExists)
            {
                return (swag, false);
            }

            var entity = swag.Convert();
            var entityEntry = ctx.Swags.Add(entity);
            var count = await ctx.SaveChangesAsync();
            if (count == 0)
            {
                return (null, false);
            }

            var dto = entityEntry.Entity.Convert();
            return (dto, true);

        }
        catch (Exception ex)
        {

            logger.LogError(ex, ex.Message);
            return (null, false);

        }

    }

    public async Task<(Swag? swag, bool successful)> UpdateSwag(Swag swag)
    {

        try
        {

            var entity = await ctx.Swags.SingleOrDefaultAsync(i => i.Id == swag.Id);
            if (entity is null)
            {
                return (null, false);
            }

            var entityEntry = ctx.Swags.Update(entity);
            var count = await ctx.SaveChangesAsync();
            if (count == 0)
            {
                return (null, false);
            }

            var dto = entityEntry.Entity.Convert();
            return (dto, true);

        }
        catch (Exception ex)
        {
            
            logger.LogError(ex, "Error in {Method}", nameof(UpdateSwag));
            return (null, false);

        }

    }

    public async Task<bool> DeleteSwag(int id)
    {

        try
        {

            var entityEntry = ctx.Remove(new Resource.Data.NumbersDB.Models.Swag { Id = id });
            var count = await ctx.SaveChangesAsync();
            return count != 0;

        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Error in {Method}", nameof(DeleteSwag));
            return false;

        }

    }

}