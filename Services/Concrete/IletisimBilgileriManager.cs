using Rehber.API.Models.Context;
using Rehber.API.Models.Entities;
using Rehber.API.Services.Abstract;

namespace Rehber.API.Services.Concrete
{
    public class IletisimBilgileriManager : IIletisimBilgileriService
    {
        private readonly RehberDbContext _rehberDbContext;

        public IletisimBilgileriManager(RehberDbContext rehberDbContext)
        {
            _rehberDbContext = rehberDbContext;
        }

        public IletisimBilgileri Update(int id, IletisimBilgileri iletisimBilgileri)
        {
            var value = _rehberDbContext.IletisimBilgileris.Where(x => x.Id == id).FirstOrDefault();
            value.TelefonNo = iletisimBilgileri.TelefonNo;
            value.Konum = iletisimBilgileri.Konum;
            value.KisiId = iletisimBilgileri.KisiId;

            _rehberDbContext.SaveChanges();
            return value;

        }
    }
}
