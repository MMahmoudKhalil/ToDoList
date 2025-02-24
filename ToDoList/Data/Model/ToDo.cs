namespace ToDoList.Data.Model
{
    public class ToDo
    {

        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string? File { get; set; }

    }
}
