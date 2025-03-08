using ImobiManager.Enums;

namespace ImobiManager.DTO
{
    public class ApartamentDto
    {
        public int Number { get; set; }
        public string BlockOrTower { get; set; }
        public int Floor { get; set; }
        public double Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int GarageSpaces { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public ApartmentStatus Status { get; set; }
        public string Description { get; set; }
    }
}
