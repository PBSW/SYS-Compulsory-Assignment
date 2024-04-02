namespace Shared.Messages;

public class DoubleIdMessage : IInfrastructureMessage
{
    public int Id1 { get; set; }
    public int Id2 { get; set; }
    public Dictionary<string, string> Headers { get; set; }
}