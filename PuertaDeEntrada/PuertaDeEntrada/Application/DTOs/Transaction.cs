using System;

namespace Application.DTOs
{
    public class Transaccion
    {
        public string Correo { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public string NumeroTransaccion { get; set; }
        public TimeSpan Tiempo { get; set; }
    }
}
