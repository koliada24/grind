using Microsoft.EntityFrameworkCore;

namespace CashingWithPaginationDemo.Src
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db, int total = 1_000_000, int batchSize = 1000)
        {
            if (await db.EntityModels.AnyAsync())
                return;

            var batch = new List<EntityModel>(batchSize);

            for (int i = 0; i < total; i++)
            {
                batch.Add(EntityModel.Create());

                if (batch.Count >= batchSize)
                {
                    db.ChangeTracker.AutoDetectChangesEnabled = false;
                    db.AddRange(batch);
                    await db.SaveChangesAsync();
                    db.ChangeTracker.AutoDetectChangesEnabled = true;
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;
                db.AddRange(batch);
                await db.SaveChangesAsync();
                db.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }
    }
}
