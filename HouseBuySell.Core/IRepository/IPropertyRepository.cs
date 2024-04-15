using HouseBuySell.Models.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Core.IRepository
{
    public interface IPropertyRepository:ICrudService<Property>
    {
        Task<IEnumerable<Property>> GetAllPropertyAsync(Expression<Func<Property, bool>> expression);
        Task<Property> GetPropertyAsync(Expression<Func<Property, bool>> expression);
    }
}
