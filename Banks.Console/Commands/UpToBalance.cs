using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class UpToBalance
{
    public UpToBalance(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming13 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank13 = CentralBank.FindBank(bankNaming13);
        var clientName13 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Client[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetClients(bank13)));
        var client13 = CentralBank.ClientFind(bank13.NameOfBank, clientName13);
        var list = CentralBank.FindAccounts(bank13, client13);
        var account = AnsiConsole.Prompt(
            new SelectionPrompt<IBankAccount>()
                .Title("Choose[green] Account[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(list));
        var money = AnsiConsole.Ask<decimal>("Введите сколько денег вы хотите пополнить");
        CentralBank.UpToBalance(bank13, account, money);
    }
}