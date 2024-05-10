using HouseBuySell.Infrastructure.IRepository;
using HouseBuySell.Infrastructure.Repository.CRUD;
using HouseBuySell.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HouseBuySell.Infrastructure.Repository
{
    public class PropertyRepository: CrudService<Property>, IPropertyRepository
    {
        private readonly ICrudService<Property> _propertyCrudService;

        public PropertyRepository(ICrudService<Property> propertyCrudService,
            HouseLandContext context) : base(context)
        {
            _propertyCrudService = propertyCrudService;
        }

        public async Task<IEnumerable<Property>> GetAllPropertyAsync(Expression<Func<Property, bool>> expression)
        {
            return await _context.Set<Property>()
                .AsNoTracking()
               .Include(p => p.PropertyType)
                .Where(expression)
                .ToListAsync();
        }

        public async Task<Property> GetPropertyAsync(Expression<Func<Property, bool>> expression)
        {
            return await _context.Set<Property>()
                .Include(p => p.PropertyType)
                .Where(expression)
                .SingleOrDefaultAsync();
        }

    }
}
