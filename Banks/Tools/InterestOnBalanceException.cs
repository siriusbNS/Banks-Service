namespace Banks.Tools;

internal class InterestOnBalanceException : Exception
{
    public InterestOnBalanceException(string message)
        : base(message) { }
}
