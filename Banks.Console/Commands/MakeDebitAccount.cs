using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class MakeDebitAccount
{
    public MakeDebitAccount(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming5 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank5 = CentralBank.FindBank(bankNaming5);
        var clientName5 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetClients(bank5)));
        var client5 = CentralBank.ClientFind(bank5.NameOfBank, clientName5);
        var firstContribution = AnsiConsole.Ask<decimal>("Введите первый взнос");
        CentralBank.AddDebitAccount(bank5, client5, firstContribution);
    }
}