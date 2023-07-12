using Banks.Tools;

namespace Banks.Entities;

public class Client : IObserver
{
    private int _passportNumberMin = 1000000000;
    public Client(string name, string surname, string adress, int? numberOfPassport)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);
        Name = name;
        Surname = surname;
        VerificationPassed = false;
        if (adress is not null || numberOfPassport is not null)
        {
            VerificationPassed = true;
        }

        if (numberOfPassport < _passportNumberMin)
            throw new PassportNumberException("Passport is not correct");
        Adress = adress;
        NumberOfPassport = numberOfPassport;
    }

    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Adress { get; private set; }
    public int? NumberOfPassport { get; private set; }
    public bool VerificationPassed { get; private set; }

    public void AddPassport(int passport)
    {
        if (passport < _passportNumberMin)
            throw new PassportNumberException("Passport is not correct");
        NumberOfPassport = passport;
        VerificationPassed = true;
    }

    public void AddAdress(string adress)
    {
        ArgumentNullException.ThrowIfNull(adress);
        Adress = adress;
        VerificationPassed = true;
    }

    public void Update(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        Bank bankInf = (Bank)obj;
        Console.WriteLine("В банке {0} процентная ставка : {1}; кредитный процент : {2}; лимит на перевод в небезопасных аккаунтах : {3}", bankInf.NameOfBank, bankInf.InterestOnBalance, bankInf.CreditInterest, bankInf.LimitOfMoneyTransaction);
    }
}