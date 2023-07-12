using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class RegisterBank
{
    public RegisterBank(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankName = AnsiConsole.Ask<string>("Введите имя банка");
        var creditInterest = AnsiConsole.Ask<decimal>("Введите кредитный процент");
        var interestOnBalance = AnsiConsole.Ask<decimal>("Введите процентную ставку");
        var minOfNegativeAmountOfMoney = AnsiConsole.Ask<decimal>("Введите минимальный отрицательный баланс кредитного счета");
        var limitOfMoneyTransaction = AnsiConsole.Ask<decimal>("Введите лимит перевода");
        CentralBank.AddBank(creditInterest, interestOnBalance, minOfNegativeAmountOfMoney, limitOfMoneyTransaction, bankName);
        AnsiConsole.Write(new Markup("[green]Банк был успешно добавлен в базу данных.[/]"));
    }
}