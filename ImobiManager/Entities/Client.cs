namespace ImobiManager.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public bool IsCompany { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        
    }
}
