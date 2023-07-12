namespace Banks.Entities;

public class UpToBalanceCommand : ICommand
{
    public UpToBalanceCommand(IBankAccount bankAccount, decimal money, int id)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);
        BankAcc = bankAccount;
        Money = money;
        TransactionChecker = false;
        Id = id;
    }

    public bool TransactionChecker { get; private set; }
    public int Id { get; private set; }
    public IBankAccount BankAcc { get; private set; }
    public decimal Money { get; private set; }
    public void Execute()
    {
        TransactionChecker = true;
        BankAcc.TopUpBalance(Money);
    }

    public void Undo()
    {
        if (TransactionChecker is false)
            throw new Exception();
        BankAcc.WithdrawMoney(Money);
    }

    public int GetId()
    {
        return Id;
    }
}