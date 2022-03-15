namespace TinyHouseLandshare.Models
{
    public interface ISeekerPostRepository
    {
        SeekerPost GetSeekerPost(int id);
        IEnumerable<SeekerPost> GetAllSeekerPost();
        SeekerPost Add(SeekerPost seekerPost);
        SeekerPost Update(SeekerPost seekerPostUpdated);
        SeekerPost Delete(int id);

    }
}
