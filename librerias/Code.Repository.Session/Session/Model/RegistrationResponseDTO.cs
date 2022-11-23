

namespace Code.Repository.Session.Model
{
    public class RegistrationResponseDTO 
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public UserSessionDTO usuario { get; set; }
    }
}
