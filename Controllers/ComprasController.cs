using System;
using hardStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hardStore.Controllers
{
    public class ComprasController : Controller
    {
        private IWebHostEnvironment Environment;
        private ILogger<ComprasController> _logger;
        private ProductosContext db;
        public ComprasController(ILogger<ComprasController> logger, ProductosContext contexto, IWebHostEnvironment _environment)
        {
            _logger = logger;
            this.db = contexto;
            Environment = _environment;
        }

        public IActionResult AgregarAlCarrito(Producto producto){
            return View("Index");
        }

        public IActionResult Index(){
            return View();
        }

    }
}