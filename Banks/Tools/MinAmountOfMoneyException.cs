namespace Banks.Tools;

public class MinAmountOfMoneyException : Exception
{
    public MinAmountOfMoneyException(string message)
        : base(message) { }
}