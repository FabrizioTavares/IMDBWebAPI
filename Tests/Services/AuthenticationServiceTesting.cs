﻿using Domain.DTOs.AuthenticationDTOs;
using Service.Services;
using Xunit;

namespace Tests.Services
{
    public class AuthenticationServiceTesting
    {

        [Fact]
        public void UserLogin()
        {
            var newLoginForm = new LoginDTO
            {
                LoginType = 0,
                Password = "12345678",
                Username = "string"
            };

            // TODO finish tests

        }
        
    }
}
