//using TinyHouseLandshare.Models;

//namespace TinyHouseLandshare.Data
//{
//    public class UserLandListingRepository : IUserLandListingRepository
//    {
//        private readonly LandShareDbContext _context;

//        public UserLandListingRepository(LandShareDbContext context)
//        {
//            _context = context;
//        }
//        public UserLandListing Add(UserLandListing userListing)
//        {
//            _context.UserLandListings.Add(userListing);
//            _context.SaveChanges();
//            return userListing;
//        }

//        public UserLandListing Delete(UserLandListing userLandListing)
//        {
//            _context.UserLandListings.Remove(userLandListing);
//            return userLandListing;
//        }

//        public IEnumerable<UserLandListing> GetAllUserListings()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<LandListing> GetUserListings(Guid UserId)
//        {
//            var userLandListingIds = _context.UserLandListings.Where(l => l.UserId == UserId).Select(l => l.LandListingId).ToList();
//            var listings = new List<LandListing>();
//            foreach(var id in userLandListingIds)
//            {
//                listings.Add(_context.LandListings.Find(id));
//            }
//            return listings;
//        }

//        public UserLandListing Update(UserLandListing userListingUpdated)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
