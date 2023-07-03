namespace PuertaDeEntrada.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool isFree { get; set; }

    }
}
