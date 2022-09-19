namespace Store
{
    public class Product
    {
        public int Id { get; }
        public string Title { get; }
        public int CategoryId { get; }

        public string Description { get; }

        public decimal Price { get; }

        public Product(int id, string title, int categoryId, string description, decimal price)
        {
            Id = id;
            Title = title;
            CategoryId = categoryId;
            Description = description;
            Price = price;
        }
    }
}
