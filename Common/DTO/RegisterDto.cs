namespace Library.Common.DTO
{
    public class RegisterDto
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
