public class Stop
{
    public required string id { get; set; }

    public required Attributes attributes { get; set; }

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

public class StopsData
{
    public List<Stop> data { get; set; }
}