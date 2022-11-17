﻿using API.Entities.Enums;

namespace API.Entities.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Slug { get; protected set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
        public DateTime Included { get; protected set; }
    }
}
