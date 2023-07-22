namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                SeeData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeeData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Models.Platform() { Name = "Item 1", Publisher = "Publisher 1", Cost = "Free" },
                    new Models.Platform() { Name = "Item 2", Publisher = "Publisher 2", Cost = "Free" },
                    new Models.Platform() { Name = "Item 3", Publisher = "Publisher 3", Cost = "Free" }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("we have data");
            }
        }
    }
}
