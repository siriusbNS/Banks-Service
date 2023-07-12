namespace Banks.Tools;

internal class VerificationException : Exception
{
    public VerificationException(string message)
        : base(message) { }
}
