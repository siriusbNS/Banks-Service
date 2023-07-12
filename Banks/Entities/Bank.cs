using Banks.Tools;

namespace Banks.Entities;

public class Bank : IObservable
{
    public Bank(decimal creditInterest, decimal interestOnBalance, decimal minOfNegativeAmountOfMoney, decimal limitOfMoneyTransaction, string nameOfBank)
    {
        ArgumentNullException.ThrowIfNull(creditInterest);
        ArgumentNullException.ThrowIfNull(interestOnBalance);
        if (creditInterest <= 0)
        {
            throw new CreditInterestException("Вы нас разорите...");
        }

        if (interestOnBalance <= 0)
        {
            throw new InterestOnBalanceException("Вы их разорите...");
        }

        if (minOfNegativeAmountOfMoney >= 0)
        {
            throw new MinAmountOfMoneyException("Money is less than 0");
        }

        if (limitOfMoneyTransaction <= 0)
        {
            throw new LimitOfMoneyException("Limit is less than 0");
        }

        ListOfBankAccounts = new List<IBankAccount>();
        ListOfObservers = new List<IObserver>();
        ListOfClients = new List<Client>();
        CreditInterest = creditInterest;
        InterestOnBalance = interestOnBalance;
        MinOfNegativeAmountOfMoney = minOfNegativeAmountOfMoney;
        NameOfBank = nameOfBank;
        LimitOfMoneyTransaction = limitOfMoneyTransaction;
    }

    public string NameOfBank { get; private set; }
    public decimal LimitOfMoneyTransaction { get; private set; }
    public decimal MinOfNegativeAmountOfMoney { get; private set; }
    public decimal CreditInterest { get; private set; }
    public decimal InterestOnBalance { get; private set; }
    private List<IBankAccount> ListOfBankAccounts { get; set; }
    private List<Client> ListOfClients { get; set; }
    private List<IObserver> ListOfObservers { get; set; }
    public IReadOnlyList<IBankAccount> GetAccounts() => ListOfBankAccounts;
    public IReadOnlyList<Client> GetClients() => ListOfClients;

