using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehber.API.Dto;
using Rehber.API.Models.Context;
using Rehber.API.Models.Entities;
using Rehber.API.Services.Abstract;

namespace Rehber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IletisimBilgileriController : ControllerBase
    {
        private readonly RehberDbContext _rehberDbContext;
        private readonly IIletisimBilgileriService _iletisimBilgileriService;
        public IletisimBilgileriController(RehberDbContext rehberDbContext, IIletisimBilgileriService iletisimBilgileriService)
        {
            _rehberDbContext = rehberDbContext;
            _iletisimBilgileriService = iletisimBilgileriService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IletisimBilgileri iletisimBilgileri)
        {
            await _rehberDbContext.IletisimBilgileris.AddAsync(iletisimBilgileri);
            await _rehberDbContext.SaveChangesAsync();

            Kisi kisi = await _rehberDbContext.Kisis.Include(p => p.IletisimBilgileris).FirstOrDefaultAsync(p => p.Id == iletisimBilgileri.KisiId);

            return Ok(kisi);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            IletisimBilgileri iletisimBilgileri = await _rehberDbContext.IletisimBilgileris.FindAsync(id);
            _rehberDbContext.Remove(iletisimBilgileri);
            await _rehberDbContext.SaveChangesAsync();

            Kisi kisi = await _rehberDbContext.Kisis.Include(p => p.IletisimBilgileris).FirstOrDefaultAsync(p => p.Id == iletisimBilgileri.KisiId);

            return Ok(kisi);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Put(int id, IletisimBilgileri iletisimBilgileri)
        {
            _iletisimBilgileriService.Update(id, iletisimBilgileri);
            await _rehberDbContext.SaveChangesAsync();

            return Ok("Güncelleme işlemi başarılı.");
        }
    }
}
