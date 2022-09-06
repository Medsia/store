namespace Store
{
    public class Product
    {
        public int Id { get; }
        public string Title { get; }
        public int CategoryId { get; }

        public string Description { get; }

        public decimal Price { get; }

        public Product(int id, string title, Category category, string description, decimal price)
        {
            Id = id;
            Title = title;
            CategoryId = category.Id;
            Description = description;
            Price = price;
        }
    }
}
