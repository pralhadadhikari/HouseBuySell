using HouseBuySell.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Infrastructure.Repository
{
    public class RawSqlRepository : IRawSqlRepository
    {
        private readonly HouseLandContext _context;

        public RawSqlRepository(HouseLandContext context)
        {
            _context = context;
            _context.Database.SetCommandTimeout(1000);
        }

        public IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class
        {
            var result = _context.Set<T>().FromSqlRaw(sql, parameters);
            return result;
        }
    }
}
