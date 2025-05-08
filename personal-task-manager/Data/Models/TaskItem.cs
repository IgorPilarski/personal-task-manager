using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalTaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}