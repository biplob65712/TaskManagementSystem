using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.ViewModels
{
    public class UserLoginVM
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public int? Role { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
