using System.Linq;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class UserListingRepository : IUserListingRepository
    {
        private readonly LandShareDbContext _context;

        public UserListingRepository(LandShareDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserListing> GetAllUserListings()
        {

            var listings = _context.UserListings;

            //  TODO: Code to get listings based on older UserListings class. To remove once new class working well
            //var listings = _context.UserSeekerListings.
            //    Select(seekerListings => new UserListing
            //{
            //    UserId = seekerListings.UserId,
            //    ListingId = seekerListings.SeekerListingId
            //}).Union(_context.UserLandListings.
            //    Select(landListings => new UserListing
            //    {
            //        UserId = landListings.UserId,
            //        ListingId = landListings.LandListingId
            //    }));
            return listings;
        }

        public IQueryable<UserListing> GetUserListings(Guid UserId)
        {
            var listings = GetAllUserListings().
                Where(listing => listing.UserId.Equals(UserId));
            return listings;
        }

        public UserListing Add(UserListing userListing)
        {
            _context.UserListings.Add(userListing);
            _context.SaveChanges();
            return userListing;
        }

        public bool Delete(Guid id)
        {
            
            var userListing = _context.UserListings.Find(id);
            if(userListing is not null)
            {
                _context.UserListings.Remove(userListing);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public Guid GetUserIdByListing(Guid listingId)
        {
            var userId = GetAllUserListings().
                Where(listing => listing.Id.Equals(listingId)).
                Select(listing => listing.UserId).FirstOrDefault();
            return userId;
        }

        public IEnumerable<LandListing> GetUserLandListings(Guid userId)
        {
            var userLandListingIds = _context.UserListings.
                Where(l => l.UserId.Equals(userId) && l.LandListingId != null ).
                Select(l => l.LandListingId).ToList();
            var listings = new List<LandListing>();
            foreach (var id in userLandListingIds)
            {
                listings.Add(_context.LandListings.Find(id));
            }
            return listings;
        }

        public SeekerListing GetUserSeekerListing(Guid userId)
        {
            var listingId = _context.UserListings.
                Where(l => l.UserId.Equals(userId)).
                Select(l => l.SeekerListingId).FirstOrDefault();
            var seekerListing = _context.SeekerListings.
                Where(l => l.Id == listingId).FirstOrDefault();
            return seekerListing;
        }

        public Guid GetListingIdBySeekerOrLandListing(Guid id)
        {
            var listingId = _context.UserListings.
                Where(l => l.SeekerListingId.Equals(id) || l.LandListingId.Equals(id)).
                Select(l => l.Id).FirstOrDefault();
            return listingId;
        }

        public string GetListingTitleBySeekerOrLandListingId(Guid id)
        {
            var title = (GetSeekerListingTitleById(id)
                        ).Union(GetLandListingTitleById(id)).FirstOrDefault();

            return title;
        }

        private IQueryable<string> GetLandListingTitleById(Guid id)
        {
            return from ll in _context.LandListings
                   where ll.Id == id
                   select ll.Title;
        }

        private IQueryable<string> GetSeekerListingTitleById(Guid id)
        {
            return from sl in _context.SeekerListings
                   where sl.Id == id
                   select sl.Title;
        }

        public UserListing? GetUserListingBySeekerOrLandListingId(Guid Id)
        {
            return _context.UserListings.Where(ul => ul.SeekerListingId.Equals(Id) || ul.LandListingId.Equals(Id)).FirstOrDefault();
        }
    }
}
