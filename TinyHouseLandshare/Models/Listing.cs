using System.ComponentModel.DataAnnotations;

namespace TinyHouseLandshare.Models
{
    public class Listing
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Details { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(3)]
        public string Country { get; set; }        
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public bool Approved { get; set; }
        public bool Submitted { get; set; }
    }
}
