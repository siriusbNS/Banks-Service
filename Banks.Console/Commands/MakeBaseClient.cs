using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class MakeBaseClient
{
    public MakeBaseClient(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var name = AnsiConsole.Ask<string>("Введите имя");
        var surname = AnsiConsole.Ask<string>("Введите фамилию");
        var bank = CentralBank.FindBank(bankNaming);
        CentralBank.AddClientBase(bank, name, surname);
    }
}
