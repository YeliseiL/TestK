namespace Test.Domain;

public class ExceptionLog
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string? QueryParameters { get; set; }
    public string? BodyParameters { get; set; }
    public string? StackTrace { get; set; }
}
