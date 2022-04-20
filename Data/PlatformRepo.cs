using PlatformService.Models;
using Throw;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(Platform model)
        {
            model.ThrowIfNull(x => throw new ArgumentNullException(nameof(x)));
            _context.Platforms!.Add(model);
            SaveChanges();
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms?.ToList()!;
        }

        public Platform GetById(int id)
        {
            return _context.Platforms?.FirstOrDefault(x => x.Id == id )!;
        
        }
        public bool SaveChanges()
        {
           return _context.SaveChanges() >=0;
        }
    }
}