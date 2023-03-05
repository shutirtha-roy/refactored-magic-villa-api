using MagicVilla_Infrastructure.BusinessObjects;
using MagicVilla_Web.Models;

namespace MagicVilla_Web.Models
{
    public class LoginResponseModel : BaseModel
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
    }
}