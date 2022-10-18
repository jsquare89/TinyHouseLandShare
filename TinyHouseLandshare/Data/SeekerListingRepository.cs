using Microsoft.EntityFrameworkCore;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class SeekerListingRepository : ISeekerListingRepository
    {
        private readonly LandShareDbContext _context;

        public SeekerListingRepository(LandShareDbContext context)
        {
            _context = context;
        }
        public SeekerListing Add(SeekerListing seekerListing)
        {
            _context.SeekerListings.Add(seekerListing);
            _context.SaveChanges();
            return seekerListing;
        }

        public SeekerListing? Delete(Guid id)
        {

            SeekerListing? seekerListing = _context.SeekerListings.Find(id);

            if(seekerListing is not null)
            {
                _context.SeekerListings.Remove(seekerListing);
                _context.SaveChanges();
                
            }
            return seekerListing;
        }

        public SeekerListing GetSeekerListing(Guid id)
        {
            return _context.SeekerListings.Include("UserListing").Where(x => x.Id == id).FirstOrDefault();// Find(id);
        }

        public IEnumerable<SeekerListing> GetAllSeekerListings()
        {
            return _context.SeekerListings;
        }

        public IEnumerable<SeekerListing> GetAllApprovedSeekerListings()
        {
            return _context.SeekerListings.Include("UserListing").Where(seekerListing => seekerListing.Approved.Equals(true));
        }

        public SeekerListing Update(SeekerListing seekerPostUpdated)
        {
            var seekerListing = _context.SeekerListings.Attach(seekerPostUpdated);
            seekerListing.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return seekerPostUpdated;
        }

        public IEnumerable<SeekerListing> GetAllUnapprovedSubmittedSeekerListings()
        {
            return _context.SeekerListings.Include("UserListing").Where(seekerListing => seekerListing.Approved.Equals(false) && seekerListing.Submitted.Equals(true));
        }

        public IEnumerable<SeekerListing> Search(SeekerSearchFilter seekerSearch)
        {
            if(seekerSearch.Location is "Any")
            {
                return GetAllApprovedSeekerListings().Where(seekerListing =>
                seekerListing.WaterConnectionRequired.Equals(seekerSearch.waterConnection) &&
                seekerListing.ElectricalConnectionRequired.Equals(seekerSearch.electricalConnection) &&
                seekerListing.InternetConnectionRequired.Equals(seekerSearch.wifiConnection) &&
                seekerListing.PetFriendlyRequired.Equals(seekerSearch.petsAllowed) &&
                seekerListing.ChildFriendlyRequired.Equals(seekerSearch.childFriendly));
            }
            return GetAllApprovedSeekerListings().Where(seekerListing =>
                seekerListing.Location.Equals(seekerSearch.Location) &&
                seekerListing.WaterConnectionRequired.Equals(seekerSearch.waterConnection) &&
                seekerListing.ElectricalConnectionRequired.Equals(seekerSearch.electricalConnection) &&
                seekerListing.InternetConnectionRequired.Equals(seekerSearch.wifiConnection) &&
                seekerListing.PetFriendlyRequired.Equals(seekerSearch.petsAllowed) &&
                seekerListing.ChildFriendlyRequired.Equals(seekerSearch.childFriendly));
        }
    }
}
