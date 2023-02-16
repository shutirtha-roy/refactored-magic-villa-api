using Autofac;
using AutoMapper;
using MagicVilla_Infrastructure.BusinessObjects;
using MagicVilla_Infrastructure.Services;

namespace MagicVilla_Web.Model
{
    public class VillaNumberModel : BaseModel
    {
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        public int VillaId { get; set; }
        public Villa Villa { get; set; }        
    }
}