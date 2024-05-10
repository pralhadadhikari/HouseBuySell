using Dapper;
using HouseBuySell.Infrastructure.IRepository;
using HouseBuySell.Infrastructure.Repository.CRUD;
using HouseBuySell.Models.Entity;
using HouseBuySell.Models.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Infrastructure.Repository
{
    public class PropertyTypeRepository : CrudService<PropertyType>, IPropertyTypeRepository
    {
        private readonly ICrudService<PropertyType> _propertyTypeCrudService;
        private readonly IDbConnection _dbConnection;
        public PropertyTypeRepository(ICrudService<PropertyType> propertyTypeCrudService,
            IConfiguration configuration,
             HouseLandContext context) : base(context)
        {
            _propertyTypeCrudService = propertyTypeCrudService;
            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        public async Task<IEnumerable<Property>> GetPropertiesAsync(SearchCriteria searchCriteria)
        {
            string storedProcedureName = "usp_GetProperty";
            var properties = await _dbConnection.QueryAsync<Property>(
                storedProcedureName,
                new
                {
                    propertytype = searchCriteria.propertytype,
                    location = searchCriteria.location,
                    lprice = searchCriteria.lprice,
                    hprice = searchCriteria.hprice
                },
                commandType: CommandType.StoredProcedure
            );

            return properties;
        }
    }
}
