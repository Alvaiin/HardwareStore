using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace hardStore.Models{

    public class Producto{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public String Nombre { get; set; }        
        [Required]
        public TipoProducto Tipo { get; set; }
        [Required]
        public String Modelo { get; set; }

        public Marca Marca { get; set; }

        public double Precio { get; set; }

    }
}