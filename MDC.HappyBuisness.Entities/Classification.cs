namespace MDC.HappyBuisness.Entities
{
    public class Classification
    {
        public Classification()
        {
            Drugs = new List<Drug>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<Drug> Drugs { get; set; }
    }
}
