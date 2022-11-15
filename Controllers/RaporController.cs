using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rehber.API.Models.Context;
using Rehber.API.Models.Entities;

namespace Rehber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaporController : ControllerBase
    {
        private readonly RehberDbContext _rehberDbContext;

        public RaporController(RehberDbContext rehberDbContext)
        {
            _rehberDbContext = rehberDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var konumlar = _rehberDbContext.IletisimBilgileris.GroupBy(p => p.Konum).Select(s => s.Key).ToList();
            var result = from konum in konumlar
                         select new
                         {
                             Konum = konum,
                             KisiSayisi = _rehberDbContext.IletisimBilgileris.Where(x => x.Konum == konum).Count(),
                             TelefonSayisi = _rehberDbContext.IletisimBilgileris.Where(x => x.Konum == konum && x.TelefonNo != "").Count(),
                         };
            return Ok(result.OrderBy(p => p.KisiSayisi).ToList());
        }
    }
}
