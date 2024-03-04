using Alasson.Models;

namespace Alasson.Service
{
    public abstract class BaseService
    {
        protected readonly AppDbContext _context;

        public BaseService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
