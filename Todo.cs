namespace minimal_api
{
    public class Todo
    {

        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public int Id { get; set; }

        public Todo(int Id, string Title, bool IsCompleted = false) {
            this.Id = Id;
            this.Title = Title;
            this.IsCompleted = IsCompleted;
        }

        public void CompleteTodo()
        {
            this.IsCompleted = true;
        }
    }
}
