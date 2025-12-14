namespace Products_service.DTOS
{
    public class UserDTO
    {
        public string NomUser { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MotPasse { get; set; } = null!;
        public int Role { get; set; }
    }
}
