using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Route("api/pais/{PaisId}/Provincia")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IEnumerable<Provincia> GetAll(int PaisId)
        {
            // el retorno es el nombre de la tabla
            return context.Provincias.Where(x => x.PaisId == PaisId).ToList();
        }

        
        [HttpGet("{id}", Name = "ProvinciaById")]
        public IActionResult GetById(int id)
        {
            var pais = context.Provincias.FirstOrDefault(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            return new ObjectResult(pais);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Provincia provincia, int idPais)
        {
            provincia.PaisId = idPais;
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Provincias.Add(provincia);
            context.SaveChanges();
            return new CreatedAtRouteResult("ProvinciaById", new { id = provincia.Id }, provincia);

        }

    }
}