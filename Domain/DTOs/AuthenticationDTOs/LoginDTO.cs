﻿namespace Domain.DTOs.AuthenticationDTOs
{
    public class LoginDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int LoginType { get; set; } = 0;
    }
}