using Banks.Tools;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    public DebitAccount(Client client, decimal amountOfMoney, decimal interestOnBalace, decimal limitOfMoneyTransaction)
    {
        ArgumentNullException.ThrowIfNull(client);
        if (amountOfMoney <= 0)
        {
            throw new NegativeAmountOfMoneyException("Negative amount of money");
        }

        if (interestOnBalace <= 0)
        {
            throw new InterestOnBalanceException("Вы нас разорите...");
        }

        if (limitOfMoneyTransaction <= 0)
        {
            throw new LimitOfMoneyException("Limit is less than 0");
        }

        Client = client;
        AmountOfMoney = amountOfMoney;
        InterestOnBalance = interestOnBalace;
        DateTimeOfLastUpdate = DateTime.Now;
        BufferDateTime = DateTime.Now;
        LimitOfMoneyTransaction = limitOfMoneyTransaction;
    }

    public decimal LimitOfMoneyTransaction { get; set; }
    public decimal InterestOnBalance { get; private set; }
    public Client Client { get; }
    public decimal AmountOfMoney { get; private set; }
    private DateTime DateTimeOfLastUpdate { get; set; }
    private DateTime BufferDateTime { get; set; }
    private decimal AmountOfAccuralFromBalace { get; set; } = 0;

    public bool GetVerification()
    {
        return Client.VerificationPassed;
    }

    public void WithdrawMoney(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (Client.VerificationPassed is false)
        {
            if (money > LimitOfMoneyTransaction)
                throw new MinAmountOfMoneyException("Thats more than limit");
        }

        if (money <= 0)
        {
            throw new MinAmountOfMoneyException("Money is less than 0");
        }

        if (money > AmountOfMoney)
        {
            throw new MinAmountOfMoneyException("Not enough money");
        }

        AmountOfMoney -= money;
    }

    public void TopUpBalance(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (money <= 0)
        {
            throw new MinAmountOfMoneyException("Money is less than 0");
        }

        AmountOfMoney += money;
    }

    public void ChangeInterest(decimal newInterest)
    {
        ArgumentNullException.ThrowIfNull(newInterest);
        if (newInterest <= 0)
        {
            throw new InterestOnBalanceException("Вы нас разорите...");
        }

        InterestOnBalance = newInterest;
    }

    public void ChangeLimitOfMoneyTransaction(decimal newLimit)
    {
        ArgumentNullException.ThrowIfNull(newLimit);
        if (newLimit <= 0)
        {
            throw new LimitOfMoneyException("Limit cannot be less than 0");
        }

        LimitOfMoneyTransaction = newLimit;
    }

    public void CalculateInterestInDate(DateTime dateTime)
    {
        ArgumentNullException.ThrowIfNull(dateTime);
        if (AmountOfMoney == 0)
        {
            throw new MinAmountOfMoneyException("Not enough money");
        }

        decimal interestOfDay = InterestOnBalance / 365;
        int newDateTimeSpan = dateTime.Subtract(BufferDateTime).Days;
        for (int i = 1; i <= newDateTimeSpan; i++)
        {
            AmountOfAccuralFromBalace += interestOfDay * AmountOfMoney;
            BufferDateTime = BufferDateTime.AddDays(1);
            if (i % 30 == 0)
            {
                CaclculateMounthInterest();
            }
        }

        BufferDateTime = dateTime;
    }

    public void TransferToAccount(IBankAccount account, decimal money)
    {
        ArgumentNullException.ThrowIfNull(account);
        ArgumentNullException.ThrowIfNull(money);
        this.WithdrawMoney(money);
        account.TopUpBalance(money);
    }

    private void CaclculateMounthInterest()
    {
        if (AmountOfAccuralFromBalace < 0)
        {
            throw new MinAmountOfMoneyException("Not enough money");
        }

        AmountOfMoney += AmountOfAccuralFromBalace;
        AmountOfAccuralFromBalace = 0;
        DateTimeOfLastUpdate = DateTimeOfLastUpdate.AddMonths(1);
    }
}