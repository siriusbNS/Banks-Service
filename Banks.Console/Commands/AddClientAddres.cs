using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class AddClientAddres
{
    public AddClientAddres(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming3 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank3 = CentralBank.FindBank(bankNaming3);
        var clientName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetClients(bank3)));
        var client = CentralBank.ClientFind(bank3.NameOfBank, clientName);
        var adress = AnsiConsole.Ask<string>("Введите адресс");
        CentralBank.AddAdressToClient(bank3, adress, client);
    }
}