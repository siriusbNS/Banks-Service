using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class MakeCreditAccount
{
    public MakeCreditAccount(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming6 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank6 = CentralBank.FindBank(bankNaming6);
        var clientName6 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetClients(bank6)));
        var client6 = CentralBank.ClientFind(bank6.NameOfBank, clientName6);
        var firstContribution1 = AnsiConsole.Ask<decimal>("Введите первый взнос");
        CentralBank.AddCreditAccount(bank6, client6, firstContribution1);
    }
}
