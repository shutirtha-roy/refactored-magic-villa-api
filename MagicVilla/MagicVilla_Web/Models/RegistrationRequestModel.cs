using MagicVilla_Web.Models;

namespace MagicVilla_Web.Models
{
    public class RegistrationRequestModel : BaseModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}