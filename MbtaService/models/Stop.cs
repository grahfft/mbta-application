public class Stop
{
    public required string Id { get; set; }

    public required Attributes Attributes { get; set; }

    public List<string> Routes { get; set; } = new List<string>();

    public List<Connection> Connections { get; set; } = new List<Connection>();
}

public class Attributes
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class Connection
{
    public string RouteId { get; set; }

    public string StopId { get; set; }
}