using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.Web.Data;
using SmartAdmin.Web.Models.Bufete;

namespace SmartAdmin.Web.Controllers
{
    public class ProcesoController : Controller
    {
        private readonly BufeteDbContext _bufeteDbContext;

        public ProcesoController(BufeteDbContext bufeteDbContext)
        {
            _bufeteDbContext = bufeteDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Proceso proceso)
        {
            await _bufeteDbContext.AddAsync(proceso);
            await _bufeteDbContext.SaveChangesAsync();
            return View();
        }
    }
}
