using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
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

        public IActionResult EliminarDelCarrito(int index){
            List<Producto> carrito = HttpContext.Session.Get<List<Producto>>("Carrito");
            if(carrito == null)
                carrito = new List<Producto>();
            
            carrito.RemoveAt(index);
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
            List<Producto> carrito = HttpContext.Session.Get<List<Producto>>("Carrito");
            if(carrito == null || carrito.Count == 0)
                return RedirectToAction("Index");

            return View();
        }

        public IActionResult EnviarDatosPago(String nombre, String apellido, String email, String direccion, String telefono){
            List<Producto> carrito = HttpContext.Session.Get<List<Producto>>("Carrito");
            if(carrito == null || carrito.Count == 0)
                return RedirectToAction("Index");
                
            string mailNuestro = "hardstoreweb@gmail.com";
            string pass = "hardstoreweb132";

            MailMessage mailCliente = new MailMessage(mailNuestro,email,"Confirmacion de compra",nombre+
                                    ", Gracias por elegirnos, en breve nos pondremos en contacto con ud. para finalizar su compra");
            StringBuilder cuerpo =new StringBuilder("\nNombre: "+nombre+
                            "\nApellido: "+apellido+
                            "\nEmail: "+email+
                            "\nDireccion: "+direccion+
                            "\nTelefono: "+telefono+
                            "\nCompra: ");

            foreach(Producto p in carrito){
                cuerpo.Append("\n"+p.ToString());
            }
            MailMessage mailDatos = new MailMessage(mailNuestro,mailNuestro,"Datos de "+nombre+" "+apellido, cuerpo.ToString());
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailNuestro, pass);
            SmtpServer.EnableSsl = true;    

            SmtpServer.Send(mailCliente);
            SmtpServer.Send(mailDatos);

            HttpContext.Session.Remove("Carrito");
            return RedirectToAction("Index");
        }

    }
}