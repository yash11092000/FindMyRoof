﻿namespace PhysioWeb.Models
{
    public class Register
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;
    }
}
