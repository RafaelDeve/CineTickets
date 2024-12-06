namespace Proyecto.Domain.Entities

{
    // Entidad Usuario
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool EsAdministrador { get; set; }

        // Relaciones
        public required ICollection<Entrada> EntradasCompradas { get; set; }
    }

    // Entidad Pelicula
    public class Pelicula
    {
        public int PeliculaId { get; set; }
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public TimeSpan Duracion { get; set; }
        public required string Genero { get; set; }
        public DateTime FechaEstreno { get; set; }

        // Relaciones
        public  ICollection<Proyeccion>? Proyecciones { get; set; }
    }

    // Entidad Proyeccion
    public class Proyeccion
    {
        public int ProyeccionId { get; set; }
        public DateTime FechaHora { get; set; }
        public int Sala { get; set; }
        public int PeliculaId { get; set; }

        // Relaciones
        public required Pelicula Pelicula { get; set; }
        public required ICollection<Entrada> Entradas { get; set; }
    }

    // Entidad Entrada
    public class Entrada
    {
        public int EntradaId { get; set; }
        public int ProyeccionId { get; set; }
        public int UsuarioId { get; set; }
        public int NumeroAsiento { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCompra { get; set; }

        // Relaciones
        public required Proyeccion Proyeccion { get; set; }
        public required Usuario Usuario { get; set; }
    }

    // Entidad Pago
    public class Pago
    {
        public int PagoId { get; set; }
        public int EntradaId { get; set; }
        public decimal Monto { get; set; }
        public required string MetodoPago { get; set; } // Puede ser "Tarjeta de Crédito", "PayPal", etc.
        public DateTime FechaPago { get; set; }

        // Relaciones
        public required Entrada Entrada { get; set; }
    }
}