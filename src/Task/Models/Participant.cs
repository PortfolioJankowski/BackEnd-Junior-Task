namespace Task.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; }
        public DateTime WorkStart { get; set; }
        public DateTime WorkEnd { get; set; }
    }
}

