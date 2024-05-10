using HouseBuySell.Infrastructure.IRepository;
using HouseBuySell.Models.Entity;
using HouseBuySell.Models.ViewModel;
using HouseBuySell.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuySell.Web.Controllers
{
    [Authorize(Policy = "IsBroker")]
    public class PropertyController : Controller
    {
        private readonly ICrudService<Property> _propertyCrudService;
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudService<PropertyFilesInfo> _propertyFilesCrudService;

        public PropertyController(ICrudService<Property> propertyCrudService,
            IPropertyTypeRepository propertyTypeRepository,
             IPropertyRepository propertyRepository,
             UserManager<ApplicationUser> userManager,
             ICrudService<PropertyFilesInfo> propertyFilesCrudService)
        {
            _propertyCrudService = propertyCrudService;
            _propertyTypeRepository = propertyTypeRepository;
            _propertyRepository = propertyRepository;
            _userManager = userManager;
            _propertyFilesCrudService = propertyFilesCrudService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewBag.PropertyType = await _propertyTypeRepository.GetAllAsync();
            var property = await _propertyRepository.GetAllAsync(p => p.CreatedBy == userId);

            return View(property);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            PropertyViewModel propertyViewModel = new PropertyViewModel();
            ViewBag.PropertyType = await _propertyTypeRepository.GetAllAsync();


            if (id > 0)
            {
                propertyViewModel = new PropertyViewModel(await _propertyCrudService.GetAsync(id));

                IEnumerable<PropertyFilesInfo> imageFilesInfo = new List<PropertyFilesInfo>();
                imageFilesInfo = await _propertyFilesCrudService.GetAllAsync(p => p.PropertyId == id);
                propertyViewModel.PropertyImageFilesFullPath = (IList<PropertyFilesInfo>)imageFilesInfo;

            }
            return View(propertyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(PropertyViewModel propertyViewModel)
        {
            Property property = new Property();
            var userId = _userManager.GetUserId(HttpContext.User);
            property = propertyViewModel.ToProperty();
            int? propId;
            if (property.Id == 0)
            {
                if (propertyViewModel.PropertyPicture != null)
                {

                    string fileDirectory = $"wwwroot/Uploads/PropertyProfile";

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid() + "_" + propertyViewModel.PropertyPicture.FileName;
                    string filePath = Path.Combine(Path.GetFullPath($"wwwroot/Uploads/PropertyProfile"), uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        PropertyFilesInfo imageFiles = new PropertyFilesInfo();
                        await propertyViewModel.PropertyPicture.CopyToAsync(fileStream);
                        property.ImageFullPath =   $"Uploads/PropertyProfile/" + uniqueFileName;

                    }


                }

                property.CreatedBy = userId;
                property.CreatedDate = DateTime.Now;
                property.ModifiedDate = DateTime.Now;
                var res = await _propertyCrudService.InsertAsync(property);
                propId = res;
            }
            else
            {
                var orgproperty = await _propertyCrudService.GetAsync(property.Id);
                orgproperty.PropertyTypeId = property.PropertyTypeId;
                orgproperty.Location = property.Location;
                orgproperty.Price = property.Price;
                orgproperty.Features = property.Features;
                orgproperty.IsActive = property.IsActive;


                if (propertyViewModel.PropertyPicture != null)
                {

                    string fileDirectory = $"wwwroot/Uploads/PropertyProfile";

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid() + "_" + propertyViewModel.PropertyPicture.FileName;
                    string filePath = Path.Combine(Path.GetFullPath($"wwwroot/Uploads/PropertyProfile"), uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        PropertyFilesInfo imageFiles = new PropertyFilesInfo();
                        await propertyViewModel.PropertyPicture.CopyToAsync(fileStream);
                        orgproperty.ImageFullPath = $"Uploads/PropertyProfile/" + uniqueFileName;

                    }


                }

                orgproperty.ModifiedBy = userId;
                orgproperty.ModifiedDate = DateTime.Now;
                _propertyCrudService.Update(orgproperty);
                propId = orgproperty.Id;
            }

            if (propertyViewModel.AdditionalPropertyImage != null && propertyViewModel.AdditionalPropertyImage.Count() > 0)
            {
                foreach (IFormFile file in propertyViewModel.AdditionalPropertyImage)
                {
                    string fileDirectory = $"wwwroot/Uploads/PropertyImage";

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
                    string filePath = Path.Combine(Path.GetFullPath($"wwwroot/Uploads/PropertyImage"), uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        PropertyFilesInfo imageFiles = new PropertyFilesInfo();
                        await file.CopyToAsync(fileStream);
                        imageFiles.PropertyId = propId ?? 0;
                        imageFiles.Filename = uniqueFileName;
                        imageFiles.Filepath = $"Uploads/PropertyImage/{uniqueFileName}";
                        imageFiles.CreatedBy = userId;
                        imageFiles.CreatedDate = DateTime.Now;
                        await _propertyFilesCrudService.InsertAsync(imageFiles);
                    }
                }
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ImageFileDelete(int Id, long PropertyId)
        {
            PropertyFilesInfo userFiles = new PropertyFilesInfo();
            userFiles = await _propertyFilesCrudService.GetAsync(Id);
            string filedel = Path.Combine(Path.GetFullPath($"wwwroot/Uploads/PropertyImage"), userFiles.Filename);
            FileInfo fi = new FileInfo(filedel);
            if (fi != null)
            {
                System.IO.File.Delete(filedel);
                fi.Delete();
            }
            _propertyFilesCrudService.Delete(userFiles);
            return RedirectToAction("AddEdit", "Property", new { id = PropertyId });
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/api/Property/PropertyDetail")]
        public async Task<IActionResult> PropertyDetail(int propertyId)
        {
            try
            {
                // var propery = await _propertyFilesCrudService.GetAllAsync(p => p.PropertyId == propertyId);

                PropertyViewModel propertyViewModel = new PropertyViewModel();
                propertyViewModel = new PropertyViewModel(await _propertyCrudService.GetAsync(propertyId));

                var PropertyType = await _propertyTypeRepository.GetAsync(propertyViewModel.PropertyTypeId);
                propertyViewModel.PropertyType = PropertyType;
                IEnumerable<PropertyFilesInfo> imageFilesInfo = new List<PropertyFilesInfo>();
                imageFilesInfo = await _propertyFilesCrudService.GetAllAsync(p => p.PropertyId == propertyId);
                propertyViewModel.PropertyImageFilesFullPath = (IList<PropertyFilesInfo>)imageFilesInfo;
                var jsonResult = Json(new { propertyViewModel });
                return jsonResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           


            //var res = "Sucess";
           
        }

        [AllowAnonymous]
        public async Task<IActionResult> PropertyDetails(int propertyId)
        {

            PropertyViewModel propertyViewModel = new PropertyViewModel();
            propertyViewModel = new PropertyViewModel(await _propertyCrudService.GetAsync(propertyId));
            propertyViewModel.PropertyType = await _propertyTypeRepository.GetAsync(propertyViewModel.PropertyTypeId);
            IEnumerable<PropertyFilesInfo> imageFilesInfo = new List<PropertyFilesInfo>();
            imageFilesInfo = await _propertyFilesCrudService.GetAllAsync(p=>p.PropertyId== propertyId);
            propertyViewModel.PropertyImageFilesFullPath = (IList<PropertyFilesInfo>)imageFilesInfo;


            return View(propertyViewModel);
        }
    }
}
