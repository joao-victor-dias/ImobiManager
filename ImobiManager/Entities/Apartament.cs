using ImobiManager.Enums;

namespace ImobiManager.Entities
{
    public class Apartament
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string BlockOrTower { get; set; }        
        public int Floor { get; set; }
        public double Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int GarageSpaces { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public ApartmentStatus Status { get; set; }
        public string Description { get; set; }

        public Reservation? Reservation { get; set; }
    }
}
