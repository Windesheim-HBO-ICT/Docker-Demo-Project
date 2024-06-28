namespace loglens.Models {
    public class Message {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
