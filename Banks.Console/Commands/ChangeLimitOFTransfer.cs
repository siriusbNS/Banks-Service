using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Console.Commands;

public class ChangeLimitOFTransfer
{
    public ChangeLimitOFTransfer(ICentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    private ICentralBank CentralBank { get; set; }

    public void Excecute()
    {
        var bankNaming10 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose[green] Bank[/]")
                .PageSize(10)
                .MoreChoicesText("[grey][/]")
                .AddChoices(CentralBank.GetNamesOfBanks()));
        var bank10 = CentralBank.FindBank(bankNaming10);
        var newLimit = AnsiConsole.Ask<decimal>("Введите новый кредитный лимит перевода");
        CentralBank.ChangeLimitOfMoneyTransaction(bank10, newLimit);
    }
}
