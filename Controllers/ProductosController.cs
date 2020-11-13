using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hardStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace hardStore.Controllers
{
    public class ProductosController : Controller
    {
        ILogger<ProductosController> _logger;
        ProductosContext db;
        public ProductosController(ILogger<ProductosController> logger, ProductosContext contexto)
        {
            _logger = logger;
            this.db = contexto;
        }

        public IActionResult NuevoProducto(String nombre,TipoProducto tipo, String modelo,Marca marca)
        {
            Producto producto = new Producto { Nombre = nombre, Tipo = tipo, Modelo = modelo, Marca = marca };
            db.Productos.Add(producto);
            db.SaveChanges();
            return RedirectToAction("agregarProducto");
        }

        public IActionResult AgregarProducto(Producto producto)
        {
            
            return View();
        }

        public IActionResult Index()
        {
            return View(db.Productos.ToList());
        }
    }
}
