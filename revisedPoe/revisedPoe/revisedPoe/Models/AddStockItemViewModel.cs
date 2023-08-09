namespace revisedPoe.Models
{
    public class AddStockItemViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Farmer { get; set; }
        public int Quantity { get; set; }
        public String StockType { get; set; }
        public DateTime entryDate { get; set; }
        public AddStockItemViewModel() { }

    }
}
