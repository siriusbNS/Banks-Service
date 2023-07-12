namespace Banks.Entities;

public class CentralBank : ICentralBank, IObserver
{
    public CentralBank()
    {
        ListOfBanks = new List<Bank>();
        Transactions = new List<ICommand>();
        TimeManager = new TimeManager();
        TimeManager.AddObserver(this);
    }

    private int Id { get; set; } = 1;
    private List<Bank> ListOfBanks { get; set; }
    private TimeManager TimeManager { get; set; }
    private List<ICommand> Transactions { get; set; }
    public IReadOnlyList<Bank> GetBanks() => ListOfBanks;

    public void TimeMangaerPlusDays(int value)
    {
        TimeManager.PlusDays(value);
    }

    public void TimeMangaerPlusMonths(int value)
    {
        TimeManager.PlusMonths(value);
    }

    public void TimeMangaerPlusYears(int value)
    {
        TimeManager.PlusYears(value);
    }

    public Bank AddBank(decimal creditInterest, decimal interestOnBalance, decimal minOfNegativeAmountOfMoney, decimal limitOfMoneyTransaction, string nameOfBank)
    {
        ArgumentNullException.ThrowIfNull(nameOfBank);
        Bank bank = new Bank(creditInterest, interestOnBalance, minOfNegativeAmountOfMoney, limitOfMoneyTransaction, nameOfBank);
        ListOfBanks.Add(bank);
        return bank;
    }

    public TransferMoneyCommand MoneyTransfer(Bank bankFrom, Bank bankTo, IBankAccount bankAccountFrom, IBankAccount bankAccountTo, decimal money)
    {
        ArgumentNullException.ThrowIfNull(bankFrom);
        ArgumentNullException.ThrowIfNull(bankTo);
        ArgumentNullException.ThrowIfNull(bankAccountFrom);
        ArgumentNullException.ThrowIfNull(bankAccountTo);
        var currentBankFrom = ListOfBanks
            .FirstOrDefault(x => x.NameOfBank == bankFrom.NameOfBank);
        var currentBankTo = ListOfBanks
            .FirstOrDefault(x => x.NameOfBank == bankTo.NameOfBank);
        ArgumentNullException.ThrowIfNull(currentBankTo);

        ArgumentNullException.ThrowIfNull(currentBankFrom);

        IBankAccount currentAccFrom = currentBankFrom
            .GetAccounts()
            .FirstOrDefault(x => x.Client == bankAccountFrom.Client);
        IBankAccount currentAccTo = currentBankTo
            .GetAccounts()
            .FirstOrDefault(x => x.Client == bankAccountTo.Client);
        ArgumentNullException.ThrowIfNull(currentAccFrom);

        ArgumentNullException.ThrowIfNull(currentAccTo);

        ICommand command = new TransferMoneyCommand(bankAccountFrom, bankAccountTo, money, Id);
        Id++;
        command.Execute();
        Transactions.Add(command);
        return (TransferMoneyCommand)command;
    }

    public Client AddClientBase(Bank bank, string name, string surname)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        ArgumentNullException.ThrowIfNull(currentBank);

