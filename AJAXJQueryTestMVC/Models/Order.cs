namespace AJAXJQueryTestMVC.Models {
    public class Order {
        public string Id { get; set; } 
        public int Quantity { get; set; }
        public DateTime OrderAt { get; set; }=DateTime.Now;
    }
}
