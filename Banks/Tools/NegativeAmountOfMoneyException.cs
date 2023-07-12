namespace Banks.Tools;

internal class NegativeAmountOfMoneyException : Exception
{
    public NegativeAmountOfMoneyException(string message)
        : base(message) { }
}