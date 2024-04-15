using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Models.ViewModel
{
    public class HouseLandDetail:BaseEntity
    {
        public int HouselandId { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string BrokerId { get; set; } 
    }
}
