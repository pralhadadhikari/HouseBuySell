using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HouseBuySell.Models.Entity
{
    public class Property:BaseEntity
    {
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
        [NotMapped]
        public IFormFile PropertyPicture { get; set; }
        [JsonIgnore]
        public PropertyType PropertyType { get; set; }
        [NotMapped]
        public string PropertyTypeName { get; set; }
        [JsonIgnore]
        public virtual ICollection<PropertyFilesInfo> PropertyFilesInfo { get; set; }
    }
}
