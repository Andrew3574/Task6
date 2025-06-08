using Microsoft.EntityFrameworkCore;
using Repositories.Data;

namespace EditorAPI.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using Task6DbContext dbContext = scope.ServiceProvider.GetRequiredService<Task6DbContext>();
            dbContext.Database.Migrate();
        }
    }
}
