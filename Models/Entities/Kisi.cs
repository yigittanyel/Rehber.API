namespace Rehber.API.Models.Entities
{
    public class Kisi
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public ICollection<IletisimBilgileri> IletisimBilgileris { get; set; }
    }
}
