using MagicVilla_Infrastructure.BusinessObjects;

namespace MagicVilla_VillaAPI.Model
{
    public class LoginResponseModel : BaseModel
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
    }
}