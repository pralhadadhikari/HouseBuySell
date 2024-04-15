using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Models.ViewModel
{
    public class SearchCriteria
    {
        [Display(Name ="Location")]
        public string location { get; set; }
        [Display(Name = "Lower Price")]
        public float? lprice { get; set; }
        [Display(Name = "Higher Price")]
        public float? hprice { get; set; }
        [Display(Name = "Property Type")]
        public int? propertytype { get; set; }
    }
}
