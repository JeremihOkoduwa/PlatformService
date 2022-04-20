using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var createScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = createScope.ServiceProvider.GetRequiredService<AppDbContext>();
            SeedData(dbContext);
            }
        }

        static void SeedData(AppDbContext context)
        {
            if (!context.Platforms!.Any())
            {
                Console.WriteLine("-----seeding data----");
                context.AddRange(
                    new Platform
                    {
                        Name = "Dot net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform
                    {
                        Name = "AWS",
                        Publisher = "Amazon",
                        Cost = "Paid"
                    },
                    new Platform
                    {
                        Name = "Twitter",
                        Publisher = "Twitter Inc",
                        Cost = "Free"
                    },
                    new Platform
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Foundation",
                        Cost = "Free"
                    }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("seeding Data exist");
            }
        }
    }
}