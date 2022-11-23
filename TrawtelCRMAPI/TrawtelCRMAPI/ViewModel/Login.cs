namespace TrawtelCRMAPI.ViewModel
{
    public class Login
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class LoginDTO
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public Guid AgentId { get; set; }
        public string? UserKey { get; set; }
    }
}
