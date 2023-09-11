﻿namespace TaskManagementSystem.Models.ViewModels
{
    public class DashboardVM
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }

        public ICollection<User> UserList { get; set; }
    }
}
