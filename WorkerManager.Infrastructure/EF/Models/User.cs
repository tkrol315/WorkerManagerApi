﻿namespace WorkerManager.Infrastructure.EF.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public uint RoleId { get; set; }
        public Role Role { get; set; }
       
       
    }
}
