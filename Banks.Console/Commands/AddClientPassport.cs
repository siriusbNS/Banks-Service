using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class AddClientPassport
{
    public AddClientPassport(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming4 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank4 = CentralBank.FindBank(bankNaming4);
        var clientName4 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetClients(bank4)));
        var client4 = CentralBank.ClientFind(bank4.NameOfBank, clientName4);
        var passport = AnsiConsole.Ask<int>("Введите пасспорт");
        CentralBank.AddPasspotToClient(bank4, passport, client4);
    }
}
