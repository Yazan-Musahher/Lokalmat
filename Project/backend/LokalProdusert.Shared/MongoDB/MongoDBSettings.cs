namespace LokalProdusert.Shared.MongoDB
{
    public class MongoDBSettings
    {
        public string? ConnectionURI { get; set; }
        public string? DatabaseName { get; set; }
        public Dictionary<string, string> CollectionNames { get; set; } = new Dictionary<string, string>();
    }
}
