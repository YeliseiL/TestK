namespace Test.App.Exceptions;

public class SecureException : Exception
{
    public SecureException(string message) : base(message) { }
}