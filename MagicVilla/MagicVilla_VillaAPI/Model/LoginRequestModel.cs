namespace MagicVilla_VillaAPI.Model
{
    public class LoginRequestModel : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}