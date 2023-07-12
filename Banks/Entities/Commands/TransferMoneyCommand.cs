namespace Banks.Entities;

public class TransferMoneyCommand : ICommand
{
    public TransferMoneyCommand(IBankAccount bankAccountFrom, IBankAccount bankAccountTo, decimal money, int id)
    {
        ArgumentNullException.ThrowIfNull(bankAccountFrom);
        ArgumentNullException.ThrowIfNull(bankAccountTo);
        BankAccountFrom = bankAccountFrom;
        BankAccountTo = bankAccountTo;
        Money = money;
        TransactionChecker = false;
        Id = id;
    }

    public IBankAccount BankAccountFrom { get; private set; }
    public IBankAccount BankAccountTo { get; private set; }
    public bool TransactionChecker { get; private set; }
    public int Id { get; private set; }
    public decimal Money { get; private set; }
    public void Execute()
    {
        BankAccountFrom.WithdrawMoney(Money);
        BankAccountTo.TopUpBalance(Money);
        TransactionChecker = true;
    }

    public void Undo()
    {
        if (!TransactionChecker)
        {
            throw new Exception();
        }

        BankAccountTo.WithdrawMoney(Money);
        BankAccountFrom.TopUpBalance(Money);
    }

    public int GetId()
    {
        return Id;
    }
}