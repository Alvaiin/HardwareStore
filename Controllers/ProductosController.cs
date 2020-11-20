﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using hardStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;


namespace hardStore.Controllers
{
    public class ProductosController : Controller
    {

        private IWebHostEnvironment Environment;
        private ILogger<ProductosController> _logger;
        private ProductosContext db;
        public ProductosController(ILogger<ProductosController> logger, ProductosContext contexto, IWebHostEnvironment _environment)
        {
            _logger = logger;
            this.db = contexto;
            Environment = _environment;
        }

        public IActionResult VerProducto(long Id){
            Producto producto = db.Productos.Find(Id);
            return View(producto);
        }

        public IActionResult BuscarProducto(string buscado){
            var productos = from p in db.Productos
                 select p;

            if (!String.IsNullOrEmpty(buscado))
            {
                productos = productos.Where(p => p.Nombre.Contains(buscado));
            }
            return View("Index",productos.ToList());
        }
        public IActionResult NuevoProducto(String nombre,TipoProducto tipo, String modelo,Marca marca, double precio, IFormFile Imagen)
        {
            if (!Imagen.ContentType.Equals("image/jpeg")) //Si el archivo cargado no es JPEG, vuelvo con error
                return RedirectToAction("agregarProducto");

            Producto producto = new Producto { Nombre = nombre, Tipo = tipo, Modelo = modelo, Marca = marca, Precio = precio };
            db.Productos.Add(producto);
            db.SaveChanges();
            if(Imagen.Length > 0)
            {
                var path = Path.Combine(Environment.WebRootPath, "img","productos",producto.Id+".jpg");
                var fileStream = System.IO.File.Create(path);
                Imagen.OpenReadStream().CopyTo(fileStream);
                fileStream.Close();
            }
            return RedirectToAction("administrarProductos");
        }

        public IActionResult EliminarProducto(long Id){
            Producto producto = db.Productos.Find(Id);
            db.Productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("administrarProductos");
        }

        public IActionResult AdministrarProductos(Producto producto)
        {
            return View(db.Productos.ToList());
        }

        public IActionResult Index()
        {
            return View(db.Productos.ToList());
        }
    }
}
