﻿namespace LibraryWebAPI.Models.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte UserRole { get; set; } 
    }
}
