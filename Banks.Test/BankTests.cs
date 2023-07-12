using Banks.Entities;
using Banks.Tools;
using Xunit.Abstractions;

namespace Banks.Test;
using Xunit;

public class BankTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BankTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        ICentralBank centralBank = new CentralBank();
        var bank1 = centralBank.AddBank(1.10M, 3.65M, -10000, 10000, "Alpha");
        var bank2 = centralBank.AddBank(1.10M, 3.65M, -100000, 100000, "Tink");
        var client1 = centralBank.AddClientFullVersion(bank1, "Vlad", "Kuv", "Kron", 1222000000);
        var account = centralBank.AddDebitAccount(bank1, client1, 100000);
        centralBank.Subscribe(bank1, client1);
        centralBank.ChangeInterestOnBalance(bank1, 3.98M);
        Assert.Contains(bank1, centralBank.GetBanks().ToList());
    }

    [Fact]
    public void Test2()
    {
        ICentralBank centralBank = new CentralBank();
        var bank1 = centralBank.AddBank(1.10M, 3.65M, -10000, 10000, "Alpha");
        var bank2 = centralBank.AddBank(1.10M, 3.65M, -100000, 100000, "Tink");
        var client1 = centralBank.AddClientFullVersion(bank1, "Vlad", "Kuv", "Kron", 1222000000);
        var account = centralBank.AddDebitAccount(bank1, client1, 100000);
        Assert.Throws<MinAmountOfMoneyException>(() => centralBank.WithDrawMoney(bank1, account, 1000001));
    }

    [Fact]
    public void Test3()
    {
        ICentralBank centralBank = new CentralBank();
        decimal interest = 3.65M;
        var bank1 = centralBank.AddBank(1.10M, interest, -10000, 10000, "Alpha");
        var bank2 = centralBank.AddBank(1.10M, 3.65M, -100000, 100000, "Tink");
        var client1 = centralBank.AddClientFullVersion(bank1, "Vlad", "Kuv", "Kron", 1222000000);
        int money = 100000;
        decimal newMoney = (money * (interest / 365) * 30) + money;
        var account = centralBank.AddDebitAccount(bank1, client1, money);
        centralBank.TimeMangaerPlusMonths(1);
        Assert.Equal(account.AmountOfMoney, newMoney);
    }
}