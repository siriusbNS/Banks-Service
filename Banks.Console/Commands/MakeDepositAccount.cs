using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class MakeDepositAccount
{
    public MakeDepositAccount(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming7 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank7 = CentralBank.FindBank(bankNaming7);
        var clientName7 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetClients(bank7)));
        var client7 = CentralBank.ClientFind(bank7.NameOfBank, clientName7);
        var firstContribution7 = AnsiConsole.Ask<decimal>("Введите первый взнос");
        var accountTerm = AnsiConsole.Ask<int>("Введите на сколько дней вам нужен этот счет");
        CentralBank.AddDepositeAccount(bank7, client7, firstContribution7, accountTerm);
    }
}