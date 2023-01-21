public class Entity
{
    public string Entity_Id { get; set; }

    public Dictionary<string, object> Attributes { get; set; } = new();
}