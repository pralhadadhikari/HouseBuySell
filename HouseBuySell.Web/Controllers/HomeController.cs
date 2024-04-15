using HouseBuySell.Core.IRepository;
using HouseBuySell.Infrastructure.Repository;
using HouseBuySell.Models.Entity;
using HouseBuySell.Models.ViewModel;
using HouseBuySell.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using System.Data;
using System.Diagnostics;

namespace HouseBuySell.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICrudService<Property> _property;
        private readonly ICrudService<PropertyFilesInfo> _propertyFilesInfo;
        private readonly ICrudService<PropertyType> _propertyType;
        private readonly IRawSqlRepository _rawSqlRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            ICrudService<Property> property,
            ICrudService<PropertyFilesInfo> propertyFilesInfo,
            ICrudService<PropertyType> propertyType,
            IRawSqlRepository rawSqlRepository,
            IPropertyTypeRepository propertyTypeRepository,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _property = property;
            _propertyFilesInfo = propertyFilesInfo;
            _propertyType = propertyType;
            _rawSqlRepository = rawSqlRepository;
            _propertyTypeRepository = propertyTypeRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(SearchCriteria searchCriteria)
        {
            PropertyViewModel propertyViewModel = new PropertyViewModel();
            propertyViewModel.PropertyTypes = await _propertyType.GetAllAsync(p => p.IsActive == true);
            
            try
            {


               //string sqlQuery = "SELECT * FROM Property WHERE isactive = 1";

               

                //propertyViewModel.Properties = await _rawSqlRepository
                //    .FromSql<Property>(sqlQuery)
                //    .Include(p => p.PropertyType)
                //    .ToListAsync();

                var properties = await _propertyTypeRepository.GetPropertiesAsync(searchCriteria);

                propertyViewModel.Properties = properties;



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(propertyViewModel);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("/api/Home/PropertyDetail")]
        public async Task<IActionResult> PropertyDetail(int propertyId)
        {
            try
            {
                PropertyViewModel propertyViewModel = new PropertyViewModel();
                propertyViewModel = new PropertyViewModel(await _property.GetAsync(propertyId));

                var PropertyType = await _propertyTypeRepository.GetAsync(propertyViewModel.PropertyTypeId);
                propertyViewModel.PropertyType = PropertyType;
                IEnumerable<PropertyFilesInfo> imageFilesInfo = new List<PropertyFilesInfo>();
                imageFilesInfo = await _propertyFilesInfo.GetAllAsync(p => p.PropertyId == propertyId);
                propertyViewModel.PropertyImageFilesFullPath = (IList<PropertyFilesInfo>)imageFilesInfo;

                // var userInfo = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByIdAsync(propertyViewModel.CreatedBy);

                var jsonResult = Json(new { propertyViewModel, user });
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }



           

        }

        public async Task<IActionResult> Search(SearchCriteria searchCriteria)
        {
            PropertyViewModel propertyViewModel = new PropertyViewModel();
            propertyViewModel.PropertyTypes = await _propertyType.GetAllAsync(p => p.IsActive == true);

            try
            {



                //var Properties = await _rawSqlRepository
                //    .FromSql<Property>("SELECT * FROM Property WHERE isactive = 1")
                //    .Include(p => p.PropertyType)
                //    .ToListAsync();

                //if (searchCriteria.propertytype.HasValue)
                //{
                //    Properties = Properties.Where(p => p.PropertyTypeId == searchCriteria.propertytype.Value).ToList();
                //}
                //if (searchCriteria.propertytype.HasValue)
                //{
                //    Properties = Properties.Where(p => p.PropertyTypeId == searchCriteria.propertytype.Value).ToList();
                //}
                //if (!string.IsNullOrEmpty(searchCriteria.location))
                //{
                //    Properties = Properties.Where(p => p.Location.Contains(searchCriteria.location)).ToList();
                //}

                //if (searchCriteria.lprice.HasValue && searchCriteria.hprice.HasValue)
                //{
                //    Properties = Properties.Where(p => p.Price >= searchCriteria.lprice && p.Price <= searchCriteria.hprice).ToList();
                //}
                //else if (searchCriteria.lprice.HasValue)
                //{
                //    Properties = Properties.Where(p => p.Price >= searchCriteria.lprice).ToList();
                //}
                //else if (searchCriteria.hprice.HasValue)
                //{
                //    Properties = Properties.Where(p => p.Price <= searchCriteria.hprice).ToList();
                //}




                var properties = await _propertyTypeRepository.GetPropertiesAsync(searchCriteria);

                propertyViewModel.Properties = properties;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(nameof(Index), propertyViewModel);
        }

        public IActionResult ImageDetail(string imagePath)
        {
            // Pass the imagePath to the view to display the full-size image
            ViewData["ImagePath"] = imagePath;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}