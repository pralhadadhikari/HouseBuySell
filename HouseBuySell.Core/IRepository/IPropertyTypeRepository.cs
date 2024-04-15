using HouseBuySell.Models.Entity;
using HouseBuySell.Models.ViewModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Core.IRepository
{
    public interface IPropertyTypeRepository:ICrudService<PropertyType>
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(SearchCriteria searchCriteria);
    }
}
