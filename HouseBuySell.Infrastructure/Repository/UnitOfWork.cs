using HouseBuySell.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Infrastructure.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly HouseLandContext _context;

        public UnitOfWork(HouseLandContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

    }
}
