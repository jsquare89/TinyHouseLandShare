using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{ 

    public interface ISeekerListingRepository
    {
        SeekerListing GetSeekerPost(Guid id);
        IEnumerable<SeekerListing> GetAllSeekerPost();
        SeekerListing Add(SeekerListing seekerPost);
        SeekerListing Update(SeekerListing seekerPostUpdated);
        SeekerListing Delete(Guid id);

    }
}
