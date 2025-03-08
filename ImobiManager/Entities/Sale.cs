namespace ImobiManager.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ApartamentId { get; set; }
        public DateTime SaleDate { get; set; }

        public Client Client { get; set; }
        public Apartament Apartament { get; set; }
    }
}
