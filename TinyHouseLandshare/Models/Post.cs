namespace TinyHouseLandshare.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public IEnumerable<string> PictureUris { get; set; }

    }
}
