namespace Banks.Entities;

public interface IBankAccount
{
    Client Client { get; }
    decimal AmountOfMoney { get; }
    void WithdrawMoney(decimal money);
    void TopUpBalance(decimal money);
    void ChangeLimitOfMoneyTransaction(decimal newLimit);
}