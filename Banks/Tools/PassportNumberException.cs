namespace Banks.Tools;

internal class PassportNumberException : Exception
{
    public PassportNumberException(string message)
        : base(message) { }
}
