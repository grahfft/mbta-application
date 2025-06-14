public class Stop
{
    public required string Id { get; set; }

    public required Attributes attributes { get; set; }

    public List<string> Routes
    { get; set; } = new List<string>();
}

public class Attributes
{
    public double latitude { get; set; }
    public double longitude { get; set; }
}