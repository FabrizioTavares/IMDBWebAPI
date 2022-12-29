using Domain.DTOs.AuthenticationDTOs;

namespace Tests.Services
{
    public class AuthenticationServiceTesting
    {

        [Fact]
        public void UserLogin()
        {
            var newLoginForm = new LoginDTO
            {
                Password = "12345678",
                Username = "string"
            };

            // TODO finish tests

        }

    }
}
