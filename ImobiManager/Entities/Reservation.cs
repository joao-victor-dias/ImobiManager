namespace ImobiManager.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ApartamentId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;

        public Client Client { get; set; }
        public Apartament Apartament { get; set; }
    }
}
