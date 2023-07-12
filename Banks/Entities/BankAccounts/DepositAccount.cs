using System.Data;
using Banks.Tools;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    public DepositAccount(Client client, decimal amountOfMoney, decimal interestOnBalace, int accountTerm, decimal limitOfMoneyTransaction)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(accountTerm);
        if (amountOfMoney <= 0)
        {
            throw new NegativeAmountOfMoneyException("Negative amount of money");
        }

        if (interestOnBalace <= 0)
        {
            throw new InterestOnBalanceException("Вы нас разорите...");
        }

        if (accountTerm <= 0)
        {
            throw new Exception();
        }

        if (limitOfMoneyTransaction <= 0)
        {
            throw new LimitOfMoneyException("Limit is less than 0");
        }

        AccountTerm = accountTerm;
        DateTimeAccountTerm = DateTime.Now.AddDays(accountTerm);
        DateTimeOfCreateAccount = DateTime.Now;
        BufferDateTime = DateTime.Now;
        Client = client;
        AmountOfMoney = amountOfMoney;
        InterestOnBalance = MakeNewInterest(interestOnBalace, amountOfMoney);
        LimitOfMoneyTransaction = limitOfMoneyTransaction;
    }

    public decimal LimitOfMoneyTransaction { get; set; }
    public int AccountTerm { get; private set; }
    public DateTime DateTimeAccountTerm { get; private set; }
    public decimal InterestOnBalance { get; private set; }
    public Client Client { get; private set; }
    public decimal AmountOfMoney { get; private set; }
    private DateTime DateTimeOfCreateAccount { get; set; }
    private DateTime BufferDateTime { get; set; }
    private decimal DepositAmountOfAccuralFromBalace { get; set; } = 0;
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

    public void WithdrawMoney(decimal money)
    {
        if (DateTimeOfCreateAccount.Date < DateTimeAccountTerm.Date)
        {
            throw new DataException("Its not time to this");
        }

        if (money <= 0)
        {
            throw new MinAmountOfMoneyException("Money is less than 0");
        }

        if (GetVerification() is false)
        {
            if (money > LimitOfMoneyTransaction)
            {
                throw new VerificationException("You cannot do this without verification");
            }
        }

        ArgumentNullException.ThrowIfNull(money);
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

    public bool GetVerification()
    {
        return Client.VerificationPassed;
    }

    public void CalculateInterestInDate(DateTime dateTime)
    {
        if (DateTimeOfCreateAccount.Date >= DateTimeAccountTerm.Date)
        {
            return;
        }

        if (AmountOfMoney == 0)
        {
            throw new MinAmountOfMoneyException("Not enough money");
        }

        decimal interestOfDay = InterestOnBalance / 365;
        int newDateTimeSpan = dateTime.Subtract(BufferDateTime).Days;
        for (int i = 0; i < newDateTimeSpan; i++)
        {
            DepositAmountOfAccuralFromBalace += interestOfDay * AmountOfMoney;
            BufferDateTime.AddDays(1);
            if (dateTime.Date.Month == BufferDateTime.Date.Month && dateTime.Date.Day == BufferDateTime.Date.Day)
            {
                CaclculateAllInterest(dateTime);
            }
        }

        BufferDateTime = dateTime;
    }

    public void TransferToAccount(IBankAccount account, decimal money)
    {
        this.WithdrawMoney(money);
        account.TopUpBalance(money);
    }

    private void CaclculateAllInterest(DateTime dateTime)
    {
        if (DepositAmountOfAccuralFromBalace <= 0)
        {
            throw new InterestOnBalanceException("Not enough money from interest");
        }

        AmountOfMoney += DepositAmountOfAccuralFromBalace;
        DepositAmountOfAccuralFromBalace = 0;
        DateTimeOfCreateAccount = dateTime;
    }

    private decimal MakeNewInterest(decimal firstPercentage, decimal amountOfMoney)
    {
        if (amountOfMoney <= 50000)
        {
            return firstPercentage;
        }

        if (amountOfMoney > 50000 && AmountOfMoney < 100000)
        {
            return firstPercentage + 0.5M;
        }

        if (amountOfMoney >= 100000)
        {
            return firstPercentage + 1M;
        }

        throw new Exception();
    }

    private void CaclculateMounthInterest()
    {
        if (DepositAmountOfAccuralFromBalace <= 0)
        {
            throw new Exception();
        }

        AmountOfMoney += DepositAmountOfAccuralFromBalace;
        DepositAmountOfAccuralFromBalace = 0;
    }
}