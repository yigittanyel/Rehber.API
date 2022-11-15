using Rehber.API.Models.Entities;

namespace Rehber.API.Services.Abstract
{
    public interface IIletisimBilgileriService
    {
        IletisimBilgileri Update(int id, IletisimBilgileri iletisimBilgileri);
    }
}
