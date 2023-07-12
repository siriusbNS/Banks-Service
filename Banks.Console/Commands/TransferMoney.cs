using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class TransferMoney
{
    public TransferMoney(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var fisrtBankNaming = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Choose[green] Bank from[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey][/]")
                            .AddChoices(CentralBank.GetNamesOfBanks()));
        var secondBankNaming = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Choose[green] Bank To[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey][/]")
                            .AddChoices(CentralBank.GetNamesOfBanks()));
        var firstBank = CentralBank.FindBank(fisrtBankNaming);
        var secondBank = CentralBank.FindBank(secondBankNaming);
        var clientNamefirst = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Choose[green] client[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey][/]")
                            .AddChoices(CentralBank.GetClients(firstBank)));
        var clientNamesecond = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Choose[green] client[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey][/]")
                            .AddChoices(CentralBank.GetClients(secondBank)));
        var clientFirst = CentralBank.ClientFind(firstBank.NameOfBank, clientNamefirst);
        var clientSecond = CentralBank.ClientFind(firstBank.NameOfBank, clientNamesecond);
        var listFirst = CentralBank.FindAccounts(firstBank, clientFirst);
        var listSecond = CentralBank.FindAccounts(secondBank, clientSecond);
        var accountFrom = AnsiConsole.Prompt(
                        new SelectionPrompt<IBankAccount>()
                            .Title("Choose[green] Accont From[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey][/]")
                            .AddChoices(listFirst));
        var accountTo = AnsiConsole.Prompt(
                        new SelectionPrompt<IBankAccount>()
                            .Title("Choose[green] Accont To[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey][/]")
                            .AddChoices(listSecond));
        var money15 = AnsiConsole.Ask<decimal>("Введите сколько денег вы хотите перевести");
        CentralBank.MoneyTransfer(firstBank, secondBank, accountFrom, accountTo, money15);
    }
}
