using HouseBuySell.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Models.ViewModel
{
    public class PropertyViewModel:BaseEntity
    {
        public PropertyViewModel()
        {

        }
        public PropertyViewModel(Property property)
        {
            if(property != null)
            {
                Id = property.Id;
                PropertyTypeId = property.PropertyTypeId;
                Location = property.Location;
                Price = property.Price;
                Features = property.Features;
                ImageFullPath = property.ImageFullPath;
                IsActive = property.IsActive;
                CreatedBy = property.CreatedBy;
            }
        }
        public int PropertyTypeId { get; set; }
        public string Location { get; set; }
        public float Price { get; set; }
        public string Features { get; set; }
        public string ImageFullPath { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public IFormFile PropertyPicture { get; set; }
        public PropertyType PropertyType { get; set; }
        public virtual ICollection<PropertyFilesInfo> PropertyFilesInfo { get; set; }

        public List<IFormFile> AdditionalPropertyImage { get; set; }
        public IList<PropertyFilesInfo> PropertyImageFilesFullPath { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public SearchCriteria searchCriteria { get; set; }

        public Property ToProperty(Property property = null)
        {
            if (property == null)
                property = new Property();
            property.Id = Id;
            property.PropertyTypeId = PropertyTypeId;
            property.Location = Location;
            property.Price = Price;
            property.Features = Features;
            property.ImageFullPath = ImageFullPath;
            property.IsActive = IsActive;
            property.CreatedBy = CreatedBy;
            return property;
        }
    }
}
