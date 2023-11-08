namespace Cal.Models
{
    public class SharedEvents
    {
        public int Id { get; set; }
        public string UniqueIdentifier { get; set; }
        public List<Event> Events { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
