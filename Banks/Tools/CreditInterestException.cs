namespace Banks.Tools;

internal class CreditInterestException : Exception
{
    public CreditInterestException(string message)
        : base(message) { }
}