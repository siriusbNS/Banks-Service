namespace Banks.Tools;

internal class LimitOfMoneyException : Exception
{
 public LimitOfMoneyException(string message)
  : base(message) { }
}