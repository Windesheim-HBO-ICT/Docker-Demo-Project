namespace backend.Models {
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
