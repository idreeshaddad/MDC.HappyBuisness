namespace MDC.HappyBuisness.Entities
{
    public class Drug
    {
        public Drug()
        {
            Deals = new List<Deal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public double Price { get; set; }
        public string? ImageName { get; set; }
        public int Rating { get; set; }

        public int ClassificationId { get; set; }
        public Classification Classification { get; set; }

        public List<Deal> Deals { get; set; }
    }
}