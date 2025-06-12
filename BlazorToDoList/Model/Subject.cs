namespace BlazorToDoList.Model
{
    public class Subject
    {
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public Subject(int subjectId, string description)
        {
            SubjectId = subjectId;
            SubjectName = description;
        }
        public Subject(int subjectId)
        {
            SubjectId = subjectId;
        }
    }
}
