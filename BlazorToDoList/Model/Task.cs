namespace BlazorToDoList.Model
{
    using System.ComponentModel.DataAnnotations;
    public class Task
    {
        public int TaskId { get; set; }
        [Required]
        [MinLength(4, ErrorMessage ="At least 4")]
        public string? Description { get; set; }
        [Required]
        [Range (1,5,ErrorMessage ="Between 1 and 5")]
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public Subject? TaskSubject { get; set; }
        public bool IsCompleted{ get; set; }
    }
}
