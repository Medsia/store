namespace Store
{
    public class Product
    {
        public int Id { get; }
        public string Title { get; }
        public int CategoryId { get; }

        public Product(int id, string title, Category category)
        {
            Id = id;
            Title = title;
            CategoryId = category.Id;
        }
    }
}
