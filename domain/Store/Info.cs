
namespace Store
{
    public class Info
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Info(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
