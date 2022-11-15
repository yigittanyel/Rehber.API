namespace Rehber.API.Models.Entities
{
    public class IletisimBilgileri
    {
        public int Id { get; set; }
        public string TelefonNo { get; set; }
        public string Mail { get; set; }
        public string Konum { get; set; }

        public int KisiId { get; set; }
    }
}
