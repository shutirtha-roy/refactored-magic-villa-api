using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models
{
    public class VillaNumberCreateVM : BaseModel
    {
        public VillaNumberCreateModel VillaNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }

        public VillaNumberCreateVM()
        {
            
        }
    }
}