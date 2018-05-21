using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.Web.Data;
using SmartAdmin.Web.Extensions;
using SmartAdmin.Web.Models.Bufete;
using SmartAdmin.Web.Utils;

namespace SmartAdmin.Web.Controllers
{
    public class PersonaController : Controller
    {
        private readonly BufeteDbContext _db;

        public PersonaController(BufeteDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var ListaPersona = await _db.Persona.OrderBy(x=>x.Apellido).ThenBy(x=>x.Nombre).ToListAsync();
            return View(ListaPersona);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            await _db.AddAsync(persona);
            await _db.SaveChangesAsync();
            return this.Redireccionar($"{Mensaje.Informacion}|{Mensaje.Satisfactorio}");
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Persona persona)
        {
            return View();
        }
    }
}
