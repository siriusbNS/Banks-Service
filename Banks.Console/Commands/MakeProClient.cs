using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class MakeProClient
{
    public MakeProClient(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming2 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var name2 = AnsiConsole.Ask<string>("Введите имя");
        var surname2 = AnsiConsole.Ask<string>("Введите фамилию");
        var address = AnsiConsole.Ask<string>("Введите адресс");
        var passport = AnsiConsole.Ask<int>("Введите пасспорт");
        var bank2 = CentralBank.FindBank(bankNaming2);
        CentralBank.AddClientFullVersion(bank2, name2, surname2, address, passport);
    }
}