using System.Security;
using Banks.Tools;
using VerificationException = Banks.Tools.VerificationException;

namespace Banks.Entities;

public class CreditAccount : IBankAccount
{
    public CreditAccount(Client client, decimal amountOfMoney, decimal creditInterest, decimal minOfNegativeAmountOfMoney, decimal limitOfMoneyTransaction)
    {
        ArgumentNullException.ThrowIfNull(client);
        if (amountOfMoney <= 0)
        {
            throw new NegativeAmountOfMoneyException("Negative amount of money");
        }

        if (creditInterest <= 0)
        {
            throw new CreditInterestException("Вы нас разорите...");
        }

        if (minOfNegativeAmountOfMoney >= 0)
        {
            throw new MinAmountOfMoneyException("Min amount of money is more than 0");
        }

        if (limitOfMoneyTransaction <= 0)
        {
            throw new LimitOfMoneyException("Limit of money is less than 0");
        }

        Client = client;
        AmountOfMoney = amountOfMoney;
        CredintInterest = creditInterest;
        MinOfNegativeAmountOfMoney = minOfNegativeAmountOfMoney;
        LimitOfMoneyTransaction = limitOfMoneyTransaction;
    }

    public decimal LimitOfMoneyTransaction { get; set; }
    public decimal MinOfNegativeAmountOfMoney { get; private set; }
    public decimal CredintInterest { get; private set; }
    public Client Client { get; }
    public decimal AmountOfMoney { get; private set; }
    public bool GetVerification()
    {
        return Client.VerificationPassed;
    }

    public void WithdrawMoney(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (AmountOfMoney - money < MinOfNegativeAmountOfMoney)
        {
            throw new MinAmountOfMoneyException("Not enough money");
        }

        if (money <= 0)
        {
            throw new MinAmountOfMoneyException("Money is less than 0");
        }

        if (!GetVerification())
        {
            if (money > LimitOfMoneyTransaction)
            {
                throw new VerificationException("You cant do this without verification");
            }
        }

        if (AmountOfMoney <= 0)
        {
            AmountOfMoney -= money * CredintInterest;
            return;
        }

        AmountOfMoney -= money;
    }

    public void ChangeCreditInterest(decimal newInterest)
    {
        ArgumentNullException.ThrowIfNull(newInterest);
        if (newInterest <= 0)
        {
            throw new CreditInterestException("Вы нас разорите...");
        }

        CredintInterest = newInterest;
    }

    public void ChangeLimitOfMoneyTransaction(decimal newLimit)
    {
        ArgumentNullException.ThrowIfNull(newLimit);
        if (newLimit <= 0)
        {
            throw new LimitOfMoneyException("Limit Less than 0");
        }

        LimitOfMoneyTransaction = newLimit;
    }

    public void TopUpBalance(decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        if (AmountOfMoney < 0)
        {
            AmountOfMoney += money - (money * CredintInterest);
            return;
        }

        AmountOfMoney += money;
    }

    public void TransferToAccount(IBankAccount account, decimal money)
    {
        ArgumentNullException.ThrowIfNull(money);
        ArgumentNullException.ThrowIfNull(account);
        this.WithdrawMoney(money);
        account.TopUpBalance(money);
    }
}