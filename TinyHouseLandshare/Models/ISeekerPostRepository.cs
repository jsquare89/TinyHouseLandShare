namespace TinyHouseLandshare.Models
{
    public interface ISeekerPostRepository
    {
        SeekerPost GetSeekerPost(Guid id);
        IEnumerable<SeekerPost> GetAllSeekerPost();
        SeekerPost Add(SeekerPost seekerPost);
        SeekerPost Update(SeekerPost seekerPostUpdated);
        SeekerPost Delete(Guid id);

    }
}
