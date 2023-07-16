using Tourist_Agency.Models;

namespace Tourist_Agency.Utils
{
    internal class TripUser
    {
        public bool IsBought { get; set; }
        public bool IsReserved { get; set; }
        public Trip Trip { get; set; }
        public TripUser(Trip trip, bool isBought, bool isReserved)
        {
            Trip = trip;
            IsBought = isBought;
            IsReserved = isReserved;
        }
    }
}
