namespace MagicVilla_Infrastructure.BusinessObjects
{
    public class VillaNumber
    {
        public int Id { get; set; }
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int VillaId { get; set; }
        public Villa Villa { get; set; }
    }
}