namespace Tourist_Agency.Utils
{
    internal class Report
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double FullPrice { get; set; }

        public Report(Models.Trip trip, int count) { 
            Name = trip.Name;
            Count = count;
            Price = trip.Price;
            FullPrice = trip.Price * count;
        }
    }
}
