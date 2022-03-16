namespace TinyHouseLandshare.Models
{
    public class Listing
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public string PictureUri { get; set; }
        public bool Approved { get; set; }
        public string Status { get; set; }
        public bool Submitted { get; set; }

    }
}
