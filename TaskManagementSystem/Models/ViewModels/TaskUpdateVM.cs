namespace TaskManagementSystem.Models.ViewModels
{
    public class TaskUpdateVM
    {
        //
        public int ID { get; set; }                    
        public string? TaskTile { get; set; }        
        public string TaskDesk { get; set; }
        //
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? Status { get; set; }
        //
        public int? UserID { get; set; }
        //
        public User? User { get; set; }

        // public ICollection<User> Users { get; set; }
    }
}
