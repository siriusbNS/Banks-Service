using Banks.Console.Commands;
using Banks.Entities;

namespace Banks.Console;
using Spectre.Console;

public class ProgramSpectre
{
    public static void Main(string[] args)
    {
        AnsiConsole.Write(new FigletText("My Bank.").Centered().Color(Color.Aqua));
        var rule = new Rule("[blue]Welcome to My Bank[/]");
        AnsiConsole.Write(rule);
        AnsiConsole.Status()
            .Start("Creating Bank System...", ctx =>
            {
                AnsiConsole.MarkupLine("Option System...");
                Thread.Sleep(2000);
                ctx.Status("Working...");
                ctx.Spinner(Spinner.Known.Balloon2);
                ctx.SpinnerStyle(Style.Parse("green"));
                AnsiConsole.MarkupLine("Doing some more work...");
                Thread.Sleep(2500);
            });
        ICentralBank centralBank = new CentralBank();
        string[] operations = new string[]
        {
            "1. Зарегестрировать банк",
            "2. Создать базового клиента(Только имя и фамилия)",
            "3. Создать Про-версию клиента(Им,фамили,адресс и номер паспорта",
            "4. Добавить клиенту данные адресса",
            "5. Добавить клиенту данные паспорта",
            "6. Создать дебетовый счет клиенту",
            "7. Создать кредитный счет клиенту",
            "8. Создать депозитный счет клиенту",
            "9. Поменять процентную ставку в банке",
            "10. Поменять коммисию банка для кредитных счетов",
            "11. Поменять лимитный перевод для неверефицованных аккаунтов",
            "12. Подписаться на уведомления от банка",
            "13. Отписаться от уведомлений от банка",
            "14. Пополнить баланс счета",
            "15. Вывести деньги с счета",
            "16. Отменить транзакцию",
            "17. Перевести деньги с одного счета на другой",
            "18. Проматать время на какое то количество дней",
            "19. Промотать время на какое то количество месяцев",
        };
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText("My Bank.").Centered().Color(Color.Aqua));
            rule = new Rule("[green]Main Menu[/]");
            AnsiConsole.Write(rule);
            string answer = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose[green] Operation[/]")
                    .PageSize(20)
                    .MoreChoicesText("[grey][/]")
                    .AddChoices(new[]
                    {
                        operations[0],
                        operations[1],
                        operations[2],
                        operations[3],
                        operations[4],
                        operations[5],
                        operations[6],
                        operations[7],
                        operations[8],
                        operations[9],
                        operations[10],
                        operations[11],
                        operations[12],
                        operations[13],
                        operations[14],
                        operations[15],
                        operations[16],
                        operations[17],
                        operations[18],
                    }));
            switch (answer)
            {
                case "1. Зарегестрировать банк":
                    var commandRB = new RegisterBank(centralBank);
                    commandRB.Excecute();
                    break;
                case "2. Создать базового клиента(Только имя и фамилия)":
                    var commandMBC = new MakeBaseClient(centralBank);
                    commandMBC.Excecute();
                    break;
                case "3. Создать Про-версию клиента(Им,фамили,адресс и номер паспорта":
                    var commandMPC = new MakeProClient(centralBank);
                    commandMPC.Excecute();
                    break;
                case "4. Добавить клиенту данные адресса":
                    var commandACA = new AddClientAddres(centralBank);
                    commandACA.Excecute();
                    break;
                case "5. Добавить клиенту данные паспорта":
                    var commandACP = new AddClientPassport(centralBank);
                    commandACP.Excecute();
                    break;
                case "6. Создать дебетовый счет клиенту":
                    var commandMDebA = new MakeDebitAccount(centralBank);
                    commandMDebA.Excecute();
                    break;
                case "7. Создать кредитный счет клиенту":
                    var commandMCA = new MakeCreditAccount(centralBank);
                    commandMCA.Excecute();
                    break;
                case "8. Создать депозитный счет клиенту":
                    var commandMDepA = new MakeDepositAccount(centralBank);
                    commandMDepA.Excecute();
                    break;
                case "9. Поменять процентную ставку в банке":
                    var commandCIOB = new ChangeInterestOnBalance(centralBank);
                    commandCIOB.Excecute();
                    break;
                case "10. Поменять коммисию банка для кредитных счетов":
                    var commandCCI = new ChangeCreditInterest(centralBank);
                    commandCCI.Excecute();
                    break;
                case "11. Поменять лимитный перевод для неверефицованных аккаунтов":
                    var commandCLOT = new ChangeLimitOFTransfer(centralBank);
                    commandCLOT.Excecute();
                    break;
                case "12. Подписаться на уведомления от банка":
                    var commandCS = new ClientSubscribe(centralBank);
                    commandCS.Excecute();
                    break;
                case "13. Отписаться от уведомлений от банка":
                    var commandUS = new UnsubscribeClient(centralBank);
                    commandUS.Excecute();
                    break;
                case "14. Пополнить баланс счета":
                    var commandUTB = new UpToBalance(centralBank);
                    commandUTB.Excecute();
                    break;
                case "15. Вывести деньги с счета":
                    var commandWDM = new WithDrawMoney(centralBank);
                    commandWDM.Excecute();
                    break;
                case "17. Перевести деньги с одного счета на другой":
                    var commandTM = new TransferMoney(centralBank);
                    commandTM.Excecute();
                    break;
            }
        }
    }
}
