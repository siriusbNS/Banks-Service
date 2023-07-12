using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class WithDrawMoney
{
    public WithDrawMoney(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming14 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank14 = CentralBank.FindBank(bankNaming14);
        var clientName14 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Client[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetClients(bank14)));
        var client14 = CentralBank.ClientFind(bank14.NameOfBank, clientName14);
        var list14 = CentralBank.FindAccounts(bank14, client14);
        var account14 = AnsiConsole.Prompt(
            new SelectionPrompt<IBankAccount>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(list14));
        var money14 = AnsiConsole.Ask<decimal>("Введите сколько денег вы хотите вывести");
        CentralBank.WithDrawMoney(bank14, account14, money14);
    }
}