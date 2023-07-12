using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class ChangeInterestOnBalance
{
    public ChangeInterestOnBalance(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming8 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank8 = CentralBank.FindBank(bankNaming8);
        var newInterestOnBalance = AnsiConsole.Ask<decimal>("Введите новую процентную ставку");
        CentralBank.ChangeInterestOnBalance(bank8, newInterestOnBalance);
    }
}