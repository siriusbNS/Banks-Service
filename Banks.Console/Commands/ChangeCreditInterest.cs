using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class ChangeCreditInterest
{
    public ChangeCreditInterest(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming9 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank9 = CentralBank.FindBank(bankNaming9);
        var newCredintInterest = AnsiConsole.Ask<decimal>("Введите новый кредитный процент");
        CentralBank.ChangeCredintInterest(bank9, newCredintInterest);
    }
}
