namespace TinyHouseLandshare.Models
{
    public class SeekerSearchFilter
    {
        public string Location { get; set; }
        public bool waterConnection { get; set; }
        public bool electricalConnection { get; set; }
        public bool wifiConnection { get; set; }
        public bool petsAllowed { get; set; } 
        public bool childFriendly { get; set; }
    }
}
