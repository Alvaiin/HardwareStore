using System;
using System.Collections.Generic;
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

        public IActionResult AgregarAlCarrito(long Id){
            List<Producto> carrito = HttpContext.Session.Get<List<Producto>>("Carrito");
            if(carrito == null)
                carrito = new List<Producto>();
            
            carrito.Add(db.Productos.Find(Id));
            HttpContext.Session.Set<List<Producto>>("Carrito",carrito);

            return RedirectToAction("Index");
        }

        public IActionResult Index(){
            List<Producto> carrito = HttpContext.Session.Get<List<Producto>>("Carrito");
            if(carrito == null)
                carrito = new List<Producto>();
            return(View(carrito));
        }

        public IActionResult FinalizarCompra(){
            return View();
        }

    }
}