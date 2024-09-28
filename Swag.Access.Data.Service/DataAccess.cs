using Microsoft.Extensions.Logging;
using Swag.Access.Data.Interface;
using Swag.Access.Data.Interface.Models;
using Swag.Access.Data.Service.Helpers;
using Swag.Resource.Data.SwagDb;

namespace Swag.Access.Data.Service;

public class DataAccess(ILogger<DataAccess> logger) : IDataAccess 
{

    public async Task<IEnumerable<Estimate>> GetEstimates()
    {

        try
        {
            
            var db = new SwagContext();
            var query = db.Estimates.AsQueryable();
            query = query.OrderBy(i => i.Id);
            var dbObjs = query.ToList();
            var dtos = dbObjs.Convert();
            return dtos;

        }
        catch (Exception ex)
        {
            
            logger.LogError(ex, "Error in {Method}", nameof(GetEstimates));
            return Array.Empty<Estimate>();

        }

    }
    
    public async Task<(Estimate? estimate, bool successful)> GetEstimate(int id)
    {

        try
        {

            var db = new SwagContext();
            var query = db.Estimates.AsQueryable();
            var dbObj = query.SingleOrDefault(i => i.Id == id);
            if (dbObj is null)
            {
                return (null, false);
            }

            var dto = dbObj.Convert();
            return (dto, true);

        }
        catch (Exception ex)
        {
            
            logger.LogError(ex, "Error in {Method}", nameof(GetEstimate));
            return (null, false);

        }
    }
    
    public async Task<Estimate?> GetEstimate(int optimistic, int mostLikely, int pessimistic)
    {

        try
        {

            var db = new SwagContext();
            var query = db.Estimates.AsQueryable();
            var dbo = query.SingleOrDefault(i => i.Optimistic == optimistic && i.MostLikely == mostLikely && i.Pessimistic == pessimistic);
            var dto = dbo?.Convert();
            return dto;

        }
        catch (Exception ex)
        {
            
            logger.LogError(ex, "Error in {Method}", nameof(GetEstimate));
            return null;

        }

    }
    
    public async Task<bool> AddEstimate(Estimate estimate)
    {

        try
        {

            var db = new SwagContext();
            var dbo = estimate.Convert();
            var alreadyExists = db.Estimates.Any(i => i.Optimistic == dbo.Optimistic && i.MostLikely == dbo.MostLikely && i.Pessimistic == dbo.Pessimistic);
            if (alreadyExists)
            {
                return false;
            }
            db.Estimates.Add(dbo);
            
            return true;

        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Error in {Method}", nameof(AddEstimate));
            return false;

        }

    }
    
    public async Task<bool> UpdateEstimate(Estimate estimate)
    {

        try
        {
            
            var db = new SwagContext();
            var dbo = db.Estimates.SingleOrDefault(i => i.Optimistic == estimate.Optimistic && i.MostLikely == estimate.MostLikely && i.Pessimistic == estimate.Pessimistic);
            if (dbo is null)
            {
                return false;
            }
            dbo.Optimistic = estimate.Optimistic;
            dbo.MostLikely = estimate.MostLikely;
            dbo.Pessimistic = estimate.Pessimistic;
            return await Task.FromResult(true);

        }
        catch (Exception ex)
        {
            
            logger.LogError(ex, "Error in {Method}", nameof(UpdateEstimate));
            return false;

        }

    }
    
    public async Task<bool> DeleteEstimate(int id)
    {
        
        try
        {
            
            var db = new SwagContext();
            var dbo = db.Estimates.SingleOrDefault(i => i.Id == id);
            if (dbo is null)
            {
                return false;
            }
            
            var beforeCount = db.Estimates.Count();
            db.Estimates.Remove(dbo);
            var afterCount = db.Estimates.Count();
            return beforeCount > afterCount;

        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Error in {Method}", nameof(DeleteEstimate));
            return false;

        }

    }

}