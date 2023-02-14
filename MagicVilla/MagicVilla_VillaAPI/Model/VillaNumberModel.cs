using MagicVilla_Infrastructure.BusinessObjects;

namespace MagicVilla_VillaAPI.Model
{
    public class VillaNumberModel : BaseModel
    {
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        public int VillaId { get; set; }
        public Villa Villa { get; set; }
    }
}