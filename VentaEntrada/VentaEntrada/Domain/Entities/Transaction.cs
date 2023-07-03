using System;
using System.ComponentModel.DataAnnotations;

namespace PuertaDeEntrada.Domain.Entities
{
    public class Transaction
    {
        public string idTransaction { get; set; }
        [Key]
        public string email { get; set; }
        public double? timeSpan { get; set; }
        public int? posicion { get; set; }

    }
}
