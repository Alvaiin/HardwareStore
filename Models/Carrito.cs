using System;
using System.Collections.Generic;

namespace hardStore.Models{

    public class Carrito {
        public List<Producto> carrito {get; set;}
        public Carrito(){
            carrito = new List<Producto>();
        }
        public void agregarAlCarrito(Producto producto){
            carrito.Add(producto);
        }
    }
}