using HouseBuySell.Core.IRepository;
using HouseBuySell.Models.Entity;
using HouseBuySell.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuySell.Web.Controllers
{
    [Authorize(Policy = "IsBroker")]
    public class PropertyTypeController : Controller
    {
        private readonly ICrudService<PropertyType> _propertyCrudService;
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertyTypeController(ICrudService<PropertyType> propertyCrudService,
            IPropertyTypeRepository propertyTypeRepository,
             UserManager<ApplicationUser> userManager)
        {
            _propertyCrudService = propertyCrudService;
            _propertyTypeRepository = propertyTypeRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var proertytype=await _propertyTypeRepository.GetAllAsync();
            return View(proertytype);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            PropertyType propertyType = new PropertyType();
            if (id > 0)
            {
                propertyType= await _propertyCrudService.GetAsync(id);
            }
            return View(propertyType);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PropertyType propertyType)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (propertyType.Id == 0)
            {
                
                propertyType.CreatedBy = userId;
                propertyType.CreatedDate = DateTime.Now;
                propertyType.ModifiedDate = DateTime.Now;

                var user = _userManager.FindByIdAsync(userId).Result;

                var res = await _propertyCrudService.InsertAsync(propertyType);
            }
            else
            {
               var  orgpropertyType = await _propertyCrudService.GetAsync(propertyType.Id);

                orgpropertyType.ProprtyType = propertyType.ProprtyType;
                orgpropertyType.ModifiedBy = userId;
                orgpropertyType.ModifiedDate = DateTime.Now;

                _propertyTypeRepository.Update(orgpropertyType);
            }

            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AddEditPropertyType(int id)
        {
            PropertyType propertyType = new PropertyType();
            if (id > 0)
            {
                propertyType = await _propertyCrudService.GetAsync(id);
            }
            return Json(propertyType);
        }



    }
}