        return currentBank.AddClientBase(name, surname);
    }

    public void AddPasspotToClient(Bank bank, int passport, Client client)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        ArgumentNullException.ThrowIfNull(currentBank);

        currentBank.AddPasspotToClient(passport, client);
    }

    public void AddAdressToClient(Bank bank, string adress, Client client)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        ArgumentNullException.ThrowIfNull(currentBank);

        currentBank.AddAdressToClient(adress, client);
    }

    public Client AddClientFullVersion(Bank bank, string name, string surname, string adress, int passport)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);
        ArgumentNullException.ThrowIfNull(adress);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        return currentBank.AddClientFullVersion(name, surname, adress, passport);
    }

    public IBankAccount AddCreditAccount(Bank bank, Client client, decimal firstContribution)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        return currentBank.AddCreditAccount(client, firstContribution);
    }

    public void ChangeCredintInterest(Bank bank, decimal newCredintInterest)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        currentBank.ChangeCredintInterest(newCredintInterest);
    }

    public void ChangeInterestOnBalance(Bank bank, decimal newInterestOnBalance)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        currentBank.ChangeInterestOnBalance(newInterestOnBalance);
    }

    public void ChangeLimitOfMoneyTransaction(Bank bank, decimal newLimit)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        currentBank.ChangeLimitOfMoneyTransaction(newLimit);
    }

    public IBankAccount AddDebitAccount(Bank bank, Client client, decimal firstContribution)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        return currentBank.AddDebitAccount(client, firstContribution);
    }

    public IBankAccount AddDepositeAccount(Bank bank, Client client, decimal firstContribution, int accountTerm)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        return currentBank.AddDepositeAccount(client, firstContribution, accountTerm);
    }

    public void Subscribe(Bank bank, IObserver observer)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(observer);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        currentBank.AddObserver(observer);
    }

    public void Unsubscribe(Bank bank, IObserver observer)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(observer);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        currentBank.RemoveObserever(observer);
    }

    public UpToBalanceCommand UpToBalance(Bank bank, IBankAccount bankAccount, decimal money)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(bankAccount);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        var currentAcc = currentBank
            .GetAccounts()
            .FirstOrDefault(x => x == bankAccount);
        if (currentAcc is null)
        {
            throw new Exception();
        }

        ICommand command = new UpToBalanceCommand(currentAcc, money, Id);
        Id++;
        command.Execute();
        Transactions.Add(command);
        return (UpToBalanceCommand)command;
    }

    public WithDrawCommand WithDrawMoney(Bank bank, IBankAccount bankAccount, decimal money)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(bankAccount);
        var currentBank = ListOfBanks
            .FirstOrDefault(x => x == bank);
        if (currentBank is null)
        {
            throw new Exception();
        }

        var currentAcc = currentBank
            .GetAccounts()
            .FirstOrDefault(x => x == bankAccount);
        if (currentAcc is null)
        {
            throw new Exception();
        }

        ICommand command = new WithDrawCommand(currentAcc, money, Id);
        Id++;
        command.Execute();
        Transactions.Add(command);
        return (WithDrawCommand)command;
    }

    public void CancelTransaction(int id)
    {
        ArgumentNullException.ThrowIfNull(id);
        var currentCommand = Transactions
            .FirstOrDefault(x => x.GetId() == id);
        if (currentCommand is null)
        {
            throw new Exception();
        }

        currentCommand.Undo();
        Transactions.Remove(currentCommand);
    }

    public void Update(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        DateTime dateTime = (DateTime)obj;
        foreach (var i in ListOfBanks)
        {
            foreach (var bankAccount in i.GetAccounts())
            {
                if (bankAccount is DepositAccount)
                {
                    var j = (DepositAccount)bankAccount;
                    j.CalculateInterestInDate(dateTime);
                }
            }

            foreach (var bankAccount in i.GetAccounts())
            {
                if (bankAccount is DebitAccount)
                {
                    var a = (DebitAccount)bankAccount;
                    a.CalculateInterestInDate(dateTime);
                }
            }
        }

        Console.WriteLine("Время перемотано на {0}.Проценты начислены.", dateTime.Date);
    }

    public List<string> GetNamesOfBanks()
    {
        List<string> list = new List<string>();
        if (ListOfBanks.Count == 0)
        {
            throw new Exception();
        }

        ListOfBanks.ForEach(x => list.Add(x.NameOfBank));
        return list;
    }

    public Bank FindBank(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        var currentBank = ListOfBanks.FirstOrDefault(x => x.NameOfBank == name);
        ArgumentNullException.ThrowIfNull(currentBank);
        return currentBank;
    }

    public List<string> GetClients(Bank bank)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks.FirstOrDefault(x => x.NameOfBank == bank.NameOfBank);
        ArgumentNullException.ThrowIfNull(currentBank);
        return currentBank.GetFullNamesOfClients();
    }

    public Client FindClient(string bankName, string name, string surname)
    {
        ArgumentNullException.ThrowIfNull(bankName);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);
        var currentBank = ListOfBanks.FirstOrDefault(x => x.NameOfBank == bankName);
        ArgumentNullException.ThrowIfNull(currentBank);
        return currentBank.FindClient(name, surname);
    }

    public Client ClientFind(string bankName, string fullName)
    {
        ArgumentNullException.ThrowIfNull(bankName);
        ArgumentNullException.ThrowIfNull(fullName);
        var currentBank = ListOfBanks.FirstOrDefault(x => x.NameOfBank == bankName);
        ArgumentNullException.ThrowIfNull(currentBank);
        return currentBank.FindClient(fullName);
    }

    public List<string> GetNamesClients(Bank bank)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks.FirstOrDefault(x => x.NameOfBank == bank.NameOfBank);
        ArgumentNullException.ThrowIfNull(currentBank);
        return currentBank.GetNameOfClients();
    }

    public List<string> GetSurnamesClients(Bank bank)
    {
        ArgumentNullException.ThrowIfNull(bank);
        var currentBank = ListOfBanks.FirstOrDefault(x => x.NameOfBank == bank.NameOfBank);
        ArgumentNullException.ThrowIfNull(currentBank);
        return currentBank.GetSurnamesOfClients();
    }

    public List<IBankAccount> FindAccounts(Bank bank, Client client)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        var currentBank = ListOfBanks.FirstOrDefault(x => x.NameOfBank == bank.NameOfBank);
        ArgumentNullException.ThrowIfNull(currentBank);
        var list = ListOfBanks.SelectMany(x => x.GetAccounts())
            .Where(x => x.Client == client)
            .ToList();
        if (list.Count == 0)
        {
            throw new Exception();
        }

        return list;
    }
}