using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HouseBuySell.Models.Entity
{
    public class PropertyType:BaseEntity
    {
        public PropertyType()
        {
            this.Property = new HashSet<Property>();
        }
         
        public string ProprtyType { get; set; }        
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Property> Property { get; set; }        
    }
}
