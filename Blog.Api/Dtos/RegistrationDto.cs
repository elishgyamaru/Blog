namespace Blog.Api.Dtos
{
    public class RegistrationDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
    }
}