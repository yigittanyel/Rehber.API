using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehber.API.Dto;
using Rehber.API.Models.Context;
using Rehber.API.Models.Entities;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;

namespace Rehber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KisiKontroller : ControllerBase
    {


        private readonly RehberDbContext _rehberDbContext;
        private readonly IMapper _mapper;

        public KisiKontroller(RehberDbContext rehberDbContext, IMapper mapper)
        {
            _rehberDbContext = rehberDbContext;
            _mapper = mapper;
        }

        #region Redis Implementation

        T GetCache<T>(string key)
        {
            var redisclient = new RedisClient("localhost", 6379);
            //IRedisTypedClient<List<Kisi>> kisiler = redisclient.As<List<Kisi>>();
            IRedisTypedClient<T> kisiler = redisclient.As<T>();

             return redisclient.Get<T>(key);
        }

        void SetCache<T>(string key, T data)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<T> kisiler = redisclient.As<T>();

            redisclient.Set<T>(key, data);
        }

        void RemoveCache<T>(string key)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<T> kisiler = redisclient.As<T>();

            redisclient.Remove(key);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            List<Kisi> kisiListesi = GetCache<List<Kisi>>("kisiler");

            if (kisiListesi == null)
            {
                kisiListesi = await _rehberDbContext.Kisis.Include(x => x.IletisimBilgileris).ToListAsync();
                SetCache<List<Kisi>>("kisiler", kisiListesi);
                //redisclient.Set<List<Kisi>>("kisiler", kisiListesi);
            }
            
            return Ok(kisiListesi);
        }



        [HttpPost]
        public async Task<IActionResult> Post(KisiDto kisiDto)
        {
            Kisi kisi=_mapper.Map<Kisi>(kisiDto);

            await _rehberDbContext.Kisis.AddAsync(kisi);
            await _rehberDbContext.SaveChangesAsync();

            RemoveCache<List<Kisi>>("kisiler");

            return Ok("Kayıt işlemi başarılı.");
        }

        [HttpPut("[action]/{id}")] 
        public async Task<IActionResult> Put(int id,KisiDto kisiDto)
        {
            Kisi kisi = await _rehberDbContext.Kisis.FindAsync(id);

            kisi.Firma = kisiDto.Firma;
            kisi.Soyad = kisiDto.Soyad;
            kisi.Ad = kisiDto.Ad;

            await _rehberDbContext.SaveChangesAsync();

            RemoveCache<List<Kisi>>("kisiler");

            return Ok("Güncelleme işlemi başarılı.");
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Kisi kisi = await _rehberDbContext.Kisis.FindAsync(id);

             _rehberDbContext.Remove(kisi);
            await _rehberDbContext.SaveChangesAsync();

            RemoveCache<List<Kisi>>("kisiler");

            return Ok("Silme işlemi başarılı.");
        }



    }
}
