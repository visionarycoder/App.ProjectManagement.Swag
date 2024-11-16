using Microsoft.EntityFrameworkCore;

namespace Ifx.Data.Helpers;

public static class DbHelper
{

    public static Task<T> GetContextAsync<T>(DbContextOptions<T> dbContextOptions) where T : DbContext
    {
        var ctx = (T)Activator.CreateInstance(typeof(T), dbContextOptions)!;
        return Task.FromResult(ctx);
    }

}