    public Client AddClientBase(string name, string surname)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);
        Client client = new ClientBuilder().AddNameAndSurname(name, surname).Build();
        ListOfClients.Add(client);
        return client;
    }

    public void AddPasspotToClient(int passport, Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(passport);
        var currentClient = ListOfClients
            .FirstOrDefault(x => x == client);
        ArgumentNullException.ThrowIfNull(currentClient);

        currentClient.AddPassport(passport);
    }

    public void AddAdressToClient(string adress, Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(adress);
        var currentClient = ListOfClients
            .FirstOrDefault(x => x == client);
        ArgumentNullException.ThrowIfNull(currentClient);

        currentClient.AddAdress(adress);
    }

    public Client AddClientFullVersion(string name, string surname, string adress, int passport)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);
        ArgumentNullException.ThrowIfNull(adress);
        ArgumentNullException.ThrowIfNull(passport);
        Client client = new ClientBuilder().AddNameAndSurname(name, surname).AddAdress(adress).AddPassport(passport).Build();
        ListOfClients.Add(client);
        return client;
    }

    public Client FindClient(string name, string surname)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);
        var currentClient = ListOfClients
            .FirstOrDefault(x => x.Name == name && x.Surname == surname);
        ArgumentNullException.ThrowIfNull(currentClient);

        return currentClient;
    }

    public IBankAccount AddCreditAccount(Client client, decimal firstContribution)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(firstContribution);
        var currentClient = ListOfClients
            .FirstOrDefault(x => x == client);
        ArgumentNullException.ThrowIfNull(currentClient);

        IBankAccount bankAccount = new CreditAccount(client, firstContribution, CreditInterest, MinOfNegativeAmountOfMoney, LimitOfMoneyTransaction);
        ListOfBankAccounts.Add(bankAccount);
        return bankAccount;
    }

    public IBankAccount AddDebitAccount(Client client, decimal firstContribution)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(firstContribution);
        var currentClient = ListOfClients
            .FirstOrDefault(x => x == client);
        ArgumentNullException.ThrowIfNull(currentClient);

        IBankAccount bankAccount = new DebitAccount(client, firstContribution, InterestOnBalance, LimitOfMoneyTransaction);
        ListOfBankAccounts.Add(bankAccount);
        return bankAccount;
    }

    public IBankAccount AddDepositeAccount(Client client, decimal firstContribution, int accountTerm)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(firstContribution);
        ArgumentNullException.ThrowIfNull(accountTerm);
        var currentClient = ListOfClients
            .FirstOrDefault(x => x == client);
        ArgumentNullException.ThrowIfNull(currentClient);

        IBankAccount bankAccount = new DepositAccount(currentClient, firstContribution, CreditInterest, accountTerm, LimitOfMoneyTransaction);
        ListOfBankAccounts.Add(bankAccount);
        return bankAccount;
    }

    public void ChangeCredintInterest(decimal newCredintInterest)
    {
        CreditInterest = newCredintInterest;
        foreach (CreditAccount creditAccount in ListOfBankAccounts)
        {
            creditAccount.ChangeCreditInterest(newCredintInterest);
        }

        NotifyObservers();
    }

    public void ChangeInterestOnBalance(decimal newInterestOnBalance)
    {
        ArgumentNullException.ThrowIfNull(newInterestOnBalance);
        InterestOnBalance = newInterestOnBalance;
        foreach (var bankAccount in ListOfBankAccounts
                     .Where(x => x is not CreditAccount))
        {
            switch (bankAccount)
            {
                case DepositAccount depositAccount:
                    depositAccount.ChangeInterest(newInterestOnBalance);
                    break;
                case DebitAccount debitAccount:
                    debitAccount.ChangeInterest(newInterestOnBalance);
                    break;
            }
        }

        NotifyObservers();
    }

    public void ChangeLimitOfMoneyTransaction(decimal newLimit)
    {
        ArgumentNullException.ThrowIfNull(newLimit);
        LimitOfMoneyTransaction = newLimit;
        ListOfBankAccounts.ForEach(x => x.ChangeLimitOfMoneyTransaction(newLimit));
        NotifyObservers();
    }

    public void WithdrawMoney(IBankAccount bankAccount, decimal money)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);
        var currentAcc = ListOfBankAccounts
            .FirstOrDefault(x => x == bankAccount);
        ArgumentNullException.ThrowIfNull(currentAcc);

        currentAcc.WithdrawMoney(money);
    }

    public void TopUpBalance(IBankAccount bankAccount, decimal money)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);
        var currentAcc = ListOfBankAccounts
            .FirstOrDefault(x => x == bankAccount);
        ArgumentNullException.ThrowIfNull(currentAcc);

        currentAcc.TopUpBalance(money);
    }

    public void AddObserver(IObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        ListOfObservers.Add(observer);
    }

    public void RemoveObserever(IObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        ListOfObservers.Remove(observer);
    }

    public void NotifyObservers()
    {
        ListOfObservers.ForEach(x => x.Update(this));
    }

    public List<string> GetFullNamesOfClients()
    {
        List<string> list = new List<string>();
        ListOfClients.ForEach(x => list.Add(x.Name + " " + x.Surname));
        return list;
    }

    public List<string> GetNameOfClients()
    {
        List<string> list = new List<string>();
        ListOfClients.ForEach(x => list.Add(x.Name));
        return list;
    }

    public List<string> GetSurnamesOfClients()
    {
        List<string> list = new List<string>();
        ListOfClients.ForEach(x => list.Add(x.Surname));
        return list;
    }

    public Client FindClient(string fullName)
    {
        ArgumentNullException.ThrowIfNull(fullName);
        var currentClient = ListOfClients
            .FirstOrDefault(x => (x.Name + " " + x.Surname) == fullName);
        ArgumentNullException.ThrowIfNull(currentClient);

        return currentClient;
    }
}