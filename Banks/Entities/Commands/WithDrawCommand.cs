namespace Banks.Entities;

public class WithDrawCommand : ICommand
{
    public WithDrawCommand(IBankAccount bankAccount, decimal money, int id)
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
        TransactionChecker = false;
        BankAcc.WithdrawMoney(Money);
    }

    public void Undo()
    {
        if (TransactionChecker is false)
            throw new Exception();
        BankAcc.TopUpBalance(Money);
    }

    public int GetId()
    {
        return Id;
    }
}