using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HouseBuySell.Models.Entity
{
    public class PropertyFilesInfo:BaseEntity
    {
        public int PropertyId { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public virtual Property Property { get; set; }

        

    }
}
