namespace Banks.Entities;

public interface ICentralBank
{
    Bank AddBank(decimal creditInterest, decimal interestOnBalance, decimal minOfNegativeAmountOfMoney, decimal limitOfMoneyTransaction, string nameOfBank);

    TransferMoneyCommand MoneyTransfer(Bank bankFrom, Bank bankTo, IBankAccount bankAccountFrom, IBankAccount bankAccountTo, decimal money);
    void TimeMangaerPlusDays(int value);
    void TimeMangaerPlusMonths(int value);
    IReadOnlyList<Bank> GetBanks();
    void TimeMangaerPlusYears(int value);
    Client AddClientBase(Bank bank, string name, string surname);
    void AddPasspotToClient(Bank bank, int passport, Client client);
    void AddAdressToClient(Bank bank, string adress, Client client);
    Client AddClientFullVersion(Bank bank, string name, string surname, string adress, int passport);
    IBankAccount AddCreditAccount(Bank bank, Client client, decimal firstContribution);
    void ChangeCredintInterest(Bank bank, decimal newCredintInterest);
    void ChangeInterestOnBalance(Bank bank, decimal newInterestOnBalance);
    void ChangeLimitOfMoneyTransaction(Bank bank, decimal newLimit);
    IBankAccount AddDebitAccount(Bank bank, Client client, decimal firstContribution);
    IBankAccount AddDepositeAccount(Bank bank, Client client, decimal firstContribution, int accountTerm);
    void Subscribe(Bank bank, IObserver observer);
    void Unsubscribe(Bank bank, IObserver observer);
    UpToBalanceCommand UpToBalance(Bank bank, IBankAccount bankAccount, decimal money);
    WithDrawCommand WithDrawMoney(Bank bank, IBankAccount bankAccount, decimal money);
    void CancelTransaction(int id);
    void Update(object obj);
    List<string> GetNamesOfBanks();
    Bank FindBank(string name);
    List<string> GetClients(Bank bank);
    List<string> GetNamesClients(Bank bank);
    List<string> GetSurnamesClients(Bank bank);
    Client FindClient(string bankName, string name, string surname);
    Client ClientFind(string bankName, string fullName);
    List<IBankAccount> FindAccounts(Bank bank, Client client);
}